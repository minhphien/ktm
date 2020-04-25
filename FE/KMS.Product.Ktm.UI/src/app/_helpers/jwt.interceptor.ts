import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppState, User } from '@app/_models';
import { Store, select } from '@ngrx/store';
import { selectUserInfo } from '@app/appState.reducer';
import { mergeMap, first } from 'rxjs/operators';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private store: Store<{appstate: AppState}>) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add auth header with jwt if user is logged in and request is to api url
        return this.store.pipe(select("appstate"),select(selectUserInfo),first(),mergeMap((user: User)=>{
            console.log("JWT",user);
            //let tempToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiIzMDQiLCJuYW1laWQiOiJwaGllbmxlIiwidW5pcXVlX25hbWUiOiJQaGllbiBNaW5oIExlIiwiZ2l2ZW5fbmFtZSI6IkxlIiwiZmFtaWx5X25hbWUiOiJQaGllbiIsImVtYWlsIjoicGhpZW5sZUBrbXMtdGVjaG5vbG9neS5jb20iLCJwcmltYXJ5c2lkIjoiMDczNiIsIm5iZiI6MTU4NzQ4MTIwNywiZXhwIjoxNTk1MjU3MjA3LCJpYXQiOjE1ODc0ODEyMDd9.nUvYyYPi4PBwEoAv3PdXU2JvAxnzNJ3NsjC2M-BHOVw';
            if(user && user.token){
                return next.handle(request.clone({
                    setHeaders: {
                        "Content-Type": "application/json",
                        Authorization: `Bearer ${user.token}`
                    }
                }));
            } else return next.handle(request);
        }));
    }
}