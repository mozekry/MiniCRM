import { Injectable } from '@angular/core';
import {CanActivate, Router} from '@angular/router';
import { Observable } from 'rxjs';
import { AlertifyService } from '../_service/alertify.service';
import { AuthService } from '../_service/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private authService:AuthService,private router:Router
    ,private alertify:AlertifyService){}

  canActivate(): boolean{
    if (this.authService.loggedIn()) {
      return true;
    }

    this.alertify.error('Not Allowed !!');
    this.router.navigate(['/home']);
    return false;
  }
   
  
  
}
