import {
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  HostBinding,
  Input,
  ViewEncapsulation
} from '@angular/core';
import { heroSwitchIcon } from './hero-switch-icon';

@Component({
  selector: 'app-hero-switch-icon',
  template: `
    <ng-content></ng-content>
  `,
  styleUrls: ['./hero-switch-icon.component.scss'],
  styles: ['.hero-switch-icon svg{width: 116px; height: 60px}'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HeroSwitchIconComponent {
  @HostBinding('class.hero-switch-icon') true: boolean;

  @Input()
  set name(iconName: string) {
    this.element.nativeElement.innerHTML = heroSwitchIcon[iconName] || null;
  }

  constructor(private element: ElementRef) {}
}
