import { Injectable } from '@angular/core';
import { catchError, merge, Observable, scan, Subject, tap, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { IExercise } from './IExercise';

@Injectable({
  providedIn: 'root',
})
export class PendingExercisesService {
  constructor(private http: HttpClient) {
    const params = new URLSearchParams(location.search);
    var jwtToken = params?.get('token');
    if (jwtToken) {
      this.token = this.token + jwtToken;
    }
  }

  token: string | null = 'Bearer ' + new URLSearchParams(location.search)?.get('token');

  pendingExercises$ = this.http
    .get<IExercise[]>('https://localhost:7132/Admin/getPendingExercises', {
      headers: {
        Authorization: `${this.token}`,
      },
    })
    .pipe(
      tap((data) => console.log('All: ', JSON.stringify(data))),
      catchError(this.handleError)
    );

  private updateExerciseSubject = new Subject<string>();
  updateExerciseAction$ = this.updateExerciseSubject.asObservable();

  updatedPendingExercises$ = merge(
    this.pendingExercises$,
    this.updateExerciseAction$
  ).pipe(
    scan((acc, value) =>
      (value instanceof Array) ? [...value] : acc.filter(ex => ex.exerciseId != value), [] as IExercise[])
  )
  
  updateExercise(exerciseId: string):void{
    this.updateExerciseSubject.next(exerciseId);
  }

  approveExercise(exerciseId: string): Observable<string> {
    return this.http
      .post<string>(
        'https://localhost:7132/Exercises/approve',
        JSON.stringify(exerciseId),
        {
          headers: {
            'Content-Type': 'application/json',
            Authorization: `${this.token}`,
          },
        }
      )
      .pipe(
        tap((data) => console.log('All: ', JSON.stringify(data))),
        catchError(this.handleError)
      );
  }

  deleteExercise(exerciseId: string): Observable<string> {
    return this.http
      .post<string>(
        'https://localhost:7132/Exercises/reject',
        JSON.stringify(exerciseId),
        {
          headers: {
            'Content-Type': 'application/json',
          },
        }
      )
      .pipe(
        tap((data) => console.log('All: ', JSON.stringify(data))),
        catchError(this.handleError)
      );
  }

  private handleError(err: HttpErrorResponse): Observable<never> {
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(() => errorMessage);
  }
}
