import { Injectable } from '@angular/core';
import { Observable , of, throwError } from 'rxjs';
import { retry, concat, retryWhen, take, flatMap, delay, map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { LightKudos, KudosState, Kudos } from '@app/_models';
import { Store } from '@ngrx/store';
import { updateKudos } from '@app/_reducers/kudos-list.reducer';
import * as _ from 'underscore';


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
    let dateParams = `${dateRange && dateRange[0] ? '&dateFrom='+dateRange[0].toDateString():""}${dateRange && dateRange[1] ? '&dateTo='+dateRange[1].toDateString():""}`;
    let url = `${environment.apiUrl}${environment.methods.Report}?teamIds=${teamId}&kudoTypeIds=${kudoType}${dateParams}`;
    let request$ = this.http.get(url);
    return request$;
  }

  getReceivedKudosByUserReportData(badgeId: string, kudoType: number, dateRange?: Date[]) {
    let dateParams = `${dateRange && dateRange[0] ? '&dateFrom='+dateRange[0].toDateString():""}${dateRange && dateRange[1] ? '&dateTo='+dateRange[1].toDateString():""}`;
    let url = `${environment.apiUrl}${environment.methods.ReportReceivedByUser}/${badgeId}?kudoTypeIds=${kudoType}${dateParams}`;
    return this.requestKudosDetailByUserReportData(url);
  }

  getSentKudosByUserReportData(badgeId: string, kudoType: number, dateRange?: Date[]) {
    let dateParams = `${dateRange && dateRange[0] ? '&dateFrom='+dateRange[0].toDateString():""}${dateRange && dateRange[1] ? '&dateTo='+dateRange[1].toDateString():""}`;
    let url = `${environment.apiUrl}${environment.methods.ReportSentByUser}/${badgeId}?kudoTypeIds=${kudoType}${dateParams}`;
    return this.requestKudosDetailByUserReportData(url);
  }

  private requestKudosDetailByUserReportData(url) {
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
    map((kudos:Kudos[])=>{
      _.each(kudos,x=>{
        x.senderImgUrl = `${environment.hrmUrls.domain}${environment.hrmUrls.methods.ReturnPhoto}/${x.senderEmployeeNumber}/300`;
        x.receiverImgUrl = `${environment.hrmUrls.domain}${environment.hrmUrls.methods.ReturnPhoto}/${x.receiverEmployeeNumber}/300`;});
      kudos = _.sortBy(kudos,(x)=>x.created).reverse();
      return kudos;
    }));
    console.log('starting ', request$);
    return request$;
  }

  getKudosAcrossTeamReportData(teamIds: string[], kudoType: number, dateRange?: Date[]): any {
    let params:HttpParams = new HttpParams()
      .append("dateFrom", dateRange && dateRange[0] ? dateRange[0].toDateString(): "")
      .append("dateTo", dateRange && dateRange[1] ? dateRange[1].toDateString(): "")
      .append("kudoTypeIds", kudoType.toString())
    _.each(teamIds, (x)=>{ params.append("teamIds",x)});
    let url = `${environment.apiUrl}${environment.methods.ReportKudosAcrossTeam}`;
    console.log('requestKudos',url);
    let request$ = this.http.get(url, {params: params});
    return request$;
  }
}