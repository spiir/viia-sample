import {
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  HostBinding,
  Input,
  ViewEncapsulation,
} from '@angular/core';
import { angleSwitchIcon } from './angle-switch-icon';

@Component({
  selector: 'app-angle-switch-icon',
  template: ` <ng-content></ng-content> `,
  styleUrls: ['./angle-switch-icon.component.scss'],
  styles: ['.angle-switch-icon svg{width: 73px; height: 73px}'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AngleSwitchIconComponent {
  @HostBinding('class.angle-switch-icon') true: boolean;

  @Input()
  set name(iconName: string) {
    this.element.nativeElement.innerHTML = angleSwitchIcon[iconName] || null;
  }

  constructor(private element: ElementRef) {}
}
