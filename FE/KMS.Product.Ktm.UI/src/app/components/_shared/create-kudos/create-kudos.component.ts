import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-create-kudos',
  templateUrl: './create-kudos.component.html',
  styleUrls: ['./create-kudos.component.less']
})
export class CreateKudosComponent implements OnInit {
  visible = false;

  constructor() { }

  ngOnInit() {
  }

  open(): void {
    this.visible = true;
  }

  close(): void {
    this.visible = false;
  }
}
