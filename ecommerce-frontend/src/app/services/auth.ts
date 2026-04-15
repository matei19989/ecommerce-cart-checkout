import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, tap } from 'rxjs';
import { AuthResponse, LoginRequest, RegisterRequest } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5284/api/auth';
  private tokenKey = 'auth_token';

  private loggedIn = new BehaviorSubject<boolean>(this.hasToken());

  isLoggedIn$ = this.loggedIn.asObservable();

  constructor(private http: HttpClient) {}

  register(request: RegisterRequest) {
    return this.http.post<AuthResponse>(`${this.apiUrl}/register`, request)
      .pipe(tap(res => this.storeToken(res)));
  }

  login(request: LoginRequest) {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, request)
      .pipe(tap(res => this.storeToken(res)));
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem('user_name');
    this.loggedIn.next(false);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getUserName(): string | null {
    return localStorage.getItem('user_name');
  }

  private hasToken(): boolean {
    return !!localStorage.getItem(this.tokenKey);
  }

  private storeToken(res: AuthResponse) {
    localStorage.setItem(this.tokenKey, res.token);
    localStorage.setItem('user_name', res.name);
    this.loggedIn.next(true);
  }
}