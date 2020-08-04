import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { GlobalSpinnerModule } from './components/global-spinner/global-spinner.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FooterModule } from './components/footer/footer.module';
import {
  TranslateService,
  TranslateModule,
  TranslateLoader,
} from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}
export function i18nInitializerFn(i18n: TranslateService) {
  return () => {
    return new Promise((resolve) => {
      i18n.setDefaultLang('en');
      i18n.use(i18n.getBrowserLang());

      resolve();
    });
  };
}

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    GlobalSpinnerModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FooterModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: createTranslateLoader,
        deps: [HttpClient],
      },
      isolate: false,
    }),
  ],  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: i18nInitializerFn,
      multi: true,
      deps: [TranslateService],
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
