import { createAction, props } from '@ngrx/store';
import { IEditInfo } from '../editProfile/IEditInfo';
import { IUserProfile } from '../userProfile/IUserProfile';

export const getUserProfile = createAction('[Account] Get User Profile');

export const storeUserProfile = createAction(
  '[Account] Store User Profile',
  props<{ userProfile: IUserProfile }>()
);

export const getEditInfo = createAction('[Account] Get Edit Info');

export const storeEditInfo = createAction(
  '[Account] Store Edit Info',
  props<{ editInfo: IEditInfo }>()
);

export const submitEditInfo = createAction(
  '[Account] Submit Edit Info',
  props<{ editInfo: IEditInfo}>()
)
