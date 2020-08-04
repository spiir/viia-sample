import { Component } from '@angular/core';
import { GlobalSpinnerService } from './components/global-spinner/global-spinner-intl';
import { NEVER } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  constructor(private readonly spinner: GlobalSpinnerService) {
    spinner.track(NEVER);
  }
}
