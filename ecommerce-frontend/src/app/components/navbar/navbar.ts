import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import { AuthService } from '../../services/auth';
import { CartService } from '../../services/cart';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, AsyncPipe],
  templateUrl: './navbar.html'
})
export class Navbar {
  constructor(
    public authService: AuthService,
    public cartService: CartService
  ) {}

  logout() {
    this.authService.logout();
    this.cartService.clearLocal();
  }
}