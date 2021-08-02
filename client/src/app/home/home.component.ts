import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  // Set to false so that it follows better logic
  registerButtonScreen = false;

  constructor() { }

  ngOnInit(): void {
  }

  registerToggle() {
    //toggles value of registerMode boolean
    this.registerButtonScreen = !this.registerButtonScreen;
  }
  
  cancelRegisterMode(event: boolean) {
      // event should return false based on preset emit from child of register component.ts cancel() method
      //retrusn back to main screen instead of register form screen.
      this.registerButtonScreen = event;  
    }
}

