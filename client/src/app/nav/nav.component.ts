import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  // Solution to problems such as AccountService not working may just be to restart visual studio
    // accountService changed to public so the nav component html can read it
      // nav.component.html Can use currentUser$ by calling in Angular "accountService.currentUser$"
  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
  }

    login()
    {
      this.accountService.login(this.model).subscribe(response => {
        //returns a response from Observable by using .subscribe() method
        console.log(response);
      }, error => {
        //Same as above by console.logging the arrow function passed in
        console.log(error); 
      });
    }

    logout() 
    {
      //Aspect will be removed later but logging out should return that tha user isn't logged in or false.
      this.accountService.logout();
    }

    //The getCurrentUser() is replaced by the statement this.currentUser$ = this.accountService.currentUser$; &
        //currentUser$: Observable<User>;
    // getCurrentUser() {
    //      Never completes like an HTTP request, have to unsubscribe. Instead of using async pipe.
    //   this.accountService.currentUser$.subscribe( user => {
    //      !! turns user object into a boolean
    //        If user = null results in false as boolean. If user = something else the boolean results in true.
    //       this.loggedIn = !!user;
    //   }, error => {
    //     console.log(error);
    //   });
    // }
}
