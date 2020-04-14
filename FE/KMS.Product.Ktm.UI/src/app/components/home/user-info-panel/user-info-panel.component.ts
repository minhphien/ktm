import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-user-info-panel',
  templateUrl: './user-info-panel.component.html',
  styleUrls: ['./user-info-panel.component.less']
})
export class UserInfoPanelComponent implements OnInit {
  @Input() currentUser;
  
  constructor() { }

  ngOnInit() {
  }

}
