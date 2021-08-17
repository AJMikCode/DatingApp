import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  //  Add router for some types of errors to redirect user to specific page, and for other errors just show notification.
  constructor(private router: Router, private toastr: ToastrService) { }

  //  intercept request that comes out or response that comes back in next where response is handled
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError(error => {
        //  First check if there is an error
        if (error) {
          //  If there is an error, swith statement for what to do based on 500 status or 401 status or etc.
          switch (error.status) {
            //  Tricky because there are two types of 400 errors one returning just string of bad request
            //  And the other one returning that both Username and Passowrd field are required 
            case 400:
              //  Funny If statement but can be seen by the error response inside console of chrome
              if (error.error.errors) {
                //  Name for Validation Errors
                const modalStateErrors = [];
                //Flatten and push validation errors into array which is modalStateErrors
                for (const key in error.error.errors) {
                  if (error.error.errors[key]) {
                    modalStateErrors.push(error.error.errors[key])
                  }
                }
                //  This is so when you receieve both validation errors of no password and no Usernmae its displayed under the form and not as toastr pop-up
                throw modalStateErrors.flat();
              } else {
                this.toastr.error(error.statusText, error.status);
              }
              break;
            case 401:
              // Shows error via toastr pop up with status text and statucs which should be number 401
              this.toastr.error(error.statusText, error.status);
              break;
            case 404:
              // Redirect to not found page
              this.router.navigateByUrl('/not-found');
              break;
            case 500:
              // Get details of error returned from api, and redirect like previous switch statement
              //  Control how the target URL should be constructed or interpreted as Navigation Extras(NavigationBehaviorOptions as 2nd Parameter)
              const navigationExtras: NavigationExtras = { state: { error: error.error } };
              this.router.navigateByUrl('/server-error', navigationExtras);
              break;
            default:
              this.toastr.error("Something Very Unexpected went wrong");
              console.log(error);
              break;
          }
        }
        //  Used at ed to end up showing the error
        return throwError(error);
      })
    );
  }
}
