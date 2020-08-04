import { Component } from '@angular/core';
import {
  trigger,
  transition,
  style,
  animate,
  state,
} from '@angular/animations';
import { GlobalSpinnerService } from './global-spinner-intl';

@Component({
  selector: 'app-global-spinner',
  templateUrl: './global-spinner.component.html',
  animations: [
    trigger('fadeOut', [
      transition(':leave', [
        // :enter is alias to 'void => *'
        state('in', style({ opacity: 1 })),
        animate('200ms ease-in', style({ opacity: 0 })),
      ]),
    ]),
  ],
})
export class GlobalSpinnerComponent {
  active$ = this.globalSpinnerService.active$;

  constructor(private globalSpinnerService: GlobalSpinnerService) {}
}
