import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { firstValueFrom, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationEndpointService {

  constructor(private httpClientService: HttpClientService) { }

  async assigRoleEndpoint(roles: string[], code: string, menu: string, successCallBack?: () => void,
    errorCallBack?: (error) => void) {
    const observable: Observable<any> = this.httpClientService.post({
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

  async getRolesToEndpoint(code: string, menu: string, successCallBack?: () => void,
    errorCallBack?: (error) => void): Promise<string[]>{
    const observable: Observable<any> = this.httpClientService.post({
      controller: "AuthorizationEndpoints",
      action: "GetRolesToEndpoint"
    }, {
      code: code,
      menu: menu
    });

    const promisData = firstValueFrom(observable);
    promisData.then(successCallBack)
      .catch(errorCallBack);

    return (await promisData).roles;
  }
}
