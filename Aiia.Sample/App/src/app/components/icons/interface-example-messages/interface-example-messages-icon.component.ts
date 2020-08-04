import {
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  HostBinding,
  Input,
  ViewEncapsulation
} from '@angular/core';
import { interfaceExampleMessagesIcon } from './interface-example-messages-icon';

@Component({
  selector: 'app-interface-example-messages-icon',
  template: `
    <ng-content></ng-content>
  `,
  styleUrls: ['./interface-example-messages-icon.component.scss'],
  styles: ['.interface-example-messages-icon svg{width: 496px; height: 419px}'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class InterfaceExampleMessagesIconComponent {
  @HostBinding('class.interface-example-messages-icon') true: boolean;

  @Input()
  set name(iconName: string) {
    this.element.nativeElement.innerHTML = interfaceExampleMessagesIcon[iconName] || null;
  }

  constructor(private element: ElementRef) {}
}
