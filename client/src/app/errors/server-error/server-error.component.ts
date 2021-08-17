import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.css']
})
export class ServerErrorComponent implements OnInit {

  error : any;
  // Only access router via the constructor can see relativity to error.interceptor.ts
  constructor(private router: Router) { 
    const navigation = this.router.getCurrentNavigation();
    //  Optional chaining operators as the ?.. Checks extras, state and errors
    this.error = navigation?.extras?.state?.error;
    
  }

  ngOnInit(): void {
  }

}
