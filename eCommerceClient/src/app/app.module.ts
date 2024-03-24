import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser';
import { AdminModule } from './admin/admin.module';
import { UiModule } from './ui/ui.module';



@NgModule({
  declarations: [
    // AppComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AdminModule,
    UiModule
  ],
  providers: [],
  bootstrap: []
})
export class AppModule { }
