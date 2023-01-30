import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { ExercisesComponent } from '../shared/exercises.component';
import { SplitsComponent } from '../shared/splits.component';
import { ExerciseCardComponent } from './exercise-card.component';

import { PendingExercisesComponent } from './pending-exercises.component';
import { PendingExercisesEffects } from './state/pending-exercises.effects';
import { pendingExercisesReducer } from './state/pending-exercises.reducer';

@NgModule({
  imports: [
    CommonModule,
    MatSidenavModule,
    ReactiveFormsModule,
    MatListModule,
    MatToolbarModule,
    MatButtonModule,
    StoreModule.forFeature('pending-exercises', pendingExercisesReducer),
    EffectsModule.forFeature([PendingExercisesEffects])
  ],
  exports: [],
  declarations: [
    ExercisesComponent,
    SplitsComponent,
    PendingExercisesComponent,
    ExerciseCardComponent,
  ],
})
export class PendingExercisesModule {}
