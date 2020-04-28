import { Component, OnInit } from '@angular/core';
import { UserService } from '@app/_services';
import { Observable } from 'rxjs';
import { Employee, LightKudos } from '@app/_models';
import { KudosService } from '@app/_services/kudos.service';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-create-kudos',
  templateUrl: './create-kudos.component.html',
  styleUrls: ['./create-kudos.component.less']
})
export class CreateKudosComponent implements OnInit {
  visible = false;
  profileInfo$: Observable<Employee>;

  constructor(private userService: UserService, private kudosService: KudosService, private message: NzMessageService) { }

  ngOnInit() {
    this.profileInfo$ = this.userService.getUserCurrentState();
  }

  createKudos(): void {
    let data: LightKudos = {
      ReceiverUsername: "phienle",
      Content: this.inputValue,
      SlackEmoji: ":clap:",
      KudoTypeId: 1
    };
    this.kudosService.createKudos(data).subscribe(response=>{
      this.kudosService.getMyKudos();
      this.cleanUpModel();
      this.message.success('Kudos. Your message is sent.', {
        nzDuration: 1500
      });
    })
  }

  cleanUpModel() {
    this.inputValue = '';
  }

  inputValue: string = '';
  suggestions = ['minhphien','phienle'];

  onChange(value: string): void {
    console.log(value);
  }

  onSelect(suggestion: string): void {
    console.log(`onSelect ${suggestion}`);
  }
}
