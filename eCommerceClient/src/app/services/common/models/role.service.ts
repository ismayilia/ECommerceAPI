import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { firstValueFrom, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private httpClientService: HttpClientService) { }

  getRoles() {

  }

  async create(name: string, successCallBack?: () => void, errorCallBack?: (error) => void) {
    const observable: Observable<any> = this.httpClientService.post({
      controller: "roles",
    }, { name: name });

    return await firstValueFrom(observable) as { succeeded: boolean };
  }
}
