import { Component, OnInit } from '@angular/core';
import { BaseComponent, SpinnerType } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { HttpClientService } from '../../../services/common/http-client.service';
import { Product } from '../../../contracts/product';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [],
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
export class ProductsComponent extends BaseComponent implements OnInit {

  constructor(spinner: NgxSpinnerService, private httpClientService: HttpClientService) {
    super(spinner)
  }

  ngOnInit() {
    this.showSpinner(SpinnerType.BallAtom);
    
    // this.httpClientService.delete({ controller: "products" }, "f1ddf86d-9c61-48c6-1a49-08dc5541f029").subscribe();

    this.httpClientService.get<Product[]>({
      controller: "products"
    }).subscribe(data => console.log(data));

    // this.httpClientService.post({
    //   controller: "products"
    // },
    //   {
    //     name: "Kalem",
    //     stock: 100,
    //     price: 15
    //   }).subscribe();

    // this.httpClientService.put({
    //   controller: "products"
    // },{
    //   id: "dce971ec-8d05-47ca-1a4b-08dc5541f029",
    //   name: "rengli kagiz",
    //   stock: "333",
    //   price: 5.5
    // }).subscribe();

    // this.httpClientService.get({
    //   baseUrl: "https://jsonplaceholder.typicode.com",
    //   controller:"posts"
    // }).subscribe(data => console.log(data));

    


  }

}
