import { Component, OnInit } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { LayoutComponent } from "./admin/layout/layout.component";
import { CommonModule } from '@angular/common';
declare var mm: any;

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  imports: [RouterOutlet, RouterModule, CommonModule]
})
export class AppComponent implements OnInit {

  title = 'eCommerceClient';
  ngOnInit() {
    $(function () {
      alert("salam");
    });
  }

}




