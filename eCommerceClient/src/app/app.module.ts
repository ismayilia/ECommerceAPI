import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser';
import { AdminModule } from './admin/admin.module';
import { UiModule } from './ui/ui.module';
import { RouterModule } from '@angular/router'; // RouterModule'ı içe aktarın
import { routes } from './app.routes'; // Rotaları içe aktarın
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProductsComponent } from './ui/components/products/products.component';
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    RouterModule,
    ToastrModule.forRoot(),
    BrowserModule,
    BrowserAnimationsModule,
    
  ],
  providers: [],
  bootstrap: []
})

export class AppModule { }
