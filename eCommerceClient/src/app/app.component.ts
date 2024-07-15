import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import {
  CustomToastrService,
  ToastrMessageType,
  ToastrPosition,
} from './services/ui/custom-toastr.service';
import { AuthService } from './services/common/auth.service';
import { Router } from '@angular/router';
import { HttpClientService } from './services/common/http-client.service';
declare var $: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  constructor(public authService: AuthService, private toastrService: CustomToastrService, private router: Router,
    private httpClientService: HttpClientService
  ) {

    // httpClientService.put({
    //   controller: "baskets"
    // }, {
    //   basketItemId:"dd766881-1d87-4e33-293e-08dca022ee31",
    //   quantity: 30
    // }).subscribe(data => {
    //   debugger
    // });





    authService.identityCheck();
  }

  signOut() {
    localStorage.removeItem('accessToken');
    this.authService.identityCheck();
    this.router.navigate([""])
    this.toastrService.message('You have been logged off', 'Logged off', {
      messageType: ToastrMessageType.Warning,
      position: ToastrPosition.TopRight,
    });
  }
}
