import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../services/ui/custom-toastr.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from '../../base/base.component';

export const AuthGuard: CanActivateFn = (route, state) => {

  const router: Router = inject(Router);
  const toastrService: CustomToastrService = inject(CustomToastrService);
  const spinner: NgxSpinnerService = inject(NgxSpinnerService);
  const jwtHelper: JwtHelperService = inject(JwtHelperService);

  spinner.show(SpinnerType.BallAtom);

  const token: string = localStorage.getItem("accessToken");
  let expired: boolean;

  try {
    expired = jwtHelper.isTokenExpired(token)
  } catch {
    expired = true
  }

  if (!token || expired) {
    router.navigate(["login"], { queryParams: { returnUrl: state.url } });
    toastrService.message("You need to log in!", "Unauthorized movement", {
      messageType: ToastrMessageType.Warning,
      position: ToastrPosition.TopRight
    })
  }

  spinner.hide(SpinnerType.BallAtom);

  return true;
};
