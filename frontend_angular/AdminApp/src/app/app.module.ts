import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import {MatSidenavModule} from '@angular/material/sidenav';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import {MatListModule} from '@angular/material/list';
import { ExercisesComponent } from './shared/exercises.component';
import { SplitsComponent } from './shared/splits.component';
import {MatToolbarModule} from '@angular/material/toolbar';
import { PendingExercisesComponent } from './pendingExercises/pending-exercises.component';
import { HttpClientModule } from '@angular/common/http';
import { ExerciseCardComponent } from './pendingExercises/exercise-card.component';
import {MatButtonModule} from '@angular/material/button';
import { UserProfileComponent } from './userAccount/userProfile/user-profile.component';
import { EditProfileComponent } from './userAccount/editProfile/edit-profile.component';
import { UserAccountModule } from './userAccount/user-account.module';


@NgModule({
  declarations: [
    AppComponent,
    ExercisesComponent,
    SplitsComponent,
    PendingExercisesComponent,
    ExerciseCardComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatSidenavModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    MatListModule,
    MatToolbarModule,
    HttpClientModule,
    MatButtonModule,
    UserAccountModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
