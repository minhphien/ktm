import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-avatar',
  templateUrl: './avatar.component.html',
  styleUrls: ['./avatar.component.scss']
})
export class AvatarComponent implements OnInit {
  @Input() displayName?: string = "";
  @Input() imgUrl?: string = "";
  @Input() size?: number = 32;

  constructor() { }

  ngOnInit(): void {
  }

}
