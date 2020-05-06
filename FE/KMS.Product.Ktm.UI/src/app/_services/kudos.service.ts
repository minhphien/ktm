import { Injectable } from '@angular/core';
import { Observable , of, throwError } from 'rxjs';
import { retry, concat, retryWhen, take, flatMap, delay, map } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { HttpClient } from '@angular/common/http';
import { LightKudos, KudosState } from '@app/_models';
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
    console.log(url);
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
    })
    );
    request$.subscribe((kudos:KudosState)=>{
      this.store.dispatch(updateKudos(kudos));
    });
    return request$;
  }

  createKudos(data: LightKudos): Observable<Object> {
    let url = `${environment.apiUrl}${environment.methods.CreateKudos}`;
    console.log(url);
    let request$ = this.http.post(url, data);
    return request$;
  }
}
