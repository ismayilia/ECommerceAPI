import { Component, OnInit } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from './services/ui/custom-toastr.service';
import { BrowserModule } from '@angular/platform-browser';
declare var $: any;



@Component({
  selector: 'app-root',
  standalone:true,
  imports:[RouterOutlet,RouterModule,CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent{

  title = 'eCommerceClient';

  constructor(private toasterService: CustomToastrService){
    
    toasterService.message("salam", "Isi",{
      messageType:ToastrMessageType.Success,
      position:ToastrPosition.BottomCenter
    });
    toasterService.message("salam", "Isi",{
      messageType:ToastrMessageType.Error,
      position:ToastrPosition.BottomCenter
    });
  }

}




