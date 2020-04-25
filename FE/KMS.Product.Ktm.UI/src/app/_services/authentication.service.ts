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
    
}