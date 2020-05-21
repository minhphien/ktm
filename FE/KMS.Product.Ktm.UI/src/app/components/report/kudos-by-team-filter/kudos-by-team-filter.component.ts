import { Component, OnInit, Output } from '@angular/core';
import * as _ from 'underscore';
import { Observable, of, concat, forkJoin } from 'rxjs';
import { ReportBaseComponent } from '@app/pages/report/reportBase.component';
import { ActivatedRoute, Router, NavigationExtras } from '@angular/router';
import { SelectFilter } from "@app/_models/SelectFilter";
import { ListOfDummyTeams, ListOfDummyTypes } from '@app/_models/dummies';
import { KudosService, ReportService } from '@app/_services';
import { map, flatMap, takeLast, take, first, last, concatAll, combineAll } from 'rxjs/operators';
import { KudosType } from '@app/_models/kudosType';

@Component({
  selector: 'app-kudos-by-team-filter',
  templateUrl: './kudos-by-team-filter.component.html',
  styleUrls: ['./kudos-by-team-filter.component.scss']
})
export class KudosByTeamFilterComponent extends ReportBaseComponent implements OnInit {

  // listOfTypes$: Observable<SelectFilter[]>;
  // listOfTeams$: Observable<SelectFilter[]>;
  
  constructor(activedRouter: ActivatedRoute, router : Router, private reportService: ReportService) { 
    super(router, activedRouter) 
    this.populateFilterData(); 
    
  }

  onReportNavigated(){
    
  }

  ngOnInit(): void { }

  populateFilterData(){
    this.listOfTypes$ = this.reportService.getAllKudosTypes();
    this.listOfTeams$ = this.reportService.getAllTeams()
    this.initialDefaultFilters();
  }
  
  
  
  onFilterChanged() {
    this.reloadPage();
  }
}