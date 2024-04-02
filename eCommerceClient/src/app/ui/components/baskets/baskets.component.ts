import { Component, OnInit } from '@angular/core';
import { BaseComponent, SpinnerType } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-baskets',
  standalone: true,
  imports: [],
  templateUrl: './baskets.component.html',
  styleUrl: './baskets.component.scss'
})
export class BasketsComponent extends BaseComponent implements  OnInit{

  constructor(spinner: NgxSpinnerService){
    super(spinner)
  }
  
    ngOnInit()
  {
    this.showSpinner(SpinnerType.BallAtom)
  }
}
