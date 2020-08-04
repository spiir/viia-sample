import {
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  HostBinding,
  Input,
  ViewEncapsulation
} from '@angular/core';
import { interfaceExampleIcon } from './interface-example-icon';

@Component({
  selector: 'app-interface-example-icon',
  template: `
    <ng-content></ng-content>
  `,
  styleUrls: ['./interface-example-icon.component.scss'],
  styles: ['.interface-example-icon svg{width: 472px; height: 572px}'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class InterfaceExampleIconComponent {
  @HostBinding('class.interface-example-icon') true: boolean;

  @Input()
  set name(iconName: string) {
    this.element.nativeElement.innerHTML = interfaceExampleIcon[iconName] || null;
  }

  constructor(private element: ElementRef) {}
}
