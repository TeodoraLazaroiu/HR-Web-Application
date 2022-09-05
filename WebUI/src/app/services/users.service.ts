import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersService{

  readonly apiurl = "https://localhost:7278/api";
  isLoggedInVar: boolean = this.IsLoggedIn();

  constructor(private http:HttpClient) { }
  
  ProceedLogin(user:any) {
    return this.http.post(this.apiurl + '/Users/Login', user);
  }

  IsLoggedIn() {
    return localStorage.getItem('token') != null;
  }

  LogOut() {
    localStorage.removeItem('token');
    window.location.reload();
  }

  GetToken() {
    return localStorage.getItem('token') || '';
  }
}
