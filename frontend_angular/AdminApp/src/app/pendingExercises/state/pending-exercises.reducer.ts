import { createAction, createFeatureSelector, createReducer, createSelector, on } from '@ngrx/store';
import { initialState, IState } from './pending-exericses.state';
import * as actions from './pending-exercises.actions';
import { IExercise } from '../IExercise';


const getPendingExercisesState = createFeatureSelector<IState>('pending-exercises');



export const getExercises = createSelector(
  getPendingExercisesState,
  state => state.exercises
)

export const pendingExercisesReducer = createReducer<IState>(
  initialState,

  on(actions.storePendingExercises, (state, action) => {
    return {
      ...state,
      exercises:action.exercises
    }
  })
);
