import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Search {
  

  //make subscribe
  private search=new BehaviorSubject<string>('');
  $search=this.search.asObservable();

  setSearch(value:string)
  {
    this.search.next(value);
  }
}
