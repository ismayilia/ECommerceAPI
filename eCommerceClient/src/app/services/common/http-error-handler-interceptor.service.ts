import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, of } from 'rxjs';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../ui/custom-toastr.service';
import { UserAuthService } from './models/user-auth.service';

@Injectable({
  providedIn: 'root'
})
export class HttpErrorHandlerInterceptorService implements HttpInterceptor {

  constructor(private toastrService: CustomToastrService, private userAuthService: UserAuthService) { }

  //req parametr--araya girme... next--parametr ise sonraki proses, devamin getirme, delegate-dir
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(catchError(error => {
      switch (error.status) {

        case HttpStatusCode.Unauthorized:
          this.toastrService.message(
            'You do not have permition for this action-401.',
            'Unathorized Action!',
            {
              messageType: ToastrMessageType.Warning,
              position: ToastrPosition.TopLeft,
            });

          this.userAuthService.refreshTokenLogin(localStorage.getItem('refreshToken')).then(data => {

          });

          break;

        case HttpStatusCode.InternalServerError:
          this.toastrService.message("Lost connection with Server-500", "Server Error", {
            messageType: ToastrMessageType.Warning,
            position: ToastrPosition.TopLeft
          })
          break;

        case HttpStatusCode.BadRequest:
          this.toastrService.message(
            'Server cannot process or recognize the request.',
            'UnRecognised Action-400',
            {
              messageType: ToastrMessageType.Warning,
              position: ToastrPosition.TopLeft,
            }
          );
          break;

        case HttpStatusCode.NotFound:
          this.toastrService.message(
            'Page could not found',
            '404 Not Found',
            {
              messageType: ToastrMessageType.Warning,
              position: ToastrPosition.TopLeft,
            }
          );
          break;

        default:
          this.toastrService.message(
            'Something went wrong please try again later.',
            'Warning!',
            {
              messageType: ToastrMessageType.Warning,
              position: ToastrPosition.TopLeft,
            }
          );
          break;



      }

      return of(error);
    }))
  }
}
