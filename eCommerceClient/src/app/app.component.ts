import { Component, OnInit } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from './services/ui/custom-toastr.service';
import { BrowserModule } from '@angular/platform-browser';
import {NgxSpinnerModule } from 'ngx-spinner';
import { data } from 'jquery';
import { HttpClientModule } from '@angular/common/http';
import { HttpClientService } from './services/common/http-client.service';
import { ProductService } from './services/common/modedls/product.service';
declare var $: any;



@Component({
  selector: 'app-root',
  standalone:true,
  imports:[RouterOutlet,RouterModule,CommonModule,NgxSpinnerModule,HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  providers:[ProductService,HttpClientService,
    {provide: "baseUrl", useValue: "https://localhost:7047/api", multi: true}
  ]
})
export class AppComponent{

  title = 'eCommerceClient';

  constructor(){}


  // ngOnInit(){
  //   $.get("https://localhost:7047/api/products/get", data =>{
  //     console.log(data);
  //   })
  // }

}





