import { Component, OnInit, signal } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { CartService } from '../../services/cart';
import { AuthService } from '../../services/auth';
import { CartItem } from '../../models/models';

@Component({
  selector: 'app-cart',
  imports: [CurrencyPipe, RouterLink],
  templateUrl: './cart.html'
})
export class Cart implements OnInit {
  items = signal<CartItem[]>([]);
  loading = signal(true);

  constructor(
    private cartService: CartService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    if (!this.authService.getToken()) {
      this.router.navigate(['/login']);
      return;
    }
    this.loadCart();
  }

  loadCart() {
    this.cartService.loadCart().subscribe({
      next: (items) => {
        this.items.set(items);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  updateQty(productId: number, qty: number) {
    if (qty < 1) {
      this.remove(productId);
      return;
    }
    this.cartService.updateQuantity(productId, qty).subscribe(() => this.loadCart());
  }

  remove(productId: number) {
    this.cartService.removeFromCart(productId).subscribe(() => this.loadCart());
  }

  getTotal(): number {
    return this.items().reduce((sum, item) => sum + item.price * item.quantity, 0);
  }
}