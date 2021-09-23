import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';
import { take } from 'rxjs/operators';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  currentUser: User | undefined; 

  constructor(private accountService: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    //contains contents of currentUser or else null.
    

    //pipe take 1 takes the first out of the user slected which should just be the user.
      //Makes sure trhat when observable completed its done and doesnt go onto the next user.
        //Rxjs search up and it will make more sense =).
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.currentUser = user);
    if(this.currentUser) {
      //clone request and add authentication header onto it
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.currentUser.token}`
        }
      })
    }

    return next.handle(request);
  }
}
