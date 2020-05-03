import { Component, OnInit } from '@angular/core';
import { KudosService } from '@app/_services/kudos.service';
import { Observable } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { KudosState, Kudos } from '@app/_models';
import { selectKudosList, selectKudosReceived } from '@app/_reducers/kudos-list.reducer';

@Component({
  selector: 'app-table-kudos-recevied',
  templateUrl: './table-kudos-recevied.component.html',
  styleUrls: ['./table-kudos-recevied.component.less']
})
export class TableKudosReceivedComponent implements OnInit {
  kudosItems$: Observable<Kudos[]>;
  constructor(private kudosService: KudosService, private store: Store<{kudosState: KudosState}>) {
    this.kudosItems$ = this.store.pipe(select("kudosState"),select(selectKudosReceived));
   }

  ngOnInit() {
    
  }
  // Treat the instructor ID as the unique identifier for the object
  trackById(index: number, data: Kudos): number {
    return data ? data.id : 0;
  }
}
