import { Component, OnInit , signal} from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import { ProductService } from '../../services/product';
import { CartService } from '../../services/cart';
import { AuthService } from '../../services/auth';
import { Router } from '@angular/router';
import { Product } from '../../models/models';

@Component({
  selector: 'app-product-list',
  imports: [CurrencyPipe],
  templateUrl: './product-list.html'
})
export class ProductList implements OnInit {
  products = signal<Product[]>([]);
  loading = signal(true);
  error = signal('');
  readonly apiHost = 'http://localhost:5284';

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.loading.set(true);
    this.productService.getAll().subscribe({
      next: (products) => {
        this.products.set(products);
        this.loading.set(false);
      },
      error: () => {
        this.error.set('Failed to load products');
        this.loading.set(false);
      }
    });
  }

  addToCart(productId: number) {
    if (!this.authService.getToken()) {
      this.router.navigate(['/login']);
      return;
    }
    this.cartService.addToCart(productId, 1).subscribe();
  }
}