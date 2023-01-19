import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, tap, throwError } from 'rxjs';
import { IEditInfo } from './editProfile/IEditInfo';
import { IUserProfile } from './userProfile/IUserProfile';

@Injectable({
  providedIn: 'root',
})
export class UserAccountService {
  constructor(private http: HttpClient) {}

  private token: string | null = 'Bearer ' + sessionStorage.getItem('token');

  userInfo$ = this.http
    .get<IUserProfile>('https://localhost:7132/UserAccount/profilePage', {
      headers: {
        Authorization: `${this.token}`,
      },
    })
    .pipe(
      tap((data) => console.log('User object: ', JSON.stringify(data))),
      map((data) => data as IUserProfile),
      catchError(this.handleError)
    );

  editInfo$ = this.http
    .get<IEditInfo>('https://localhost:7132/UserAccount/getEditProfileModel', {
      headers: {
        Authorization: `${this.token}`,
      },
    })
    .pipe(
      tap((data) => console.log('Edit object: ', JSON.stringify(data))),
      map((data) => data as IEditInfo),
      catchError(this.handleError)
    );

  editProfile(user: IEditInfo): Observable<IEditInfo> {
    return this.http
      .post<IEditInfo>(
        'https://localhost:7132/UserAccount/editProfile',
        JSON.stringify(user),
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
