import { Component, OnInit, Output } from '@angular/core';
import * as _ from 'underscore';
import { Observable, of } from 'rxjs';
import { ReportBaseComponent } from '@app/pages/report/reportBase.component';
import { ActivatedRoute, Router, NavigationExtras } from '@angular/router';
import { SelectFilter } from "@app/_models/SelectFilter";
import { ListOfDummyTeams, ListOfDummyTypes } from '@app/_models/dummies';

@Component({
  selector: 'app-kudos-by-team-filter',
  templateUrl: './kudos-by-team-filter.component.html',
  styleUrls: ['./kudos-by-team-filter.component.scss']
})
export class KudosByTeamFilterComponent extends ReportBaseComponent implements OnInit {

  listOfTypes: SelectFilter[] = ListOfDummyTypes;
  listOfTeams:SelectFilter[] = ListOfDummyTeams;
  
  constructor(activedRouter: ActivatedRoute, router : Router) { 
    super(router, activedRouter)  
    this.initialDefaultFilters();
  }

  onReportNavigated(){
    console.log(this.filters);
  }

  ngOnInit(): void { }
  
  initialDefaultFilters(){
    this.filters.selectedTeam = this.filters.selectedTeam || _.first(this.listOfTeams);
    this.filters.selectedKudosType = this.filters.selectedKudosType || _.first(this.listOfTypes);
    this.reloadPage();
  }
  
  onFilterChanged() {
    this.reloadPage();
  }
}