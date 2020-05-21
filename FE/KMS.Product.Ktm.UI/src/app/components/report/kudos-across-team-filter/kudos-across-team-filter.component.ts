import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ReportBaseComponent } from '@app/pages/report/reportBase.component';
import { SelectFilter } from '@app/_models/SelectFilter';
import { ListOfDummyTeams, ListOfDummyTypes } from '@app/_models/dummies';
import * as _ from 'underscore';

@Component({
  selector: 'app-kudos-across-team-filter',
  templateUrl: './kudos-across-team-filter.component.html',
  styleUrls: ['./kudos-across-team-filter.component.scss']
})
export class KudosAcrossTeamFilterComponent extends ReportBaseComponent implements OnInit {

  listOfTypes: SelectFilter[] = ListOfDummyTypes;
  listOfTeams: SelectFilter[] = ListOfDummyTeams;
  
  constructor(protected router: Router, activatedRoute: ActivatedRoute) { super(router, activatedRoute) }

  ngOnInit(): void {
    this.initialDefaultFilters();
  }

  initialDefaultFilters(){
    if (this.filter && ! this.filter.selectedKudosType){
      this.filter.selectedKudosType = _.first(this.listOfTypes);
      this.reloadPage();
    }
  }

  onFilterChanged() {
    this.reloadPage();
  }
}
