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

  constructor(private http: HttpClient, private router: Router, private route: ActivatedRoute, public activatedRoute: ActivatedRoute, @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit() {
    this.activatedRoute.paramMap.subscribe(() => {
      this.user = window.history.state["user"] as UserData;
      if (!this.user) {
        this.router.navigate(['/']);
      }
    });
  }

  saveUser() {
    this.http.put(this.baseUrl + 'api/user/saveUser', this.user).subscribe(result => {
      alert("zapisane");
    }, error => console.error(error));
  }
}
