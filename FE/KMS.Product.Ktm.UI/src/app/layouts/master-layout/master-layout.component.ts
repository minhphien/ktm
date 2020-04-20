import { Component, OnInit } from '@angular/core';
import { User } from '@app/_models/user';
import { Employee } from "@app/_models/employee";
import { Router } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { selectUserInfo } from '@app/appState.reducer';
import { AppState } from '@app/_models';
import { Observable, of } from 'rxjs';
import { mergeMap } from 'rxjs/operators';
import { UserService } from '@app/_services';

@Component({
  selector: 'app-master-layout',
  templateUrl: './master-layout.component.html',
  styleUrls: ['./master-layout.component.scss']
})
export class MasterLayoutComponent implements OnInit {
  currentUser$: Observable<User>;
  menus = [
    {
      name: 'Home',
      path: '/home'
    },
    {
      name: 'Report',
      path: '/report'
    },
    {
      name: 'User info',
      path: '/user-profile'
    }
  ]

  constructor(
    private store: Store <{appstate: AppState}>,
    private userService: UserService
  ) {
    this.currentUser$= store.pipe(select("appstate"),select(selectUserInfo));
  }

  ngOnInit() {
  }
}
