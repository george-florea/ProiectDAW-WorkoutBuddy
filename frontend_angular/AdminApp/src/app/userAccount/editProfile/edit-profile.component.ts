import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs';
import { UserAccountService } from '../user-account.service';
import { IEditInfo } from './IEditInfo';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css'],
})
export class EditProfileComponent implements OnInit, OnDestroy {
  constructor(private service: UserAccountService) {}

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
    this.sub$ = this.service.editInfo$.subscribe({
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
    this.service
          .editProfile(this.profileForm.value as IEditInfo)
          .subscribe({
            next: data => location.href = '/user-profile',
            error: err => location.href = 'https://localhost:3000'
          });
  }
}
