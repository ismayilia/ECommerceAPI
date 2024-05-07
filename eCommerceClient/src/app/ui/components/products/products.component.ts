import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { BaseComponent, SpinnerType } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [RouterModule,RouterOutlet,CommonModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss'
})
export class ProductsComponent extends BaseComponent implements OnInit{
  constructor(spinner: NgxSpinnerService){
    super(spinner)
  }
  
    ngOnInit()
  {
    this.showSpinner(SpinnerType.BallAtom)
  }

}
