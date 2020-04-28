import { Component, OnInit } from '@angular/core';
import { KudosService } from '@app/_services/kudos.service';
import { Observable } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { KudosState, Kudos } from '@app/_models';
import { selectKudosList, selectKudosSent } from '@app/_reducers/kudos-list.reducer';

@Component({
  selector: 'app-table-kudos-sent',
  templateUrl: './table-kudos-sent.component.html',
  styleUrls: ['./table-kudos-sent.component.less']
})
export class TableKudosSentComponent implements OnInit {
  kudosItems$: Observable<Kudos[]>;
  constructor(private kudosService: KudosService, private store: Store<{kudosState: KudosState}>) { }

  ngOnInit() {
    this.kudosItems$ = this.store.pipe(select("kudosState"),select(selectKudosSent));
  }
  // Treat the instructor ID as the unique identifier for the object
  trackById(index: number, data: Kudos): number {
    return data ? data.id : 0;
  }
}
