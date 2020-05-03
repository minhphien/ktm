import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Kudos, KudosState } from '@app/_models';
import { KudosService } from '@app/_services/kudos.service';
import { Store, select } from '@ngrx/store';
import { selectKudosReceived } from '@app/_reducers/kudos-list.reducer';

export class SelectFilter {
  name: string;
  value?: string;
  disabled?: boolean ;
}
class ReportFilters {
  kudosType?: string;
  team?: string;
  dateFrom?: Date;
  dateTo?: Date;
}
@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.less']
})
export class ReportComponent implements OnInit {
  kudosItems$: Observable<Kudos[]>;
  listOfOption: SelectFilter[] = [
    {name: "Kudos", value: "Kudos"},
    {name: "Gift", value: "Gift", disabled: true},
    {name: "Gift", value: "ff", disabled: true},
  ];
  listOfTeam: SelectFilter[];
  filters: ReportFilters;
  constructor(private kudosService: KudosService, private store: Store<{kudosState: KudosState}>) {
    this.kudosItems$ = this.store.pipe(select("kudosState"),select(selectKudosReceived));
    this.filters = {
      kudosType: this.listOfOption[0].name
    }
  }

  ngOnInit() {
    if (!this.kudosItems$) this.kudosService.getMyKudos();
  }
  // Treat the instructor ID as the unique identifier for the object
  trackById(index: number, data: Kudos): number {
    return data ? data.id : 0;
  }
}
