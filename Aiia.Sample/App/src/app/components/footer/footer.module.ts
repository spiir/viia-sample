import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent } from './footer.component';
import { IconsModule } from '../icons/icons.module';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [FooterComponent],
  imports: [CommonModule, IconsModule, TranslateModule],
  exports: [FooterComponent, TranslateModule]
})
export class FooterModule {}
