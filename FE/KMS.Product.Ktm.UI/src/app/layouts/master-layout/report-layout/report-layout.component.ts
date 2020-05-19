import { Component, OnInit } from '@angular/core';
import * as _ from 'underscore';
import { ReportBaseComponent } from '@app/pages/report/reportBase.component';
import { Router, ActivatedRoute } from '@angular/router';
import { SelectFilter } from "@app/_models/SelectFilter";

@Component({
  selector: 'app-report-layout',
  templateUrl: './report-layout.component.html',
  styleUrls: ['./report-layout.component.scss']
})
export class ReportLayoutComponent extends ReportBaseComponent implements OnInit {

  listOfReports: SelectFilter[] = [
    { name: "Sent/Received kudos by one Team", routeUrl: "/report/kudos-by-team",  value: "1", disabled: false },
    { name: "Sent/Received kudos across Teams", routeUrl: "/report/kudos-across-team", value: "2", disabled: false },
    { name: "Sent/Received kudos by users", routeUrl: "/report/kudos-by-user", value: "3", disabled: false }
  ];

  selectedReport: any = _.first(this.listOfReports);

  subFilter = {
    employee: null,
    visible: false,
    detailReportType: null      
  }


  constructor(private activatedRoute: ActivatedRoute, router: Router) { 
    super(router)
    this.activatedRoute.queryParams.subscribe(s=>{
      try {
        let routeFilters = history.state.data.filters;
        this.filters = routeFilters;
      } catch(e) {}
    })
  }

  onReportChanged(newVal: SelectFilter){
    this.router.navigateByUrl(newVal.routeUrl);
  }

  ngOnInit(): void { }

}
