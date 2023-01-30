import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { map, mergeMap, switchMap, tap } from 'rxjs';
import { PendingExercisesService } from '../pending-exercises.service';
import * as exercisesActions from './pending-exercises.actions';

@Injectable()
export class PendingExercisesEffects {
  constructor(
    private actions$: Actions,
    private service: PendingExercisesService
  ) {}

  getPendingExercises = createEffect(() => {
    return this.actions$.pipe(
      ofType(exercisesActions.getPendingExercises),
      mergeMap(() =>
        this.service.updatedPendingExercises$.pipe(
          tap((exercises) => console.log(exercises)),
          map((exercises) =>
            exercisesActions.storePendingExercises({ exercises })
          )
        )
      )
    );
  });

  approvePendingExercise = createEffect(() => {
    return this.actions$.pipe(
      ofType(exercisesActions.ApproveExercise),
      switchMap((action: any) =>
        this.service.approveExercise(action.exerciseId).pipe(
          map(() =>
            exercisesActions.UpdateExercises({
              exerciseId: action.exerciseId,
            })
          )
        )
      )
    );
  });

  deletePendingExercise = createEffect(() => {
    return this.actions$.pipe(
      ofType(exercisesActions.DeleteExercise),
      switchMap((action) => {
        return this.service.deleteExercise(action.exerciseId).pipe(
          map(() =>
            exercisesActions.UpdateExercises({
              exerciseId: action.exerciseId,
            })
          )
        );
      })
    );
  });

  updatePendingExercise = createEffect(() => {
      return this.actions$.pipe(
        ofType(exercisesActions.UpdateExercises),
        switchMap((action) => this.service.updateExercise(action.exerciseId))
      );
    },
    { dispatch: false }
  );
}
