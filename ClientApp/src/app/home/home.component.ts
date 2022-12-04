import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { UserData } from '../models/userData';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  users: UserData[];
  user: UserData;

  constructor(private http: HttpClient, private router: Router, @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit() {
    this.http.get<UserData[]>(this.baseUrl + 'api/user/get').subscribe(result => {
      this.users = result;
      this.users.forEach(u => {
        u.hasPassword = u.password.toLowerCase() == String(true);
        u.password = null;
      });
    }, error => console.error(error));
  }

  chooseUser(user: UserData) {
    this.user = user;
  }

  checkPassword() {
    this.http.post(this.baseUrl + 'api/user/getUserByIdAndPassword', { id: this.user.id, password: this.user.password }).subscribe(result => {
      this.router.navigate(['/user/' + this.user.id], { state: { user: result } });
    }, error => {
      if (error.status == 403) {
        alert("błędne hasło");
      }

      console.error(error);
    });
  }
}
