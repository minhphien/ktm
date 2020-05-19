import { Component, OnInit } from '@angular/core';
import { SelectFilter } from '@app/_models/SelectFilter';
import { ListOfDummyTeams } from '@app/_models/dummies';
import { ReportBaseComponent } from '../reportBase.component';
import { Router, ActivatedRoute } from '@angular/router';
import { ReportFilters } from '@app/_models/ReportFilters';
import { Observable, of } from 'rxjs';
import { filter, flatMap } from 'rxjs/operators';
import { KudosService } from '@app/_services';
import * as _ from 'underscore';

@Component({
  selector: 'app-kudos-across-team',
  templateUrl: './kudos-across-team.component.html',
  styleUrls: ['./kudos-across-team.component.scss']
})
export class KudosAcrossTeamComponent extends ReportBaseComponent implements OnInit {
  viewData$: Observable<any>;
  constructor(router: Router, activatedRoute: ActivatedRoute, private kudosService: KudosService) {
    super(router, activatedRoute);
  }

  onReportNavigated(){
    this.updateDataset(this.filters);
  }

  updateDataset(val: ReportFilters){
    console.log('updateDataset',val);
    this.viewData$ = of(val).pipe(
      filter(f=>f.subFilter.visible == false), 
      flatMap(f=>this.kudosService.getKudosAcrossTeamReportData(_.map(val.selectedTeams, x=>x.value), f.selectedKudosType.value, f.dateRange))
    );
    this.viewData$.subscribe(val=>console.log(val));
  }

  navigateToTeam(data: any) {
    this.filters.selectedTeam = _.find(ListOfDummyTeams,x=>x.value == data.team.teamId);
    this.navigateToUrl("/report/kudos-by-team");
  }

  ngOnInit(): void { }

  trackById(data: any): number {
    return data ? data.teamName : 0;
  }

}
