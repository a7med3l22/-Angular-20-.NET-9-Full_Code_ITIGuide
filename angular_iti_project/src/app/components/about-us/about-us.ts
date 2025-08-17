import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { selectCurrentLanguage } from '../../language/store/language.selectors';
import { AsyncPipe, CommonModule } from '@angular/common';
import { FormsModule } from '@angular/Forms';

@Component({
  selector: 'app-about-us',
  imports: [AsyncPipe,FormsModule,CommonModule],
  templateUrl: './about-us.html',
  styleUrl: './about-us.css'
})
export class AboutUs {

  /**
   *
   */
    currentLang$: Observable<string>;

  constructor(private store:Store) {
    this.currentLang$ = this.store.select(selectCurrentLanguage);
  }
}
