
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';

const routes: Routes = [
    {
      path: '**',
      component: AppComponent,
      data: { error: 'route-not-found' },
    },
  ];

  @NgModule({
    imports: [
      RouterModule.forRoot(routes, {
        useHash: false,
        enableTracing: false,
      }),
    ],
    exports: [RouterModule],
  })
  export class AppRoutingModule {}
