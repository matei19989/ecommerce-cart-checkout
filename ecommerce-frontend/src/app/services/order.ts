import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CheckoutRequest, Order } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiUrl = 'http://localhost:5284/api/orders';

  constructor(private http: HttpClient) {}

  checkout(shippingAddress: string) {
    return this.http.post<Order>(`${this.apiUrl}/checkout`, { shippingAddress } as CheckoutRequest);
  }

  getOrders() {
    return this.http.get<Order[]>(this.apiUrl);
  }
}