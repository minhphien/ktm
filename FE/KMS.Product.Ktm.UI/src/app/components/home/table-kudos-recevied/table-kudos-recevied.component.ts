import { Component, OnInit, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { Kudos } from '@app/_models';

@Component({
  selector: 'app-table-kudos-received',
  templateUrl: './table-kudos-recevied.component.html',
  styleUrls: ['./table-kudos-recevied.component.less']
})
export class TableKudosReceivedComponent implements OnInit {
  @Input() kudosData: Observable<Kudos[]>;
  constructor() { }

  ngOnInit() { }
  
  trackById(data: Kudos): number {
    return data ? data.id : 0;
  }
}