import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import { AuthService } from '../../services/auth';
import { CartService } from '../../services/cart';
import { map } from 'rxjs';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, AsyncPipe],
  templateUrl: './navbar.html'
})
export class Navbar implements OnInit {
  itemCount$!: Observable<number>;

  constructor(
    public authService: AuthService,
    private cartService: CartService
  ) {
    this.itemCount$ = this.cartService.cartItems$.pipe(
      map(items => items.reduce((sum, item) => sum + item.quantity, 0))
    );
  }

  ngOnInit() {
    if (this.authService.getToken()) {
      this.cartService.loadCart().subscribe();
    }

    this.authService.isLoggedIn$.subscribe(loggedIn => {
      if (loggedIn) {
        this.cartService.loadCart().subscribe();
      }
    });
  }

  logout() {
    this.authService.logout();
    this.cartService.clearLocal();
  }
}