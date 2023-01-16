import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IActionHandler } from './IActionHandler';
import { IExercise } from './IExercise';

@Component({
  selector: 'app-exercise-card',
  templateUrl: './exercise-card.component.html',
  styleUrls: ['./exercise-card.component.css']
})
export class ExerciseCardComponent implements OnInit{
  @Input() exercise!: IExercise;

  @Output() handler: EventEmitter<IActionHandler> = new EventEmitter<IActionHandler>();

  imageSrc: string = ''

  ngOnInit(): void {
    this.imageSrc = `https://localhost:7132/Image/getImageById?id=${this.exercise.idImage}`
  }

  approveHandler = () => {
    this.handler.emit({isApproved: true, exerciseId: this.exercise.exerciseId} as IActionHandler)
  }

  deleteHandler = () => {
    this.handler.emit({isApproved: false, exerciseId: this.exercise.exerciseId} as IActionHandler)
  }
  
}
