import { Component, OnInit } from '@angular/core';
import { SelectFilter } from '@app/_models/SelectFilter';
import { ListOfDummyTeams } from '@app/_models/dummies';
import { ReportBaseComponent } from '../reportBase.component';
import { Router, ActivatedRoute } from '@angular/router';
import { ReportFilters } from '@app/_models/ReportFilters';
import { Observable, of, from } from 'rxjs';
import { KudosService } from '@app/_services';
import * as _ from 'underscore';
import { filter, flatMap, takeLast } from 'rxjs/operators';
import { Team } from '@app/_models/kudosType';

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
    this.viewData$ = of(val).pipe(
      filter(f=>f.subFilter.visible == false), 
      flatMap(f=>this.kudosService.getKudosAcrossTeamReportData(
        f.selectedKudosType.value, 
        f.dateRange,
        _.map(val.selectedTeams, x=>x.value))
    ));
  }

  navigateToTeam(data: any) {
    this.listOfTeams$.pipe(takeLast(1)).subscribe(listOfTeam=>{
      this.filter.selectedTeam = _.find(listOfTeam,x => x.value == data.team.teamId);
      this.navigateToUrl("/report/kudos-by-team")
    });
  }

  ngOnInit(): void { }

  trackById(data: any): number {
    return data ? data.teamName : 0;
  }

}
