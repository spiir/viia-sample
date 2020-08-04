import {
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  HostBinding,
  Input,
  ViewEncapsulation
} from '@angular/core';
import { chevronIcon } from './chevron-icon';

@Component({
  selector: 'app-chevron-icon',
  template: `
    <ng-content></ng-content>
  `,
  styleUrls: ['./chevron-icon.component.scss'],
  styles: ['.chevron-icon svg{width: 13px; height: 8px}'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ChevronIconComponent {
  @HostBinding('class.chevron-icon') true: boolean;

  @Input()
  set name(iconName: string) {
    this.element.nativeElement.innerHTML = chevronIcon[iconName] || null;
  }

  constructor(private element: ElementRef) {}
}
