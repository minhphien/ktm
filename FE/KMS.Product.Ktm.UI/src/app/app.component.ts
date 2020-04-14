import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from './_services';
import { User, Role } from './_models';
import { first } from 'rxjs/operators';

@Component({ selector: 'app', templateUrl: 'app.component.html' })
export class AppComponent implements OnInit {
    loading = false;
    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) {

    }
    ngOnInit(): void {
        const urlParams = new URLSearchParams(window.location.search);
        const accessToken = urlParams.get('accessToken');

        if (accessToken) {
            this.loading = true;
            this.authenticationService.login(accessToken).pipe(first()).subscribe(() => {
                this.loading = false;
                this.router.navigate([window.location.pathname]);
            });
        } else {
            window.location.replace("https://home.kms-technology.com/login?returnUrl=http://localhost:4200" + window.location.pathname)
        }
        // if(!this.currentUser) {
        //     window.location.replace("https://home.kms-technology.com/login?returnUrl=http://localhost:4200/")
        // }
    }

    // ngOnInit(): void {
    //     window.location.replace()
    // }

    get isAdmin() {
        // return this.currentUser && this.currentUser.role === Role.Admin;
        return true;
    }

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }
}