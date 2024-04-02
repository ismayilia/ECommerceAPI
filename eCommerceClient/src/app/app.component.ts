import { Component, OnInit } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from './services/ui/custom-toastr.service';
import { BrowserModule } from '@angular/platform-browser';
import {NgxSpinnerModule } from 'ngx-spinner';
declare var $: any;



@Component({
  selector: 'app-root',
  standalone:true,
  imports:[RouterOutlet,RouterModule,CommonModule,NgxSpinnerModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent{

  title = 'eCommerceClient';

  constructor(){
    
    
  }

}




