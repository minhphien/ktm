import { Injectable } from '@angular/core';
import { Observable , of, throwError } from 'rxjs';
import { retry, concat, retryWhen, take, flatMap, delay, map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { LightKudos, KudosState, Kudos } from '@app/_models';
import { Store } from '@ngrx/store';
import { updateKudos } from '@app/_reducers/kudos-list.reducer';
import * as _ from 'underscore';
import { SelectFilter } from '@app/_models/SelectFilter';
import { KudosType } from '@app/_models/kudosType';


@Injectable({
  providedIn: 'root'
})
export class KudosService {

  constructor(private http: HttpClient, private store: Store<{kudosState: KudosState}>) { 
    
  }
  
  getMyKudos(): Observable<any> {
    let url = `${environment.apiUrl}${environment.methods.UserKudos}`;
    let request$ = this.http.get(url).pipe(retryWhen(error => {
        return error.pipe(
          flatMap((error: any) => {
            
              if(error.status  === 503) {
                return of(error.status).pipe(delay(1500));
              }
              return throwError({error: 'No retry'});
          }),
          take(5),
          concat(throwError({error: 'Sorry, there was an error (after 5 retries)'})));
      }),
      map((kudos:KudosState)=>{
        _.each(kudos.kudoReceives,x=>{
            x.senderImgUrl = `${environment.hrmUrls.domain}${environment.hrmUrls.methods.ReturnPhoto}/${x.senderEmployeeNumber}/300`;
            x.receiverImgUrl = `${environment.hrmUrls.domain}${environment.hrmUrls.methods.ReturnPhoto}/${x.receiverEmployeeNumber}/300`;});
        _.each(kudos.kudoSends,x=>{
          x.senderImgUrl = `${environment.hrmUrls.domain}${environment.hrmUrls.methods.ReturnPhoto}/${x.senderEmployeeNumber}/300`;
          x.receiverImgUrl = `${environment.hrmUrls.domain}${environment.hrmUrls.methods.ReturnPhoto}/${x.receiverEmployeeNumber}/300`;});
        kudos.kudoReceives = _.sortBy(kudos.kudoReceives,(x)=>x.created).reverse();
        kudos.kudoSends = _.sortBy(kudos.kudoSends,(x)=>x.created).reverse();

        return kudos;
    }));
    request$.subscribe((kudos:KudosState)=>{
      this.store.dispatch(updateKudos(kudos));
    });
    return request$;
  }

  createKudos(data: LightKudos): Observable<Object> {
    let url = `${environment.apiUrl}${environment.methods.CreateKudos}`;
    let request$ = this.http.post(url, data);
    return request$;
  }

  getKudosReportData(teamId: number, kudoType: number, dateRange?: Date[]): any {
    let params:HttpParams;
    params = this
      .appendDateParams(params,dateRange)
      .append("kudoTypeIds", kudoType.toString())
      .append("teamIds", teamId.toString());

    let url = `${environment.apiUrl}${environment.methods.Report}`;
    let request$ = this.http.get(url, { params: params});

    return request$;
  }

  getReceivedKudosByUserReportData(badgeId: string, kudoType: number, dateRange?: Date[]) {
    let params: HttpParams;
    params = this
      .appendDateParams(params,dateRange)
      .append("kudoTypeIds", kudoType.toString());
      
    let url = `${environment.apiUrl}${environment.methods.ReportReceivedByUser}/${badgeId}`;
    return this.requestKudosDetailByUserReportData(url, params);
  }

  getSentKudosByUserReportData(badgeId: string, kudoType: number, dateRange?: Date[]) {
    let params:HttpParams;
    params = this
      .appendDateParams(params,dateRange)
      .append("kudoTypeIds", kudoType.toString());

    let url = `${environment.apiUrl}${environment.methods.ReportSentByUser}/${badgeId}`;
    return this.requestKudosDetailByUserReportData(url, params);
  }

  private requestKudosDetailByUserReportData(url, params?: HttpParams) {
    let request$ = this.http.get(url, {params: params}).pipe(retryWhen(error => {
      return error.pipe(
        flatMap((error: any) => {
            if(error.status  === 503) {
              return of(error.status).pipe(delay(1500));
            }
            return throwError({error: 'No retry'});
        }),
        take(5),
        concat(throwError({error: 'Sorry, there was an error (after 5 retries)'})));
    }),
    map((kudos:Kudos[])=>{
      _.each(kudos,x=>{
        x.senderImgUrl = `${environment.hrmUrls.domain}${environment.hrmUrls.methods.ReturnPhoto}/${x.senderEmployeeNumber}/300`;
        x.receiverImgUrl = `${environment.hrmUrls.domain}${environment.hrmUrls.methods.ReturnPhoto}/${x.receiverEmployeeNumber}/300`;});
      kudos = _.sortBy(kudos,(x)=>x.created).reverse();
      return kudos;
    }));
    return request$;
  }

  getKudosAcrossTeamReportData(kudoType: number, dateRange: Date[], teamIds: string[]): any {
    let params: HttpParams = new HttpParams()
    params = this.appendParamList(params, "teamIds", teamIds);
    params = this
      .appendDateParams(params,dateRange)
      .append("kudoTypeIds", kudoType.toString());

    let url = `${environment.apiUrl}${environment.methods.ReportKudosAcrossTeam}`;

    let request$ = this.http.get(url, {params: params});
    return request$;
  }

  private appendDateParams(params: HttpParams, dateRange?: Date[]): HttpParams {
    if(!params){ params = new HttpParams() }
    if(!dateRange) return params;
    return params
      .append("dateFrom", dateRange[0] ? dateRange[0].toDateString(): "")
      .append("dateTo", dateRange[1] ? dateRange[1].toDateString(): "")
  }

  private appendParamList(params: HttpParams, key: string, items: string[]){
    if(!params){ params = new HttpParams() }
    if (!items) return params;
    console.log('check', items);
    _.forEach(items, (x) => { 
      params = params.append(key, x)
    });
    return params;
  }
}