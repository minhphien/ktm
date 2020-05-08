import { PipeTransform, Pipe } from '@angular/core';
import * as _ from 'underscore';
@Pipe({ name: 'emojicss' })
export class EmojiCssPipe implements PipeTransform {
    transform(value: string): string {
        try{
         return _.map(value.match(/:\w+:/g),(val)=>'tw-32 tw-' + val.substr(1,val.length-2)).reduce((x,y)=>x + ' '+ y);
        }catch(e){
            return "hamburger";
        }
    }
}
