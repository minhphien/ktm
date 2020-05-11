import { Injectable, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import {Http, Response} from '@angular/http';
import * as $ from "jquery";
import {Observable} from 'rxjs/Rx';

@Injectable()
export class AppService {
    constructor(
        @Inject(DOCUMENT)
        private _document: HTMLDocument, private http: Http) {}
    
    setAppFavicon(id: string, basepath: string, icon: string) {
        $("#appFavicon").attr('href', basepath + "/" + icon);
    }
}
