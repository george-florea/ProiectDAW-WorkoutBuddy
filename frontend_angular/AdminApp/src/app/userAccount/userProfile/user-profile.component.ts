import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { UserAccountService } from '../user-account.service';
import { IUserProfile } from './IUserProfile';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit, OnDestroy{
  constructor(private service: UserAccountService) {}

  sub$!: Subscription;
  user: IUserProfile = {
    name: '',
    username: '',
    roles: [],
    birthDate: new Date(),
    email: '',
    currentWeight: 0
  };

  ngOnInit(): void {
    this.sub$ = this.service.userInfo$.subscribe({
      next: user => this.user = user,
      error: err => console.log(err)
    })
  }

  ngOnDestroy(): void {
    this.sub$.unsubscribe();
  }
}
