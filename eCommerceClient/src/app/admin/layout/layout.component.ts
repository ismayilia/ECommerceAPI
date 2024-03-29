import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from "./components/header/header.component";
import { SidebarComponent } from "./components/sidebar/sidebar.component";
import { FooterComponent } from "./components/footer/footer.component";
import { RouterModule } from '@angular/router';
import {MatSidenavModule} from '@angular/material/sidenav';


@Component({
    selector: 'app-layout',
    standalone: true,
    templateUrl: './layout.component.html',
    styleUrl: './layout.component.scss',
    imports: [HeaderComponent, FooterComponent, RouterModule, SidebarComponent,MatSidenavModule]
})


export class LayoutComponent implements OnInit {
constructor() {
    
}
    ngOnInit(): void {
        
    }
}

