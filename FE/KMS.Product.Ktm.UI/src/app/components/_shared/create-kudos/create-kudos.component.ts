import { Component, OnInit } from '@angular/core';
import { UserService } from '@app/_services';
import { Observable } from 'rxjs';
import { Employee } from '@app/_models';

@Component({
  selector: 'app-create-kudos',
  templateUrl: './create-kudos.component.html',
  styleUrls: ['./create-kudos.component.less']
})
export class CreateKudosComponent implements OnInit {
  visible = false;
  profileInfo$: Observable<Employee>;

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.profileInfo$ = this.userService.getUserCurrentState();
  }

  open(): void {
    this.visible = true;
  }

  close(): void {
    this.visible = false;
  }

  inputValue: string = '';
  suggestions = ['afc163', 'benjycui', 'yiminghe', 'RaoHai', '中文', 'にほんご'];

  onChange(value: string): void {
    console.log(value);
  }

  onSelect(suggestion: string): void {
    console.log(`onSelect ${suggestion}`);
  }
}
