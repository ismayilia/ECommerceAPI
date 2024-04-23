import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { NgxFileDropEntry, NgxFileDropModule } from 'ngx-file-drop';
import { HttpClientService } from '../http-client.service';
import { HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { AlertifyService, MessageType, Position } from '../../admin/alertify.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../ui/custom-toastr.service';
import { MatDialog } from '@angular/material/dialog';
import { FileUploadDialogComponent, FileUploadDialogState } from '../../../dialogs/file-upload-dialog/file-upload-dialog.component';
import { DialogService } from '../dialog.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from '../../../base/base.component';

@Component({
  selector: 'app-file-upload',
  standalone: true,
  imports: [NgxFileDropModule, CommonModule],
  templateUrl: './file-upload.component.html',
  styleUrl: './file-upload.component.scss'
})
export class FileUploadComponent {

  constructor(private httpClientService: HttpClientService,
    private alertifyService: AlertifyService,
    private customToastrService: CustomToastrService,
    private dialog: MatDialog,
    private dialogService: DialogService,
    private spinner: NgxSpinnerService
  ) { }

  public files: NgxFileDropEntry[];
  //partial olurki object olaraq cata bilek deye, input o
  @Input() options: Partial<FileUploadOptions>;

  //gelen deyerleri tutmaq ucun

  public selectedFiles(files: NgxFileDropEntry[]) {
    
    this.files = files;

    const fileData: FormData = new FormData();

    for (const file of files) {
      (file.fileEntry as FileSystemFileEntry).file((_file: File) => {
        fileData.append(_file.name, _file, file.relativePath)
      })
    }

    this.dialogService.openDialog({
      componentType: FileUploadDialogComponent,
      data: FileUploadDialogState.Yes,
      afterClosed: () => this.httpClientService.post({
        controller: this.options.controller,
        action: this.options.action,
        queryString: this.options.queryString,
        headers: new HttpHeaders({ "responseType": "blob" })
      }, fileData).subscribe(data => {
  
        const message: string = "Files upload success!";
  
        if (this.options.isAdminPage) {
          this.alertifyService.message(message, {
            dismissOthers: true,
            messageType: MessageType.Success,
            position: Position.TopRight
          })
        }
        else {
          this.customToastrService.message(message, "Success", {
            messageType: ToastrMessageType.Success,
            position: ToastrPosition.TopRight
          })
        }
      }, (errorResponse: HttpErrorResponse) => {
  
        const message: string = "Files upload error!";
  
  
        if (this.options.isAdminPage) {
          this.alertifyService.message(message, {
            dismissOthers: true,
            messageType: MessageType.Error,
            position: Position.TopRight
          })
        }
        else {
          this.customToastrService.message(message, "Not Success", {
            messageType: ToastrMessageType.Error,
            position: ToastrPosition.TopRight
          })
        }
      })
  
    });
  
  }
  // openDialog(afterClosed: any): void {
  //   const dialogRef = this.dialog.open(FileUploadDialogComponent, {
  //     width: '300px',
  //     data: FileUploadDialogState.Yes,
  //   });

  //   dialogRef.afterClosed().subscribe(result => {
  //     if (result == FileUploadDialogState.Yes)
  //       afterClosed();
  //   });
  // }
}

export class FileUploadOptions {
  controller?: string;
  action?: string;
  queryString?: string;
  explanation?: string;
  accept?: string;
  isAdminPage?: boolean = false;
}
