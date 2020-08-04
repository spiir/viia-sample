import {
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  HostBinding,
  Input,
  ViewEncapsulation,
} from '@angular/core';
import { linkedinIcon } from './linkedin-icon';

@Component({
  selector: 'app-linkedin-icon',
  template: ` <ng-content></ng-content> `,
  styleUrls: ['./linkedin-icon.component.scss'],
  styles: ['.linkedin-icon svg{width: 32px; height: 32px}'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LinkedinIconComponent {
  @HostBinding('class.linkedin-icon') true: boolean;

  @Input()
  set name(iconName: string) {
    this.element.nativeElement.innerHTML = linkedinIcon[iconName] || null;
  }

  constructor(private element: ElementRef) {}
}
