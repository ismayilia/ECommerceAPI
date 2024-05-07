import { Component, OnInit } from '@angular/core';
import { BaseComponent, SpinnerType } from '../../../base/base.component';
import { AlertifyService } from '../../../services/admin/alertify.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-customers',
  standalone: true,
  imports: [],
  templateUrl: './customers.component.html',
  styleUrl: './customers.component.scss'
})
export class CustomersComponent extends BaseComponent implements OnInit{

  constructor(private alertify: AlertifyService,spinner: NgxSpinnerService) {
    super(spinner)
 }

ngOnInit(): void {
  this.showSpinner(SpinnerType.BallAtom);
}
}
