import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating App';
  users: any;

  // removed private http: HttpClient from constructor
  constructor(private accountService: AccountService) {} 

  ngOnInit() {
    // this.getUsers();
    //setCurrentUser is part of this AppComponent not account.service.ts 
    this.setCurrentUser();
  }

  setCurrentUser() {
    // JSON.parse() gets objects out of stringified form in account.service.ts
    // Need the || operator to check against one string and then a null value of a JSON.
    const user: User = JSON.parse(localStorage.getItem('user') || '{}');
    //import in account service and get its setcurrentuser method and use the user const set here as the param.
    this.accountService.setCurrentUser(user);
  }

    // Moved Over home.component.ts
    // getUsers() {
    //  Will send get request to get users from the url and check the Observable return by using the subscribe method
    //    Setting the parameter equal to the users by an arrow function.
    //      If theres an error or not 200 ok as end result it will console.log the error.
    // this.http.get("https://localhost:5001/api/users").subscribe(response => {
    //   this.users = response;
    // }, error => {
    // console.log(error);
    // })  
    // }
}