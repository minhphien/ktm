﻿import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { first, map } from "rxjs/operators"
import { UserService } from './_services';
import { Observable } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { loading, loaded, selectLoading, updateUser, selectUserInfo } from './appState.reducer';
import { AppState, User } from './_models';

@Component({ selector: 'app', templateUrl: 'app.component.html' })
export class AppComponent implements OnInit {
    loading$: Observable<boolean>;
    user$: Observable<User>;
    constructor(
        private router: Router,
        private store: Store<{appstate: AppState}>,
        private userService: UserService
        ) 
    {
        
    }

    ngOnInit(): void {
        this.loading$ = this.store.pipe(select("appstate"),select(selectLoading));        
        this.user$ = this.store.pipe(select("appstate"), select(selectUserInfo));

        this.store.dispatch(loading());        
        this.store.dispatch(loaded());
    }
}