import { Component, OnInit,  AfterViewInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Kudos, KudosState } from '@app/_models';
import { KudosService } from '@app/_services/kudos.service';
import { Store } from '@ngrx/store';
import * as _ from 'underscore';

export class SelectFilter {
  name: string;
  value: string;
  disabled?: boolean ;
}

class ReportFilters {
  selectedKudosType?: any;
  selectedTeam?: any;
  dateRange?: Date[];
}


@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {
  kudosData$: Observable<any>;
  listOfTypes:SelectFilter[] = [
    {name: "Kudos", value: "1"},
    {name: "Gift", value: "2", disabled: false},
    {name: "Compliment", value: "3", disabled: true},
    {name: "Travel abroad", value: "4", disabled: true}
  ];
  listOfTeams:SelectFilter[] = [{name: "Default", value: "1", disabled: false}, {name: "Default 2", value: "2", disabled: false}]
  filters: ReportFilters = {
    selectedKudosType: _.first(this.listOfTypes),
    selectedTeam: _.first(this.listOfTeams)
  }
  
  dateRange: any;
   
  
  constructor(private kudosService: KudosService, private store: Store<{kudosState: KudosState}>) { }

  compareFn = (o1: any, o2: any) => (o1 && o2 ? o1.value === o2.value : o1 === o2);

  ngOnInit() {
    this.updateDataset(this.filters);
  }

  updateDataset(val: ReportFilters){
    this.kudosData$ = this.kudosService.getKudosReport(
      val.selectedTeam.value, 
      val.selectedKudosType.value, 
      val.dateRange
      );
  }

  onFilterChanged(){
    this.updateDataset(this.filters);
    console.log(this.dateRange);
  }

  trackById(index: number, data: any): number {
    return data ? data.employee.badgeId : 0;
  }
}
