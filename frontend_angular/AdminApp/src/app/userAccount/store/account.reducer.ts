import { createAction, createFeatureSelector, createReducer, createSelector, on } from '@ngrx/store';
import { IAccountState, initialState } from './account.state';
import * as accountActions from './account.actions'


const geAccountState = createFeatureSelector<IAccountState>('account');

export const getUserProfileSelector = createSelector(
    geAccountState,
  state => state.userProfile
)

export const getEditInfoSelector = createSelector(
    geAccountState,
  state => state.editInfo
)


export const accountReducer = createReducer<IAccountState>(
  initialState,
  on(accountActions.storeUserProfile, (state, action) => {
    return {
      ...state,
      userProfile: action.userProfile
    };
  }),

  on(accountActions.storeEditInfo, (state, action) => {
    return {
      ...state,
      editInfo: action.editInfo
    };
  }),
);
