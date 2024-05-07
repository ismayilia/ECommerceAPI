import { Component, Inject, Output } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { BaseDialog } from '../base/base-dialog';
import { FileUploadOptions, FileUploadComponent } from '../../services/common/file-upload/file-upload.component';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';

@Component({
    selector: 'app-select-product-image-dialog',
    standalone: true,
    templateUrl: './select-product-image-dialog.component.html',
    styleUrl: './select-product-image-dialog.component.scss',
    imports: [MatDialogModule, MatButtonModule,FileUploadComponent]
})
export class SelectProductImageDialogComponent extends BaseDialog<SelectProductImageDialogComponent> {
  constructor(dialogRef: MatDialogRef<SelectProductImageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: SelectProductImageState | string,
  ) {
    super(dialogRef)
  }

  @Output() options: Partial<FileUploadOptions> = {
    accept: ",png, .jpg, .jpeg, .gif",
    action: "upload",
    controller: "products",
    explanation: "Urun secin veya buraya surukleyin...",
    isAdminPage: true,
    queryString: `id=${this.data}`
  };
}

export enum SelectProductImageState {
  Close
}
