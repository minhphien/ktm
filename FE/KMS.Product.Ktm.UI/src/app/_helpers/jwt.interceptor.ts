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