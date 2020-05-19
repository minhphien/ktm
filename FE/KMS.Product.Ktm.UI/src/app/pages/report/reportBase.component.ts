import { Output, Input } from '@angular/core';
import { ReportFilters } from '@app/_models/ReportFilters';
import { NavigationExtras, Router } from '@angular/router';
import * as _ from 'underscore';
import { stringify } from 'querystring';

export abstract class ReportBaseComponent {
  compareFn = (o1: any, o2: any) => (o1 && o2 ? o1.value === o2.value : o1 === o2);
  
  filters: ReportFilters = {
    selectedKudosType: null,
    selectedTeam: null,
    dateRange: [],
    subFilter: this.initialSubFilter()
  }

  constructor(protected router: Router) { }
  
  initialSubFilter(): any {
    return {
      data: null,
      visible: false,
      detailReportType: null 
    }
  }

  navigateToUrl(url: string = this.router.url.split('?')[0], resetSubFilter: boolean = false){
    if (resetSubFilter) this.filters.subFilter = this.initialSubFilter();
    let navigationExtras: NavigationExtras = {
      queryParams: { timeStamp: _.now()},
      state: {
        data: { filters: this.filters}
      }
    };
    this.router.navigate([url], navigationExtras);
  }
  
  reloadPage(){ this.navigateToUrl(); }
}