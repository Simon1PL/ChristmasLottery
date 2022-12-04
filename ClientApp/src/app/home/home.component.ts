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

  constructor(private http: HttpClient, private router: Router, @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit() {
    this.http.get<UserData[]>(this.baseUrl + 'api/user/get').subscribe(result => {
      this.users = result;
    }, error => console.error(error));
  }

  chooseUser(user: UserData) {
    if (confirm(user.userName + " czy to Ty? Jeśli nie kliknij CANCEL i wybierz siebie.")) {
      this.router.navigate(['/user/' + user.id]);
    }
  }
}
