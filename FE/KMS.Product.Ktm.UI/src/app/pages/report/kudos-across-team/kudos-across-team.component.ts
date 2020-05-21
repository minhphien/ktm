import { Component, OnInit } from '@angular/core';
import { SelectFilter } from '@app/_models/SelectFilter';
import { ListOfDummyTeams } from '@app/_models/dummies';
import { ReportBaseComponent } from '../reportBase.component';
import { Router, ActivatedRoute } from '@angular/router';
import { ReportFilters } from '@app/_models/ReportFilters';
import { Observable, of } from 'rxjs';
import { KudosService } from '@app/_services';
import * as _ from 'underscore';
import { filter, flatMap } from 'rxjs/operators';

@Component({
  selector: 'app-kudos-across-team',
  templateUrl: './kudos-across-team.component.html',
  styleUrls: ['./kudos-across-team.component.scss']
})
export class KudosAcrossTeamComponent extends ReportBaseComponent implements OnInit {
  viewData$: Observable<any>;
  
  constructor(private kudosService: KudosService, router: Router, activatedRoute: ActivatedRoute) {
    super(router, activatedRoute)
  }

  static count: number = 0;
  
  onReportNavigated(){
    this.updateDataset(this.filter);
  }

  updateDataset(val: ReportFilters){
    console.log('selected Team',_.map(val.selectedTeams, x=>x.value), val.selectedKudosType.value, val.dateRange);
    console.log('getKudosAcrossTeamReport', this.kudosService)
    this.viewData$ = of(val).pipe(
      filter(f=>f.subFilter.visible == false), 
      flatMap(f=>this.kudosService.getKudosAcrossTeamReportData(
        f.selectedKudosType.value, 
        f.dateRange,
        _.map(val.selectedTeams, x=>x.value))
    ));
  }

  navigateToTeam(data: any) {
    this.filter.selectedTeam = _.find(ListOfDummyTeams,x=>x.value == data.team.teamId);
    this.navigateToUrl("/report/kudos-by-team");
  }

  ngOnInit(): void { }

  trackById(data: any): number {
    return data ? data.teamName : 0;
  }

}
