import { Component, OnInit,  AfterViewInit } from '@angular/core';
import { Observable, of, merge } from 'rxjs';
import { KudosService } from '@app/_services/kudos.service';
import * as _ from 'underscore';
import { ReportFilters } from '@app/_models/ReportFilters';
import { ActivatedRoute, Router } from '@angular/router';
import { ReportBaseComponent } from '../reportBase.component';
import { find, map, flatMap, filter } from 'rxjs/operators';

@Component({
  selector: 'app-kudos-by-team',
  templateUrl: './kudos-by-team.component.html',
  styleUrls: ['./kudos-by-team.component.scss']
})
export class KudosByTeamComponent extends ReportBaseComponent implements OnInit {

  kudosData$: Observable<any>;
  subviewData$: Observable<any>;
  history: any;

  constructor(private kudosService: KudosService, 
              router: Router, 
              activatedRoute: ActivatedRoute) {
    super(router, activatedRoute)
  }

  onReportNavigated(data: any){
    this.updateDataset(this.filter);
  }
  
  ngOnInit(): void { }

  updateDataset(val: ReportFilters) {
    this.kudosData$ = of(val).pipe(
      filter(f=>f.subFilter.visible == false), 
      flatMap(f=>this.kudosService.getKudosReportData(f.selectedTeam.value, f.selectedKudosType.value, f.dateRange))
    );
    let subReport = of(val).pipe(filter(f => f.subFilter.visible == true));
    this.subviewData$ = merge(
      subReport.pipe(
        filter(f=>f.subFilter.detailReportType == 'received'), 
        flatMap(f => this.kudosService.getReceivedKudosByUserReportData(
          f.subFilter.data.badgeId, 
          f.selectedKudosType.value, 
          f.dateRange))
      ),
      subReport.pipe(
        filter(f=>f.subFilter.detailReportType == 'sent'), 
        flatMap(f => this.kudosService.getSentKudosByUserReportData(
          f.subFilter.data.badgeId, 
          f.selectedKudosType.value, 
          f.dateRange))
      ));
  }
  //todo: fix data type from any to eplicit data type 
  trackById(index: number, data: any): number {
    return data ? data.employee.badgeId : 0;
  }
  
  //TODO: convert employee:any to Object type 
  openReceiveSubView(employee: any) {
    this.filter.subFilter = {
      visible: true,
      detailReportType: 'received',
      data: employee
    }
    this.reloadPage();
  }

  openSendSubView(employee: any) {
    this.filter.subFilter = {
      visible: true,
      detailReportType: 'sent',
      data: employee
    }    
    this.reloadPage();
  }
}

