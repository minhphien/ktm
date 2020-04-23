import { Component, OnInit } from '@angular/core';
import { KudosService } from '@app/_services/kudos.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.less']
})
export class TableComponent implements OnInit {
  kudosItems$: Observable<any>;
  listOfData = [
    {
      key: '1',
      name: 'John Brown',
      age: 32,
      address: 'New York No. 1 Lake Park'
    },
    {
      key: '2',
      name: 'Jim Green',
      age: 42, 
      address: 'London No. 1 Lake Park'
    },
    {
      key: '3',
      name: 'Joe Black',
      age: 32,
      address: 'Sidney No. 1 Lake Park'
    }
  ];
  constructor(private kudosService: KudosService) { }

  ngOnInit() {
    this.kudosItems$ = this.kudosService.getMyKudos();
    this.kudosItems$.subscribe((data)=>{
      console.log(data);
    });
  }

}
