import { Component, OnInit, Input } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { AppState, User, Employee } from '@app/_models';
import { selectUserInfo } from '@app/appState.reducer';
import { Observable } from 'rxjs';
import { UserService } from '@app/_services';

@Component({
  selector: 'app-user-info-panel',
  templateUrl: './user-info-panel.component.html',
  styleUrls: ['./user-info-panel.component.less']
})
export class UserInfoPanelComponent implements OnInit {
  currentUser$: Observable<User>;
  profileInfo$: Observable<Employee>;
  constructor(private store: Store <{appstate: AppState}>, private userService: UserService) {
    this.currentUser$ = store.pipe(select("appstate"), select(selectUserInfo))
  }
  ngOnInit() {
    this.profileInfo$ = this.userService.getUserCurrentState();
  }

}
