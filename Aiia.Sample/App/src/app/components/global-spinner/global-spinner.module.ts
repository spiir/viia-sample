import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SpinnerModule } from '../spinner/spinner.module';
import { GlobalSpinnerComponent } from './global-spinner.component';
import { GlobalSpinnerService } from './global-spinner-intl';

@NgModule({
  declarations: [GlobalSpinnerComponent],
  imports: [CommonModule, SpinnerModule],
  exports: [GlobalSpinnerComponent],
  providers: [GlobalSpinnerService]
})
export class GlobalSpinnerModule {}
