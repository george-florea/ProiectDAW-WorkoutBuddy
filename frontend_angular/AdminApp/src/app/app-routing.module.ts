import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PendingExerciseGuard } from './pendingExercises/pending-exercise.guard';
import { PendingExercisesComponent } from './pendingExercises/pending-exercises.component';
import { ExercisesComponent } from './shared/exercises.component';
import { SplitsComponent } from './shared/splits.component';
import { EditProfileComponent } from './userAccount/editProfile/edit-profile.component';
import { EditProfileGuard } from './userAccount/editProfile/edit-profile.guard';
import { UserProfileComponent } from './userAccount/userProfile/user-profile.component';
import { UserProfileGuard } from './userAccount/userProfile/user-profile.guard';

const routes: Routes = [
  {path: 'exercises', component: ExercisesComponent},
  {path: 'splits', component: SplitsComponent},
  {path: 'pending-exercises', canActivate: [PendingExerciseGuard], component: PendingExercisesComponent},
  {path: 'user-profile',  canActivate: [UserProfileGuard], component: UserProfileComponent},
  {path: 'edit-profile', canActivate: [EditProfileGuard], component: EditProfileComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
