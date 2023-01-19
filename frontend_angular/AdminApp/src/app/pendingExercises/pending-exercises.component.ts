import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscriber, Subscription } from 'rxjs';
import { IActionHandler } from './IActionHandler';
import { IExercise } from './IExercise';
import { PendingExercisesService } from './pending-exercises.service';

@Component({
  templateUrl: './pending-exercises.component.html',
  styleUrls: ['./pending-exercises.component.css'],
})
export class PendingExercisesComponent implements OnInit, OnDestroy {
  constructor(private service: PendingExercisesService) {}

  sub$!: Subscription;
  exercises: IExercise[] = []

  ngOnInit(){
    this.sub$ = this.service.updatedPendingExercises$.subscribe({
      next: exercises => this.exercises = exercises,
    });
  }

  ngOnDestroy(): void {
    this.sub$.unsubscribe();
  }

  actionHandler(actionObject: IActionHandler): void {
      if (actionObject.isApproved) {
        this.service
          .approveExercise(actionObject.exerciseId)
          .subscribe({
            error: err => location.href = 'https://localhost:3000'
          });
      } else {
        this.service
          .deleteExercise(actionObject.exerciseId)
          .subscribe((res) => console.log(res));
      }
      this.service.updateExercise(actionObject.exerciseId);
    
  }
}
