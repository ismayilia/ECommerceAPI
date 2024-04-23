import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { BaseDialog } from '../base/base-dialog';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-file-upload-dialog',
  standalone: true,
  imports: [MatDialogModule,MatButtonModule],
  templateUrl: './file-upload-dialog.component.html',
  styleUrl: './file-upload-dialog.component.scss'
})
export class FileUploadDialogComponent extends BaseDialog<FileUploadDialogComponent>{
constructor(dialogRef: MatDialogRef<FileUploadDialogComponent>,
  @Inject(MAT_DIALOG_DATA) public data: FileUploadDialogState
){
  super(dialogRef);
}
}

export enum FileUploadDialogState{
  Yes,
  No
}
