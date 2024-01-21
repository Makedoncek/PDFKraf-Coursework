// file-size.pipe.ts

import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'toMegabytes'
})
export class FileSizePipe implements PipeTransform {

  transform(value: number): string {
    const fileSizeInMB = (value / (1024 * 1024)).toFixed(3);
    return `${fileSizeInMB} MB`;
  }
}
