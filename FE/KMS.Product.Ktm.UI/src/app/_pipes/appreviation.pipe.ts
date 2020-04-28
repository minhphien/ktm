import { PipeTransform, Pipe } from '@angular/core';
import * as _ from "underscore";
@Pipe({ name: 'appr' })
export class AppreviationPipe implements PipeTransform {
    transform(value: string): string {
        return _.reduce(value.split(" "), (result: string, val: string) => result  + val[0], '');
    }
}

