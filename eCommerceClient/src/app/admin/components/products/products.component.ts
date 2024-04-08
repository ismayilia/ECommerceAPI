import { Component, OnInit } from '@angular/core';
import { BaseComponent, SpinnerType } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { HttpClientService } from '../../../services/common/http-client.service';
import { Create_Product } from '../../../contracts/create_product';
import {MatSidenavModule} from '@angular/material/sidenav';
import { CreateComponent } from "./create/create.component";
import { ListComponent } from "./list/list.component";



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
    
    this.showSpinner(SpinnerType.BallAtom);

  }

}
