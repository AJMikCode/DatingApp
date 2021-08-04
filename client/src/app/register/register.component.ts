import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  // @Input() usersFromHomeComponent : any;
  // Make sure to import EventEmitter from @angular/core, nothing else
  @Output() cancelRegister = new EventEmitter();

  model: any = {};
  
  constructor(private accountService : AccountService, private toastrService: ToastrService) { }

  ngOnInit(): void {
  }

  register() {
    //Subscribe to observable and get a response 
    this.accountService.register(this.model).subscribe(response => {
      console.log(response);
      this.cancel();
    }, error => {
      console.log(error);
      this.toastrService.error(error.error);
    })
  }

  // Search up child to parent communication using angular 
  // Uses cancelRegister Ouput and emits false for registerButtonScreen
  cancel() {
    this.cancelRegister.emit(false);
  }

}
