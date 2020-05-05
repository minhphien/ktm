import { Component, OnInit, Output, EventEmitter } from '@angular/core';
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
  userSuggestion$: Observable<string[]>;
  selectedUsers: string[] = [];
  suggestions: string[] = [];

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

  updateSuggestions(value: string): void {
    if (value.length>3) { 
      let keyword = value.replace(/@/g, '');
      console.log('getting suggested list for', value, keyword);
      this.userSuggestion$ = this.userService.getSuggestedUserList(keyword, 10);
      this.userSuggestion$.subscribe(users=>{
        console.log('suggestions updated',users);
        this.suggestions = users;
      });
    }
  }

  renderPreView(val: any): void {
    console.log('renderPreView ', val);
    if (this.inputValue) {
      const regex = this.getRegExp('@');
      const previewValue = this.inputValue.replace(
        regex,
        match => {
          this.updateSuggestions(match);
          return `${match}`;
        }
      );
    }
  }

  searchChange(): void {
    console.log('searchChange');
  }
  
  getRegExp(prefix: string | string[]): RegExp {
    const prefixArray = Array.isArray(prefix) ? prefix : [prefix];
    let prefixToken = prefixArray.join('').replace(/(\$|\^)/g, '\\$1');

    if (prefixArray.length > 1) {
      prefixToken = `[${prefixToken}]`;
    }

    return new RegExp(`(\\s|^)(${prefixToken})[^\\s]*`, 'g');
  }

  onSelect(suggestion: string): void {
    console.log(`onSelect @${suggestion}`);
    
   // this.inputValue = this.inputValue.replace(/@/g,``);

    if (this.inputValue) {
      this.inputValue = this.inputValue.replace(
        this.getRegExp('@'),
        match => {
          return `u/<{>${match.substr(1)}>`;
        }
      );
    }
  }

  onSuggestionMatched(val: any): void {
    console.log('onSuggestionMatched', val);
    
  }
}
