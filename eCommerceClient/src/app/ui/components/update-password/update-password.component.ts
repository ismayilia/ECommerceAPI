import { Component, OnInit } from '@angular/core';
import { BaseComponent, SpinnerType } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserAuthService } from '../../../services/common/models/user-auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService, MessageType, Position } from '../../../services/admin/alertify.service';
import { UserService } from '../../../services/common/models/user.service';
import { subscribe } from 'diagnostics_channel';

@Component({
  selector: 'app-update-password',
  templateUrl: './update-password.component.html',
  styleUrl: './update-password.component.scss'
})
export class UpdatePasswordComponent extends BaseComponent implements OnInit {
  constructor(spinner: NgxSpinnerService, private userAuthService: UserAuthService,
    private activatedRoute: ActivatedRoute, private alertifyService: AlertifyService,
    private userService: UserService, private router: Router
  ) {
    super(spinner);
  }

  state: any = false;

  ngOnInit(): void {
    this.showSpinner(SpinnerType.BallAtom);
    //url-deki parametrleri oxumaq icim bir object
    this.activatedRoute.params.subscribe({
      next: async params => {
        const userId: string = params['userId'];
        const resetToken: string = params['resetToken'];
        this.state = await this.userAuthService.verifyResetToken(resetToken, userId, () => {
          this.hideSpinner(SpinnerType.BallAtom);
        })
      }
    })
  }

  updatePassword(password: string, passwordConfirm: string) {
    this.showSpinner(SpinnerType.BallAtom);
    if (password != passwordConfirm) {
      this.alertifyService.message('Confirm password', {
        messageType: MessageType.Error,
        position: Position.TopRight,
      });
      this.hideSpinner(SpinnerType.BallAtom);
      return;
    }

    this.activatedRoute.params.subscribe({
      next: async (params) => {
        const userId: string = params['userId'];
        const resetToken: string = params['resetToken'];
        await this.userService.updatePassword(
          userId,
          resetToken,
          password,
          passwordConfirm,
          () => {
            this.alertifyService.message('Password has been changed', {
              messageType: MessageType.Success,
              position: Position.TopRight,
            });
            this.router.navigate(['/login']);
          },
          (error) => {
            console.log(error);
          }
        );
        this.hideSpinner(SpinnerType.BallAtom);
      },
    });
  }
}
