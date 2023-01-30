import { IExercise } from '../IExercise';

export interface IState {
    exercises: IExercise[]
}

export const initialState = {
  exercises: [] as IExercise[]
} as IState;
