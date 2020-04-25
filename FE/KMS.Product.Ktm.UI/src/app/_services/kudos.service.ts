import { Injectable } from '@angular/core';
import { Observable , of, throwError } from 'rxjs';
import { retry, concat, retryWhen, take, flatMap, delay } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class KudosService {

  constructor(private http: HttpClient) { 
    
  }
  getMyKudos(): Observable<any> {
    let url = `${environment.apiUrl}${environment.methods.UserKudos}`;
    console.log(url);
    return this.http.get(url).pipe(retryWhen(error => {
      return error.pipe(
         flatMap((error: any) => {
            if(error.status  === 503) {
              return of(error.status).pipe(delay(1000));
            }
            return throwError({error: 'No retry'});
         }),
         take(5),
         concat(throwError({error: 'Sorry, there was an error (after 5 retries)'})));
      }));
  }
  createKudos() {

  }
}
