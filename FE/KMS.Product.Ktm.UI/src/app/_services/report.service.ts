import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { SelectFilter } from '@app/_models/SelectFilter';
import { map, retryWhen, flatMap, delay, take, concat } from 'rxjs/operators';
import { Observable, of, throwError } from 'rxjs';
import { KudosType, Team } from '@app/_models/kudosType';
import * as _ from 'underscore';

@Injectable({ providedIn: 'root' })
export class ReportService {
  
    constructor(private http: HttpClient) {  }

    getAllKudosTypes(): Observable<SelectFilter[]> {
        let url = `${environment.apiUrl}${environment.methods.AllKudosTypes}`;
        return this.http.get(url).pipe(map((x:KudosType[]) => 
            _.map(x,(item) => <SelectFilter>{ name: item.typeName, value: item.id.toString(), disabled: false }
        )));
    }

    getAllTeams(): Observable<SelectFilter[]> {
        let url = `${environment.apiUrl}${environment.methods.AllTeams}`;
        
        return this.http.get(url).pipe(map((x:Team[]) => 
            _.map(x,(item) => <SelectFilter>{ name: item.teamName, value: item.id.toString(), disabled: false }
        )));
    }
}