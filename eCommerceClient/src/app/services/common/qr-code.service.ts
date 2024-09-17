import { Injectable } from '@angular/core';
import { HttpClientService } from './http-client.service';
import { firstValueFrom, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class QrCodeService {

  constructor(private httpClientService: HttpClientService) { }

  async generateQRCode(productId: string) {
    // blob file ve shekil ishledilen bir tipdir, byte esasen
    const observable: Observable<Blob> = this.httpClientService.get({
      controller: "products",
      action: "qrcode",
      responseType: 'blob'
    },productId)

    return await firstValueFrom(observable);
  }
}