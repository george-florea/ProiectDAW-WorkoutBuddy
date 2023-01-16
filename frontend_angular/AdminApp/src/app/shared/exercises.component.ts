import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-exercises',
  templateUrl: './exercises.component.html',
})
export class ExercisesComponent implements OnInit {
  ngOnInit() {
    window.location.href = "https://localhost:3000/exercises";
  }
}
