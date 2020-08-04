import {
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  HostBinding,
  Input,
  ViewEncapsulation
} from '@angular/core';
import { quoteIcon } from './quote-icon';

@Component({
  selector: 'app-quote-icon',
  template: `
    <ng-content></ng-content>
  `,
  styleUrls: ['./quote-icon.component.scss'],
  styles: ['.quote-icon svg{width: 40px; height: 40px}'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class QuoteIconComponent {
  @HostBinding('class.quote-icon') true: boolean;

  @Input()
  set name(iconName: string) {
    this.element.nativeElement.innerHTML = quoteIcon[iconName] || null;
  }

  constructor(private element: ElementRef) {}
}
