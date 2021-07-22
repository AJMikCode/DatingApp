import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating App';
  users: any;

  constructor(private http: HttpClient) {} 

  ngOnInit() {
    this.getUsers();
  }

    getUsers() {
    //Will send get request to get users from the url and check the Observable return by using the subscribe method
    //Setting the parameter equal to the users by an arrow function.
    // If theres an error or not 200 ok as end result it will console.log the error.
    this.http.get("https://localhost:5001/api/users").subscribe(response => {
      this.users = response;
    }, error => {
    console.log(error);
    })  
    }
}