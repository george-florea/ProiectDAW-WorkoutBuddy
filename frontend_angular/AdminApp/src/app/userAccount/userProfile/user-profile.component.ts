import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { IAccountState } from '../store/account.state';
import { IUserProfile } from './IUserProfile';
import * as accountActions from '../store/account.actions'
import {  getUserProfileSelector } from '../store/account.reducer';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit, OnDestroy{
  constructor(private store: Store<IAccountState>) {}

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
    this.store.dispatch(accountActions.getUserProfile());

    this.sub$ = this.store.select(getUserProfileSelector).subscribe({
      next: user => this.user = user
    })

    // this.sub$ = this.service.userInfo$.subscribe({
    //   next: user => this.user = user,
    //   error: err => console.log(err)
    // })
  }

  ngOnDestroy(): void {
    this.sub$.unsubscribe();
  }
}
