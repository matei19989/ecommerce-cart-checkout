import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';

import { AuthService } from './auth';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    localStorage.clear();
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()]
    });
    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
    localStorage.clear();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should store token and flip isLoggedIn$ on login', () => {
    let loggedIn = false;
    service.isLoggedIn$.subscribe(v => loggedIn = v);

    service.login({ email: 'a@b.com', password: 'pw' }).subscribe();
    httpMock.expectOne('http://localhost:5284/api/auth/login')
      .flush({ token: 'abc', name: 'Test', email: 'a@b.com' });

    expect(service.getToken()).toBe('abc');
    expect(loggedIn).toBe(true);
  });

  it('should clear token and flip isLoggedIn$ on logout', () => {
    service.login({ email: 'a@b.com', password: 'pw' }).subscribe();
    httpMock.expectOne('http://localhost:5284/api/auth/login')
      .flush({ token: 'abc', name: 'Test', email: 'a@b.com' });

    let loggedIn = true;
    service.isLoggedIn$.subscribe(v => loggedIn = v);

    service.logout();

    expect(service.getToken()).toBeNull();
    expect(loggedIn).toBe(false);
  });
});
