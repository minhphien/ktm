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

@Component({
  selector: 'app-create-kudos',
  templateUrl: './create-kudos.component.html',
  styleUrls: ['./create-kudos.component.less']
})
export class CreateKudosComponent implements OnInit {

  visible = false;
  profileInfo$: Observable<Employee>;
  userSuggestion$: Observable<any[]>;
  selectedUsers: string[] = [];
  suggestions: string[] = [];

  constructor(private userService: UserService, private kudosService: KudosService, private message: NzMessageService) { }

  ngOnInit() {
    this.profileInfo$ = this.userService.getUserCurrentState();
  }

  createKudos(): void {
     console.log(this.content);
    // console.log(
      $(this.content).children("span[class='mention']").each(
        (obj:any)=>{ 
          console.log(obj);
          // console.log($(obj).attr("data-username"));
        });

    // let username =
    //   of(_.toArray<string>(_.each($(this.content).children("span[class='mention']"),
    //     (x:any) => $(x).attr('data-username')))).pipe(mergeAll());
    //   username.subscribe(x=>{
    //     console.log(x);
    //   })
    //   // username$.pipe(
      //     mergeMap((username: string) => { 
      //       let data =
      //       <LightKudos> {
      //         ReceiverUsername: username,
      //         Content: this.content,
      //         SlackEmoji: ":clap:",
      //         KudoTypeId: 1
      //       };
      //       let reponse = this.kudosService.createKudos(data);
      //       return reponse;
      //     }))
      // .subscribe(response=>{
      // //this.kudosService.getMyKudos();
      // this.content = '';
      // this.message.success('Kudos!!! Your message(s) is sent.', {
      //   nzDuration: 1500
      // });
    // });

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
