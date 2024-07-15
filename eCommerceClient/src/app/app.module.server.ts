import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';

import { AppModule } from './app.module';
import { AppComponent } from './app.component';
import { UiModule } from './ui/ui.module';

@NgModule({
  imports: [
    AppModule,
    ServerModule,
    UiModule
  ],
  bootstrap: [AppComponent],
})
export class AppServerModule {}
