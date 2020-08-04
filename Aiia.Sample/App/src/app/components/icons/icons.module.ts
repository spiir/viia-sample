import { NgModule } from '@angular/core';
import { AiiaIconComponent } from './aiia/aiia-icon.component';
import { LinkedinIconComponent } from './linkedin/linkedin-icon.component';
import { TwitterIconComponent } from './twitter/twitter-icon.component';
import { HeroSwitchIconComponent } from './hero-switch/hero-switch-icon.component';
import { AngleSwitchIconComponent } from './angle-switch/angle-switch-icon.component';
import { QuoteIconComponent } from './quote/quote-icon.component';
import { InterfaceExampleMessagesIconComponent } from './interface-example-messages/interface-example-messages-icon.component';
import { InterfaceExampleIconComponent } from './interface-example/interface-example-icon.component';

@NgModule({
  declarations: [
    TwitterIconComponent,
    LinkedinIconComponent,
    InterfaceExampleIconComponent,
    InterfaceExampleMessagesIconComponent,
    AiiaIconComponent,
    HeroSwitchIconComponent,
    AngleSwitchIconComponent,
    QuoteIconComponent,
  ],
  exports: [
    TwitterIconComponent,
    LinkedinIconComponent,
    InterfaceExampleIconComponent,
    InterfaceExampleMessagesIconComponent,
    AiiaIconComponent,
    HeroSwitchIconComponent,
    AngleSwitchIconComponent,
    QuoteIconComponent,
  ],
})
export class IconsModule {}
