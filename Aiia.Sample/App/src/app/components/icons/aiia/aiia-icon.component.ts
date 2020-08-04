import {
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  HostBinding,
  Input,
  ViewEncapsulation
} from '@angular/core';
import { aiiaIcon } from './aiia-icon';

@Component({
  selector: 'app-aiia-icon',
  template: `
    <ng-content></ng-content>
  `,
  styleUrls: ['./aiia-icon.component.scss'],
  styles: ['.aiia-icon svg{width: 73px; height: 37px}'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AiiaIconComponent {
  @HostBinding('class.aiia-icon') true: boolean;

  @Input()
  set name(iconName: string) {
    this.element.nativeElement.innerHTML = aiiaIcon[iconName] || null;
  }

  constructor(private element: ElementRef) {}
}
