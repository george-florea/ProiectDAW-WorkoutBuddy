import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, mergeMap, switchMap, tap } from 'rxjs';
import { UserAccountService } from '../user-account.service';
import * as accountActions from './account.actions';

@Injectable()
export class AccountEffects {
  constructor(private actions$: Actions, private service: UserAccountService) {}

  getUserProfile = createEffect(() => {
    return this.actions$.pipe(
      ofType(accountActions.getUserProfile),
      mergeMap(() =>
        this.service.userInfo$.pipe(
          tap((account) => console.log(account)),
          map((userProfile) => accountActions.storeUserProfile({ userProfile }))
        )
      )
    );
  });

  getEditInfo = createEffect(() => {
    return this.actions$.pipe(
      ofType(accountActions.getEditInfo),
      mergeMap(() =>
        this.service.editInfo$.pipe(
          tap((account) => console.log(account)),
          map((editInfo) => accountActions.storeEditInfo({ editInfo }))
        )
      )
    );
  });

  submitEditInfo = createEffect(
    () => {
      return this.actions$.pipe(
        ofType(accountActions.submitEditInfo),
        switchMap((action) => {
          console.log(action);
          return this.service
            .editProfile(action.editInfo)
            .pipe(tap(() => (location.href = '/user-profile')));
        })
      );
    },
    { dispatch: false }
  );
}
