import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthenticationService,AppService } from '@app/_services';
import { environment } from '@environments/environment';
import { State, Store, select } from '@ngrx/store';
import { AppState, User } from '@app/_models';
import { selectUserInfo, updateUser } from '@app/appState.reducer';
import { DefaultUrlSerializer,UrlSerializer } from "@angular/router";

@Component({ templateUrl: 'login.component.html' })
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    loading = false;
    submitted = false;
    returnUrl: string;
    error = '';
    return: string = '';
    constructor(
        private router: Router,
        private appState: State<{appstate: AppState}>,
        private store: Store<{appstate: AppState}>,
        private authenticationService: AuthenticationService
    ) {  }

    signOn(){
        const urlParams = new URLSearchParams(window.location.search);
        
        const accessToken = urlParams.get('accessToken');
        
        if (accessToken) {
          this.authenticationService.signOn(accessToken).pipe(first()).subscribe((user:User)=> { 
            this.store.dispatch(updateUser(user));
              if(user) {
                var currenturl = this.authenticationService.endRetry();
                if (!currenturl) {this.router.navigate(['/']);}
                else {
                  var parser = document.createElement('a');
                  parser.href = currenturl;
                  var params = new URLSearchParams(parser.search);
                  this.router.navigate([params.get('returnUrl')]);
                }
                
              }
          });
        } else {
          if(this.authenticationService.validToRetryLogin()){
            this.authenticationService.startRetry(`${document.URL}`);
            window.location.replace(`${environment.KmsHomeUrl}/login?returnUrl=${document.URL}`);
          } else{
            this.authenticationService.endRetry();
            this.router.navigate(['/404']);
          }
        }
    }

    ngOnInit() {
        let user = <AppState>this.appState.getValue();
        this.signOn();
    }
}
