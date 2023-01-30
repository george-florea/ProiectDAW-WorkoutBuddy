import { createAction, props } from '@ngrx/store';
import { IExercise } from '../IExercise';

export const getPendingExercises = createAction(
  '[Pending-Exercises] Get Pending Exercises'
);

export const storePendingExercises = createAction(
  '[Pending-Exercises] Store Pending Exercises',
  props<{ exercises: IExercise[] }>()
);

export const ApproveExercise = createAction(
  '[Pending-Exercises] Approve Pending Exercise',
  props<{ exerciseId: string }>()
);

export const DeleteExercise = createAction(
  '[Pending-Exercises] Delete Pending Exercise',
  props<{ exerciseId: string }>()
);

export const UpdateExercises = createAction(
  '[Pending-Exercises] Update Pending Exercise',
  props<{ exerciseId: string }>()
);
