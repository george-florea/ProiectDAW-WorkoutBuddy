import { Component, OnDestroy, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable, Subscriber, Subscription } from 'rxjs';
import { IActionHandler } from './IActionHandler';
import { IExercise } from './IExercise';
import { PendingExercisesService } from './pending-exercises.service';
import * as actions from './state/pending-exercises.actions';
import {
  getExercises,
} from './state/pending-exercises.reducer';

@Component({
  templateUrl: './pending-exercises.component.html',
  styleUrls: ['./pending-exercises.component.css'],
})
export class PendingExercisesComponent implements OnInit, OnDestroy {
  constructor(
    private store: Store<any>
  ) {}

  sub$!: Subscription;
  exercises:IExercise[] = [];

  ngOnInit() {
    this.store.dispatch(actions.getPendingExercises());

    this.sub$ = this.store.select(getExercises).subscribe({
      next: (exercises) => {
        this.exercises = exercises;
      },
    });
  }

  ngOnDestroy(): void {
    this.sub$.unsubscribe();
  }

  actionHandler(actionObject: IActionHandler): void {
    if (actionObject.isApproved) {
      this.store.dispatch(actions.ApproveExercise({exerciseId: actionObject.exerciseId}))
    } else {
      this.store.dispatch(actions.DeleteExercise({exerciseId: actionObject.exerciseId}))
    }
  }
}
