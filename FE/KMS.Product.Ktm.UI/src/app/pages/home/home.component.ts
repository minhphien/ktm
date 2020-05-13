import { Component } from '@angular/core';
import { User } from '@app/_models/user';
import { Store, select } from '@ngrx/store';
import { AppState, Kudos, KudosState } from '@app/_models';
import { selectUserInfo } from '@app/appState.reducer';
import { Observable } from 'rxjs';
import { KudosService } from '@app/_services/kudos.service';
import { selectKudosReceived, selectKudosSent } from '@app/_reducers/kudos-list.reducer';

@Component({
  templateUrl: 'home.component.html'
})
export class HomeComponent {
  loading = false;
  
  currentUser$: Observable<User>;
  receivedKudos$: Observable<Kudos[]>;
  sentKudos$: Observable<Kudos[]>;
  
  userFromApi: User;
  gutter = 32;
  
  constructor(
    private store: Store<{appstate: AppState}>, 
    private kudosStore: Store<{kudosState: KudosState}>
  ) {
    this.currentUser$ = store.pipe(select("appstate"), select(selectUserInfo));
  }

  ngOnInit() {
    this.receivedKudos$ = this.kudosStore.pipe(select("kudosState"),select(selectKudosReceived));
    this.sentKudos$ = this.kudosStore.pipe(select("kudosState"),select(selectKudosSent));
  }
}