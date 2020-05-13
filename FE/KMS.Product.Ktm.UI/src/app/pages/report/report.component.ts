import { Component, OnInit,  AfterViewInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Kudos, KudosState, Employee } from '@app/_models';
import { KudosService } from '@app/_services/kudos.service';
import { Store } from '@ngrx/store';
import * as _ from 'underscore';

export class SelectFilter {
  name: string;
  value: string;
  disabled?: boolean ;  
}

//TODO: convert any to explicit type, if possible.
class ReportFilters {
  selectedKudosType?: any;
  selectedTeam?: any;
  dateRange?: Date[];
  subFilter?: {
    detailReportType: string;
    visible: boolean;
    employee: string;
  }
}


@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent implements OnInit {
  kudosData$: Observable<any>;
  subviewData$: Observable<any>;
  listOfReports:SelectFilter[] = [
    {name: "Sent/Received kudos by one Team", value: "1", disabled: false},
    {name: "Sent/Received kudos across Teams", value: "2", disabled: false},
    {name: "Sent/Received kudos by users", value: "3", disabled: false}
  ];

  selectedReport: any = _.first(this.listOfReports);

  listOfTypes:SelectFilter[] = [
    {name: "Kudos", value: "1"},
    {name: "Gift", value: "2", disabled: false},
    {name: "Compliment", value: "3", disabled: true},
    {name: "Travel abroad", value: "4", disabled: true}
  ];

  listOfTeams:SelectFilter[] = [{name: "Default", value: "1", disabled: false}, {name: "Default 2", value: "2", disabled: false}]
  
  filters: ReportFilters = {
    selectedKudosType: _.first(this.listOfTypes),
    selectedTeam: _.first(this.listOfTeams),
    dateRange: [],
    subFilter: {
      employee: null,
      visible: false,
      detailReportType: null      
    }
  }

  constructor(private kudosService: KudosService, private store: Store<{kudosState: KudosState}>) { }

  compareFn = (o1: any, o2: any) => (o1 && o2 ? o1.value === o2.value : o1 === o2);

  ngOnInit() {
    this.updateDataset(this.filters);
  }

  updateDataset(val: ReportFilters) {
    this.kudosData$ = this.kudosService.getKudosReport(
      val.selectedTeam.value, 
      val.selectedKudosType.value, 
      val.dateRange
    );
  }

  onFilterChanged() {
    this.updateDataset(this.filters);
    this.closeSubView();
    console.log(this.filters.dateRange);
  }

  trackById(index: number, data: any): number {
    return data ? data.employee.badgeId : 0;
  }
  
  //TODO: convert employee:string to enum
  openReceiveSubView(employee: any) {
    this.openSubView();
    this.filters.subFilter.detailReportType = 'received';
    this.filters.subFilter.employee = employee;
    this.subviewData$ = this.kudosService.getReceivedKudosByUserReport(employee.badgeId, this.filters.selectedKudosType.value, this.filters.dateRange);
  }

  openSendSubView(employee: any) {
    this.openSubView();
    this.filters.subFilter.detailReportType = 'sent';
    this.filters.subFilter.employee = employee;
    this.subviewData$ = this.kudosService.getSentKudosByUserReport(employee.badgeId, this.filters.selectedKudosType.value, this.filters.dateRange);
  }

  openSubView(){   
    this.filters.subFilter.visible = true;
  }

  closeSubView() {
    this.filters.subFilter.visible = false;
  }

}
