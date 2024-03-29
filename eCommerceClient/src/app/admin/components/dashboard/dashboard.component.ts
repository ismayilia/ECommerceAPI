import { Component, OnInit } from '@angular/core';
import { AlertifyService, MessageType, Position } from '../../../services/admin/alertify.service';


@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  constructor(private alertify: AlertifyService) {

  }
  ngOnInit(): void {
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
