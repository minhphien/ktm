import { Component, OnInit } from '@angular/core';
import { User } from '@app/_models/user';
import { Employee } from "@app/_models/employee";
import { Router } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { selectUserInfo } from '@app/appState.reducer';
import { AppState } from '@app/_models';
import { Observable, of } from 'rxjs';
import { mergeMap } from 'rxjs/operators';
import { UserService, AuthenticationService } from '@app/_services';
import { KudosService } from '@app/_services/kudos.service';

@Component({
  selector: 'app-master-layout',
  templateUrl: './master-layout.component.html',
  styleUrls: ['./master-layout.component.scss']
})
export class MasterLayoutComponent implements OnInit {
  currentUser$: Observable<Employee>;
  menus = [
    {
      name: 'Dashboard',
      path: '/home'
    },
    {
      name: 'Report',
      path: '/report'
    }
  ]

  constructor(private userService: UserService, private kudosService: KudosService) {
    this.currentUser$= this.userService.getUserCurrentState();
  }

  ngOnInit() {
    this.kudosService.getMyKudos();
  }
}
