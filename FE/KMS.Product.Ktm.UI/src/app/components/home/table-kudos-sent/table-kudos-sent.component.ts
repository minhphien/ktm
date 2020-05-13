import { Component, OnInit, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { Kudos } from '@app/_models';

@Component({
  selector: 'app-table-kudos-sent',
  templateUrl: './table-kudos-sent.component.html',
  styleUrls: ['./table-kudos-sent.component.less']
})
export class TableKudosSentComponent implements OnInit {
  @Input() kudosData: Observable<Kudos[]>;
  constructor() { }

  ngOnInit() {
    
  }
  
  trackById(data: Kudos): number {
    return data ? data.id : 0;
  }
}
