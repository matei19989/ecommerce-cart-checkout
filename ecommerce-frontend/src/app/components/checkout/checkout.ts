import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { OrderService } from '../../services/order';
import { CartService } from '../../services/cart';

@Component({
  selector: 'app-checkout',
  imports: [FormsModule],
  templateUrl: './checkout.html'
})
export class Checkout {
  shippingAddress = '';
  error = '';
  success = false;

  constructor(
    private orderService: OrderService,
    private cartService: CartService,
    public router: Router
  ) {}

  placeOrder() {
    if (!this.shippingAddress.trim()) {
      this.error = 'Shipping address is required.';
      return;
    }

    this.orderService.checkout(this.shippingAddress).subscribe({
      next: () => {
        this.success = true;
        this.cartService.clearLocal();
      },
      error: (err) => this.error = err.error?.message || 'Checkout failed'
    });
  }
}