import { Component } from '@angular/core';
import {
  AlertifyService,
  MessageType,
  Position,
} from '../../../services/admin/alertify.service';
import { BaseComponent, SpinnerType } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { SignalRService } from '../../../services/common/signalr.service';
import { ReceiveFunctions } from '../../../constants/receive-functions';
import { HubUrls } from '../../../constants/hub-urls';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent extends BaseComponent {
  constructor(private alertify: AlertifyService, spinner:NgxSpinnerService,
    private signalRService: SignalRService
  ) {
    super(spinner);
    signalRService.start(HubUrls.ProductHub)
    signalRService.start(HubUrls.OrderHub)


  }
  ngOnInit(): void {
    // this.showSpinner(SpinnerType.BallAtom)
    this.signalRService.on(ReceiveFunctions.ProductAddedMessageReceiveFunction, message => {
      this.alertify.message(message, {
        messageType: MessageType.Notify,
        position: Position.TopRight
      })
    });

    this.signalRService.on(ReceiveFunctions.OrderAddedMessageReceiveFunction, message => {
      this.alertify.message(message, {
        messageType: MessageType.Notify,
        position: Position.TopCenter
      });
    })
  }

  m() {
    this.alertify.message('Merhaba', {
      messageType: MessageType.Success,
      delay: 5,
      position: Position.BottomCenter,
    });
  }

  d() {
    this.alertify.dismiss();
  }
}
