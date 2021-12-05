import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from '../_models/user';
import { AlertifyService } from '../_service/alertify.service';
import { AuthService } from '../_service/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  User! : User;
  registerForm!: FormGroup;
  constructor(private authService:AuthService,private alertify:AlertifyService
    ,private fb:FormBuilder,private router:Router) { }

  ngOnInit() {
   this.createRegisterForm();
  }
  createRegisterForm(){
    const now = new Date();
    this.registerForm = this.fb.group({
      username: ['',[Validators.required]],
      password: ['',[Validators.required,Validators.minLength(4),Validators.maxLength(8)]],
      confirmPassword: ['',[Validators.required]]
    },
    {
      validators: [this.match('password', 'confirmPassword')]
    })
  }

  match(controlName: string, checkControlName: string): ValidatorFn {
    return (controls: AbstractControl) => {
      const control = controls.get(controlName);
      const checkControl = controls.get(checkControlName);
      if (checkControl?.errors && !checkControl?.errors.matching) {
        return null;
      }
      if (control?.value !== checkControl?.value) {
        controls.get(checkControlName)?.setErrors({ matching: true });
        return { matching: true };
      } else {
        return null;
      }
    };
  }
  

  register(){
    if (this.registerForm.valid) {
      this.User = Object.assign({},this.registerForm.value)
      this.authService.register(this.User).subscribe(() =>{
        this.alertify.success("Registeration successful")
      },error => {this.alertify.error(error)}
      , () => {
        this.authService.login(this.User).subscribe(() => {
          this.router.navigate(['/customers'])
        })
      });
      
    }
   
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
}
