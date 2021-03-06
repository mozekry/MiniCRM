import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_service/auth.service';
import { Router} from '@angular/router';
import { AlertifyService } from '../_service/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model:any={};
  constructor(public authService: AuthService,private router:Router,private alertify:AlertifyService) { }

  ngOnInit() {
  }
  login(){  
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged in');

    }, error => {
      this.alertify.error(error);
    },() => {
      this.router.navigate(['/customers']);
    });
  }

  loggedIn(){
    return this.authService.loggedIn();
    
  }

  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertify.message('logged out');
    this.router.navigate(['/home']);
  }

}
