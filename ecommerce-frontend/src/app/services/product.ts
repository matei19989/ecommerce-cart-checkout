import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'http://localhost:5284/api/products';

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<Product[]>(this.apiUrl);
  }

  getById(id: number) {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }
}