import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';
import { User } from '@app/_models/user';
import { Employee } from "@app/_models/employee";
import { Observable } from 'rxjs';
import { SESSION_USER_INFO } from '@app/appState.reducer';
import { stringify } from 'querystring';
import { retry, map } from 'rxjs/operators';

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
        return this.http.get<Employee>(url).pipe(map((employee:Employee)=>{
            if(employee){
                employee.imgUrl = `${environment.hrmUrls.domain}${environment.hrmUrls.methods.ReturnPhoto}/${employee.employeeId}/300`
            }
            return employee;
        }) ,retry(3));
    }
    
    getUserInfoSession(): User{
        let userString = sessionStorage.getItem(SESSION_USER_INFO);
        if (userString && userString != 'undefined') {
            console.log('parsing');
            return <User>JSON.parse(userString);
        }
        else return null;
    }
}