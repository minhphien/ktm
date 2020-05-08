import { PipeTransform, Pipe } from '@angular/core';
import * as _ from "underscore";
@Pipe({ name: 'appr' })
export class AppreviationPipe implements PipeTransform {
    transform(value: string): string {
        let chrs = _.map(value.split(" "),_.first);
        return `${_.first(chrs)}${_.last(chrs)}`;
    }
}

