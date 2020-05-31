import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { merge,of, BehaviorSubject, Observable } from 'rxjs';
import { map, first, retry } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { User, AppState } from '@app/_models';
import { Store, select } from '@ngrx/store';
import { selectUserInfo, updateUser, deleteUser } from '@app/appState.reducer';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {

    public static MAX_RETRY: number = 1; 
    private readonly RETRY_NAME: string = "retry"; 
    private readonly ROUTE_URL: string = "routeUrl"; 
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser$: Observable<User>;

    constructor(private http: HttpClient, private store: Store<{appstate: AppState}>) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    }

    get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    signOn(accessToken: string) {
        this.currentUser$ =  this.http.get<User>(`${environment.KmsHomeUrl}/api/account/authenticate`, 
                { headers: { "Authorization": `Bearer ${accessToken}` } });
        this.currentUser$.subscribe((user: User) => { 
            this.store.dispatch(updateUser(user));
        });
        return this.currentUser$;
    }

    signOut() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.store.dispatch(deleteUser());
        this.currentUserSubject.next(null);
    }

    validToRetryLogin(): boolean {
        return (Number(this.getSessionField(this.RETRY_NAME) || "0")) < AuthenticationService.MAX_RETRY;
    }

    startRetry(routeUrl: string) {
        let val = Number(this.getSessionField(this.RETRY_NAME)) || 0;
        sessionStorage.setItem(this.RETRY_NAME, `${val+1}`);
        sessionStorage.setItem(this.ROUTE_URL, routeUrl);
    }

    endRetry(): string {
        sessionStorage.removeItem(this.RETRY_NAME);
        var url = sessionStorage.getItem(this.ROUTE_URL);
        sessionStorage.removeItem(this.ROUTE_URL);
        return url;
    }

    private getSessionField(key: string): string{
        let val = sessionStorage.getItem(key);
        if (val === undefined) sessionStorage.setItem(key, "0");
        return val;
    }
    
}