import { Component, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { UserService } from '@app/_services';
import { Observable, forkJoin, of, from } from 'rxjs';
import { Employee, LightKudos } from '@app/_models';
import { KudosService } from '@app/_services/kudos.service';
import { NzMessageService } from 'ng-zorro-antd/message';
import { QuillEditorComponent } from 'ngx-quill';
import 'quill-mention';
import * as _ from 'underscore';
import * as $ from 'jquery';
import { map, mergeMap, mergeAll } from 'rxjs/operators';
import { NzModalService } from 'ng-zorro-antd';

@Component({
  selector: 'app-create-kudos',
  templateUrl: './create-kudos.component.html',
  styleUrls: ['./create-kudos.component.scss']
})
export class CreateKudosComponent implements OnInit {

  visible = false;
  profileInfo$: Observable<Employee>;
  userSuggestion$: Observable<any[]>;
  selectedUsers: string[] = [];
  suggestions: string[] = [];

  constructor(private userService: UserService, private kudosService: KudosService, 
    private message: NzMessageService, private modal: NzModalService) { }

  ngOnInit() {
    this.profileInfo$ = this.userService.getUserCurrentState();
  }

  showMentionConfirm(): void {
    this.modal.warning({
      nzTitle: 'No recevier found',
      nzContent: 'Please mention @someone that you want to send the kudos in your message.',
    });
  }

  createKudos(): void {
    if (!this.content) return;
    let html = $(this.content)[0];
    let mentionList = $(html).children("span[class='mention']");
    if (mentionList.length == 0){
      this.showMentionConfirm()
      return;
    }
    forkJoin(of(new Observable(obs=>{
      mentionList.each((idx, val)=>{
          obs.next($(val).attr("data-username"));
        });
      }).pipe(
        map(async (username: string) =>  { 
          let data =
          <LightKudos> {
            ReceiverUsername: username,
            Content: this.content,
            SlackEmoji: ":clap:",
            KudoTypeId: 1
          };
          let reponse = await this.kudosService.createKudos(data).toPromise();
          return reponse;})
    ).toPromise())).toPromise().then((response: any) =>{
      this.content = '';
      this.message.success('Kudos!!! Your message(s) is sent.', {
        nzDuration: 1500
      });
      this.kudosService.getMyKudos();
    });
  }

  //
  // Rich editor
  //

  toolbarOptions = ['bold', 'italic', 'underline', 'strike'];

  @ViewChild(QuillEditorComponent, { static: true }) 
  editor: QuillEditorComponent
  
  content = ''
  
  modules = {
    mention: {
      allowedChars: /^[A-Za-z\sÅÄÖåäö]*$/,
      dataAttributes: ['id', 'value', 'denotationChar','username'],
      onSelect: (item, insertItem) => {
        const editor = this.editor.quillEditor
        insertItem(item)
        editor.insertText(editor.getLength() - 1, '', 'user')
      },
      source: (searchTerm, renderList) => {
        const values = [];

        if (searchTerm.length === 0) {
          renderList(values, searchTerm)
        } else {
          this.userSuggestion$ = this.userService.getSuggestedUserList(searchTerm, 10);
          this.userSuggestion$.subscribe(users=>{
            renderList(users, searchTerm);
          });
        }
      }
    },
    toolbar: null
  }

  //
  //

}
