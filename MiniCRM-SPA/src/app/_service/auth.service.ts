import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
//import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import {map} from 'rxjs/Operators'
import { User } from '../_models/user';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

constructor(private http:HttpClient) { }

baseURL = environment.apiUrl +  'auth/'
 jwtHelper = new JwtHelperService();
 decodedToken: any;
 currentUser!: User | null;
 photoUrl = new BehaviorSubject<string>('../../assets/user.png');
 currentPhotoUrl = this.photoUrl.asObservable();
login(model:any)
{
  return this.http.post(this.baseURL+'login',model)
  .pipe(
    map((response:any) =>{
      const user = response;
      if(user){
        localStorage.setItem('token',user.token);
        //localStorage.setItem('user',JSON.stringify(user.user));
        this.decodedToken = this.jwtHelper.decodeToken(user.token);
        //this.currentUser = user.user;
      }
    })
  )
}
loggedIn(){
  debugger;
  const token = localStorage.getItem('token')!;//! Non-null assertion operator which mean this function won't return null 
  return !this.jwtHelper.isTokenExpired(token);
}
register(user:User){
  return this.http.post(this.baseURL + 'register',user);
}
}


