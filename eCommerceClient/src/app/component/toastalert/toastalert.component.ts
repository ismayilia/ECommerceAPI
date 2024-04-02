import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { AppModule } from '../../app.module';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-toastalert',
  standalone: true,
  imports: [MatCardModule,MatButtonModule],
  templateUrl: './toastalert.component.html',
  styleUrl: './toastalert.component.scss'
})
export class ToastalertComponent {
  constructor(private toasterService: ToastrService){
    
  }

  showsuccess(){
    this.toasterService.success("salam","isi")
  }
}
