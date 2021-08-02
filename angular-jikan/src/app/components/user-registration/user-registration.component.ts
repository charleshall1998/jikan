import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterUserRequest } from 'src/app/models/RegisteredUserRequest';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css']
})
export class UserRegistrationComponent implements OnInit {

  constructor(private authService : AuthService, private router : Router) { }

  ngOnInit(): void {
  }

  /**
   * Submits the user-entered form for registration.
   *
   * @param registerForm - The NgForm being submitted.
   *
   */
  onSubmit(registerForm: NgForm): void {
    if(registerForm.valid) {
      const user = registerForm.value;
      if(user.password == user.confirmPass) {
        
        let toRegister : RegisterUserRequest = {
          Username : user.username,
          Name : user.name,
          Email : user.email,
          Password : user.password,
        }

        this.authService.registerUser(toRegister).subscribe((_) => console.log(_));
        this.router.navigate([""]);

      }
    }
  }

}
