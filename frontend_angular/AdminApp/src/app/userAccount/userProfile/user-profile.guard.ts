import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserProfileGuard implements CanActivate {
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    const params = new URLSearchParams(location.search);
    const token = params?.get('token');

    const storageToken = sessionStorage.getItem('token');

    if (!token && !storageToken) {
      alert('Not authenticated');
      location.href = 'https://localhost:3000';
      return false;
    }

    if (storageToken == null) {
      sessionStorage.setItem('token', token!);
    }

    if (location.href != 'http://localhost:4200/user-profile') {
        location.href = 'http://localhost:4200/user-profile';
      }

    return true;
  }
  
}

