import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationEndpointService {

  constructor(private httpClientService: HttpClientService) { }

  async assigRoleEndpoint(roles: string[], code: string, menu: string, successCallBack?: () => void,
    errorCallBack?: (error) => void) {
    const observable: any =  this.httpClientService.post({
      controller: "AuthorizationEndpoints",
    }, {
      roles: roles,
      code: code,
      menu: menu
    })

    const promisData = observable.subscribe({
      next: successCallBack,
      error: errorCallBack
    });

    await promisData;
  }
}
