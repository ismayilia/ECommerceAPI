import { Component, OnInit, ViewChild, viewChild } from '@angular/core';
import { BaseComponent, SpinnerType } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { HttpClientService } from '../../../services/common/http-client.service';
import { Create_Product } from '../../../contracts/create_product';
import {MatSidenavModule} from '@angular/material/sidenav';
import { CreateComponent } from "./create/create.component";
import { ListComponent } from "./list/list.component";
import { DeleteDirective } from '../../../directives/admin/delete.directive';




@Component({
    selector: 'app-products',
    standalone: true,
    templateUrl: './products.component.html',
    styleUrl: './products.component.scss',
    imports: [MatSidenavModule, CreateComponent, ListComponent]
})
export class ProductsComponent extends BaseComponent implements OnInit {

  constructor(spinner: NgxSpinnerService, private httpClientService: HttpClientService) {
    super(spinner)
  }

  ngOnInit() {
    
  }

  @ViewChild(ListComponent) listComponents: ListComponent

  createdProduct(createdProduct: Create_Product){
    this.listComponents.getProducts();
  }

}
