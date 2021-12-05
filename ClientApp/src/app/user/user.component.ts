import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { UserData } from '../models/userData';

@Component({
  selector: 'app-home',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  user: UserData = new UserData();

  constructor(private http: HttpClient, private route: ActivatedRoute, private router: Router, @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      const id = params['id'];
      this.http.get<UserData>(this.baseUrl + 'api/user/getSingle/' + id).subscribe(result => {
        this.user = result;
      }, error => console.error(error));
    });
  }

  saveUser() {
    this.http.post(this.baseUrl + 'api/user/post', this.user).subscribe(result => {
      alert("zapisane");
    }, error => console.error(error));
  }
}
