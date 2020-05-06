import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { User } from '@app/_models/user';
import { Employee } from "@app/_models/employee";
import { Observable, of, from } from 'rxjs';
import { SESSION_USER_INFO } from '@app/appState.reducer';
import { map } from 'rxjs/operators';
import * as _ from 'underscore';

@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient) { }

    getAllUsers(): Observable<User[]> {
        return this.http.get<User[]>(`${environment.apiUrl}/users`);
    }

    getUserById(id: number):Observable<User> {
        return this.http.get<User>(`${environment.apiUrl}/users/${id}`);
    }
    
    setUserInfoSession(userInfo: User){
        console.log('updating session.', userInfo);
        sessionStorage.setItem(SESSION_USER_INFO, JSON.stringify(userInfo));
    }

    getUserCurrentState() : Observable<Employee>{
        let url = `${environment.hrmUrls.domain}${environment.hrmUrls.methods.ReturnInfoUserLogin}`;
        console.log(url);
        return this.http.get<Employee>(url)
        .pipe(map((employee:Employee)=>{
            if(employee){
                employee.imgUrl = `${environment.hrmUrls.domain}${environment.hrmUrls.methods.ReturnPhoto}/${employee.employeeId}/300`
            }
            return employee;
        }));
    }
    
    getUserInfoSession(): User{
        let userString = sessionStorage.getItem(SESSION_USER_INFO);
        if (userString && userString != 'undefined') {
            console.log('parsing');
            return <User>JSON.parse(userString);
        }
        else return null;
    }

    getSuggestedUserList(keyword: string, maxTotal?: number): Observable<any[]>{
        if (!keyword || keyword.length<3) return null;
        if (maxTotal<=0) maxTotal = 1;       
        const root = environment.hrmUrls;
        const url = `${root.domain}${root.methods.SuggestedUsers}/${maxTotal}?employeeId=0&exceptEmployee=&filterName=${keyword}&filterOnlyStatus=&includeTerminated=true`;
        let request$ = this.http.get<Employee[]>(url);
        request$.subscribe(val=>{console.log(val)});
        return request$.pipe(
                map((users: Employee[]) => _.map(
                    users, u => { 
                        return { 
                            'id': u.employeeNumber, 
                            'value': `${u.fullName} - ${u.employeeCode}`, 
                            'username': u.userAccount
                        }}))
                );
    }
}