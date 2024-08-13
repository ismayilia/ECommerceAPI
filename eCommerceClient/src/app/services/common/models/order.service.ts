import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Create_Order } from '../../../contracts/order/create_order';
import { firstValueFrom, Observable } from 'rxjs';
import { List_Order } from '../../../contracts/order/list_order';
import { SingleOrder } from '../../../contracts/order/single_order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private httpClientService: HttpClientService) { }

  async create(order: Create_Order): Promise<void> {
    const observable: Observable<any> = this.httpClientService.post({
      controller: "orders"
    }, order);

    await firstValueFrom(observable);
  }


  async getAllOrders(page: number = 0, size: number = 5, successCallBack?: () => void,
    errorCallBack?: (errorMessage: string) => void): Promise<{ totalOrderCount: number; orders: List_Order[] }> {
    const observable: Observable<{ totalOrderCount: number; orders: List_Order[] }> = this.httpClientService.get({
      controller: "orders",
      queryString: `page=${page}&size=${size}`
    });

    const promiseData = firstValueFrom(observable)
    promiseData.then(value => successCallBack())
      .catch(error => errorCallBack(error));

    return await promiseData;
  }

  async getOrderById(id: string, successCallBack?: () => void, errorCallBack?: (errorMessage: string) => void) {
    const observable: Observable<SingleOrder> = this.httpClientService.get<SingleOrder>({
      controller: "orders",
    }, id);

    const promiseData = firstValueFrom(observable)
    promiseData.then(value => successCallBack())
      .catch(error => errorCallBack(error));

    return await promiseData;
  }

  async completeOrder(id: string) {
    const observable: Observable<any> = this.httpClientService.get({
      controller: "orders",
      action: "complete-order"
    },id);

    await firstValueFrom(observable);
    
  }
}
