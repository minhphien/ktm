import { Output, Input, OnInit } from '@angular/core';
import { ReportFilters } from '@app/_models/ReportFilters';
import { NavigationExtras, Router, ActivatedRoute } from '@angular/router';
import * as _ from 'underscore';
import { ListOfReports } from '@app/_models/dummies';
import { SelectFilter } from '@app/_models/SelectFilter';
import { concat, of, Observable, forkJoin } from 'rxjs';

export abstract class ReportBaseComponent {
  compareFn = (o1: any, o2: any) => (o1 && o2 ? o1.value === o2.value : o1 === o2);
  
  private static Filters: ReportFilters = {
    selectedReport: null,
    selectedKudosType: null,
    selectedTeam: null,
    selectedTeams: [],
    dateRange: [],
    subFilter: null
  }
  private static ListOfTypes$: Observable<SelectFilter[]>;
  private static ListOfTeams$: Observable<SelectFilter[]>;

  get listOfTypes$() { return ReportBaseComponent.ListOfTypes$ }
  set listOfTypes$(value) { ReportBaseComponent.ListOfTypes$ = value }

  get listOfTeams$() { return ReportBaseComponent.ListOfTeams$ }
  set listOfTeams$(value) { ReportBaseComponent.ListOfTeams$ = value }

  get filter() { return ReportBaseComponent.Filters }
  set filter(value) { ReportBaseComponent.Filters = value }

  constructor(protected router: Router, protected activatedRoute: ActivatedRoute){
    this.filter.selectedReport = this.getSelectedReportFromRoute();
    this.filter.subFilter = this.initialSubFilter();
    activatedRoute.queryParams.subscribe(x=> {
      let extras = this.router.getCurrentNavigation().extras
      if (extras && extras.state) {
        if(extras.state.data){
          this.filter = history.state.data.filters;
        }
        this.onReportNavigated(this.router.getCurrentNavigation().extras.state.data)
      }
    });
  }

  onReportNavigated(data: any){ } //placeholder as an event handler after the report navigated
  
  initialSubFilter(): any {
    return {
      data: null,
      visible: false,
      detailReportType: null 
    }
  }

  getSelectedReportFromRoute(){
    return _.find(ListOfReports, (x:SelectFilter) => x.routeUrl == this.currentRoute());
  }

  currentRoute(): string {
    return this.router.url.split('?')[0];
  }

  navigateToUrl(url: string = this.currentRoute(), resetSubFilter: boolean = false){    
    if (resetSubFilter) this.filter.subFilter = this.initialSubFilter();
    let navigationExtras: NavigationExtras = {
      queryParams: { timeStamp: _.now()},
      state: {
        data: { filters: this.filter}
      }
    };
    this.router.navigate([url], navigationExtras);
  }
  
  reloadPage(){ this.navigateToUrl(); }

  initialDefaultFilters(){
    forkJoin({type: this.listOfTypes$, team: this.listOfTeams$})
      .subscribe( f => {
        this.filter.selectedKudosType = this.filter.selectedKudosType || _.first(f.type); 
        this.filter.selectedTeam = this.filter.selectedTeam || _.first(f.team); 
        this.reloadPage(); 
      });
  }
  
}