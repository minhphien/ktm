import { Component } from '@angular/core';
import { User } from '@app/_models/user';
import { Store, select } from '@ngrx/store';
import { AppState } from '@app/_models';
import { selectUserInfo } from '@app/appState.reducer';
import { Observable } from 'rxjs';
import { KudosService } from '@app/_services/kudos.service';

@Component({
  templateUrl: 'home.component.html'
})
export class HomeComponent {
  loading = false;
  currentUser$: Observable<User>;
  userFromApi: User;
  gutter = 32;
  constructor(
    private store: Store<{appstate: AppState}>
  ) {
    this.currentUser$ = store.pipe(select("appstate"), select(selectUserInfo));
  }

  ngOnInit() {
  }
}