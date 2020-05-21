import { Component, OnInit } from '@angular/core';
import * as _ from 'underscore';
import { ReportBaseComponent } from '@app/pages/report/reportBase.component';
import { Router, ActivatedRoute } from '@angular/router';
import { SelectFilter } from "@app/_models/SelectFilter";
import { ListOfReports } from '@app/_models/dummies';

@Component({
  selector: 'app-report-layout',
  templateUrl: './report-layout.component.html',
  styleUrls: ['./report-layout.component.scss']
})
export class ReportLayoutComponent extends ReportBaseComponent implements OnInit {

  listOfReports: SelectFilter[] =  ListOfReports;
  filters: any;
  constructor( activatedRoute: ActivatedRoute, router: Router) { 
    super(router, activatedRoute)
    this.filters = this.filter;
  }

  onReportNavigated(){
    this.filter.selectedReport = this.getSelectedReportFromRoute();
  }

  onReportChanged(newVal: SelectFilter){
    this.navigateToUrl(newVal.routeUrl, true);
  }

  ngOnInit(): void { 
    console.log('filter from layout',this.filter)    
  }

}
