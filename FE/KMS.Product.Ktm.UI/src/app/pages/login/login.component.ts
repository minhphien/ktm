import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AuthenticationService } from '@app/_services';
import { environment } from '@environments/environment';
import { State, Store, select } from '@ngrx/store';
import { AppState, User } from '@app/_models';
import { selectUserInfo, updateUser } from '@app/appState.reducer';
import { Observable } from 'rxjs';

@Component({ templateUrl: 'login.component.html' })
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;
    error = '';
    return: string = '';
    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private appState: State<{appstate: AppState}>,
        private store: Store<{appstate: AppState}>,
        private authenticationService: AuthenticationService
    ) { 
    }

    signOn(){
        console.log("signing on")
        const urlParams = new URLSearchParams(window.location.search);
        const accessToken = urlParams.get('accessToken');
        if (accessToken) {
          this.authenticationService.signOn(accessToken).pipe(first()).subscribe((user:User)=>{
            console.log('from sign in', user);  
            this.store.dispatch(updateUser(user));
              if(user) {
                  this.router.navigate(['/home']);
              }
          });
        } else {
          window.location.replace(`${environment.KmsHomeUrl}/login?returnUrl=${document.URL}`);
        }
    }

    ngOnInit() {
        // get return url from route parameters or default to '/'
        
        // redirect to home if already logged in
        let user = <AppState>this.appState.getValue();
        console.log('info from login',user)
        this.signOn();
    }
}
