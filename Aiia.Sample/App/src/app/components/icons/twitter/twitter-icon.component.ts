import {
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  HostBinding,
  Input,
  ViewEncapsulation,
} from '@angular/core';
import { twitterIcon } from './twitter-icon';

@Component({
  selector: 'app-twitter-icon',
  template: ` <ng-content></ng-content> `,
  styleUrls: ['./twitter-icon.component.scss'],
  styles: ['.twitter-icon svg{width: 32px; height: 32px}'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TwitterIconComponent {
  @HostBinding('class.twitter-icon') true: boolean;

  @Input()
  set name(iconName: string) {
    this.element.nativeElement.innerHTML = twitterIcon[iconName] || null;
  }

  constructor(private element: ElementRef) {}
}
