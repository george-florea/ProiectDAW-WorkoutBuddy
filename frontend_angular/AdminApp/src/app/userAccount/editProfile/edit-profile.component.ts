import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs';
import { getEditInfoSelector } from '../store/account.reducer';
import { IAccountState } from '../store/account.state';
import { UserAccountService } from '../user-account.service';
import { IEditInfo } from './IEditInfo';
import * as accountActions from '../store/account.actions'


@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css'],
})
export class EditProfileComponent implements OnInit, OnDestroy {
  constructor(private store: Store<IAccountState>) {}

  sub$!: Subscription;
  user: IEditInfo = {
    name: '',
    username: '',
    birthDate: new Date(),
    email: '',
  };

  profileForm = new FormGroup({
    name: new FormControl(''),
    username: new FormControl(''),
    birthdate: new FormControl(new Date()),
    email: new FormControl(''),
  });

  ngOnInit(): void {
    this.store.dispatch(accountActions.getEditInfo());

    this.sub$ = this.store.select(getEditInfoSelector).subscribe({
      next: (user) => {
        this.user = user;
        this.setUser(user);
      },
      error: (err) => console.log(err),
    });
  }

  ngOnDestroy(): void {
    this.sub$.unsubscribe();
  }

  setUser(user: IEditInfo):void {
    this.profileForm.get("name")?.setValue(user.name);
    this.profileForm.get("username")?.setValue(user.username);
    this.profileForm.get("birthdate")?.setValue(user.birthDate);
    this.profileForm.get("email")?.setValue(user.email);
  }

  onSubmit(){
    this.store.dispatch(accountActions.submitEditInfo({ editInfo: this.profileForm.value as IEditInfo}))
  }
}
