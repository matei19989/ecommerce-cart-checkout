import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, switchMap, tap } from 'rxjs';
import { CartItem, CartItemDto } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiUrl = 'http://localhost:5284/api/cart';

  private cartItems = new BehaviorSubject<CartItem[]>([]);
  cartItems$ = this.cartItems.asObservable();

  constructor(private http: HttpClient) {}

  loadCart() {
    return this.http.get<CartItem[]>(this.apiUrl)
      .pipe(tap(items => this.cartItems.next(items)));
  }

  addToCart(productId: number, quantity: number = 1) {
    return this.http.post(this.apiUrl, { productId, quantity } as CartItemDto)
      .pipe(switchMap(() => this.loadCart()));
  }

  updateQuantity(productId: number, quantity: number) {
    return this.http.put(`${this.apiUrl}/${productId}`, { productId, quantity } as CartItemDto)
      .pipe(switchMap(() => this.loadCart()));
  }

  removeFromCart(productId: number) {
    return this.http.delete(`${this.apiUrl}/${productId}`)
      .pipe(switchMap(() => this.loadCart()));
  }

  clearLocal() {
    this.cartItems.next([]);
  }

  getItemCount(): number {
    return this.cartItems.value.reduce((sum, item) => sum + item.quantity, 0);
  }
}