import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, of as observableOf, scheduled } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  // Add construtor to bring in services
  constructor(private accountService: AccountService, private toasterService: ToastrService) {}
  // Auth guard instantly subscribes to any observables, noe need to subscribe to currentUser in account service
  canActivate(): Observable<boolean>{
    // Going to do something with this but no need to subscribe to the observable
    return this.accountService.currentUser$.pipe (
      map(user => {
        if(user) return true;
        this.toasterService.error('Cannot load, please log in to see these!');
        // return false statment is new and changes the return type from true | undefined into boolean.
        return false;
      })
    )
  }
  
}
