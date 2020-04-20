import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

import { UserService } from '@app/_services';
import { AppState, User } from '@app/_models';
import { Store, State } from '@ngrx/store';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
    userInfo: User; 
    constructor(
        private router: Router,
        private userService: UserService,
        private state: State<{appState: AppState}>
    ) {
        
     }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let userInfo =  ((this.state.getValue()).appstate).userInfo || this.userService.getUserInfoSession();
        console.log('guard', userInfo);
        if (!userInfo) {
            this.router.navigate(['/login'],{
                queryParams: {
                  returnUrl: state.url
                }
              });
            return false;
        } else {
            return true;
        }
    }
}