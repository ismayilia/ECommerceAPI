import { Component, OnInit } from '@angular/core';
import { AlertifyService, MessageType, Position } from '../../../services/admin/alertify.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from '../../../base/base.component';


@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent extends BaseComponent implements OnInit {
  constructor(private alertify: AlertifyService,spinner: NgxSpinnerService) {
     super(spinner)
  }

  ngOnInit(): void {
    this.showSpinner(SpinnerType.BallAtom);
  }

  m() {
    this.alertify.message("Salam", {
      messageType: MessageType.Success,
      delay: 3,
      position: Position.TopRight,
      dismissOthers:false
    })
  }
  d() {
    this.alertify.dismiss();
  }
}
