import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ReportBaseComponent } from '@app/pages/report/reportBase.component';
import { SelectFilter } from '@app/_models/SelectFilter';
import { ListOfDummyTeams, ListOfDummyTypes } from '@app/_models/dummies';
import * as _ from 'underscore';
import { ReportService } from '@app/_services';
import { Observable, forkJoin } from 'rxjs';

@Component({
  selector: 'app-kudos-across-team-filter',
  templateUrl: './kudos-across-team-filter.component.html',
  styleUrls: ['./kudos-across-team-filter.component.scss']
})
export class KudosAcrossTeamFilterComponent extends ReportBaseComponent implements OnInit {

  // listOfTypes$: Observable<SelectFilter[]>;
  // listOfTeams$: Observable<SelectFilter[]>;
  
  constructor(protected router: Router, activatedRoute: ActivatedRoute, private reportService: ReportService) { 
    super(router, activatedRoute) 
    this.populateFilterData();
  }

  ngOnInit(): void {
    
  }

  populateFilterData(){
    this.listOfTypes$ = this.reportService.getAllKudosTypes();
    this.listOfTeams$ = this.reportService.getAllTeams()
    this.initialDefaultFilters();
  }

  onFilterChanged() {
    this.reloadPage();
  }
}
