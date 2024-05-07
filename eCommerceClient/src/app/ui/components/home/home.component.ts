import { Component, OnInit } from '@angular/core';
import { BasketsComponent } from '../baskets/baskets.component';
import { BaseComponent, SpinnerType } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent extends BaseComponent implements OnInit{
  constructor(spinner: NgxSpinnerService){
    super(spinner)
  }
  
    ngOnInit()
  {
    this.showSpinner(SpinnerType.BallAtom)
  }
}
