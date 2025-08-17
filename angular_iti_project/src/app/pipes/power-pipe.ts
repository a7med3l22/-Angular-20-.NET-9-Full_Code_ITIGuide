import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'power'
})
export class PowerPipe implements PipeTransform {

  transform(value: number,exponent:number=2): number {
    return Math.pow(value,exponent);
  }

}
