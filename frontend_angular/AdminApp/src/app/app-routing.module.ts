import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PendingExerciseGuard } from './pendingExercises/pending-exercise.guard';
import { PendingExercisesComponent } from './pendingExercises/pending-exercises.component';
import { ExercisesComponent } from './shared/exercises.component';
import { SplitsComponent } from './shared/splits.component';

const routes: Routes = [
  // { path: '', redi },
  // { path: '**', redirectTo: 'welcome', pathMatch: 'full' },
  {path: 'exercises', component: ExercisesComponent},
  {path: 'splits', component: SplitsComponent},
  {path: 'pending-exercises', canActivate: [PendingExerciseGuard], component: PendingExercisesComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
