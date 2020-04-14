import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthenticationService } from './_services';

@Component({ selector: 'app', templateUrl: 'app.component.html' })
export class AppComponent implements OnInit {
    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) {

    }
    ngOnInit(): void {
    }

    get isAdmin() {
        // return this.currentUser && this.currentUser.role === Role.Admin;
        return true;
    }

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }
}