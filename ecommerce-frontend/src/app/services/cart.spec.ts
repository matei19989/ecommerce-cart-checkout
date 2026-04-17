import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { CartService } from './cart';

describe('CartService', () => {
  let service: CartService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        provideHttpClient(),
        provideHttpClientTesting()
      ]
    });
    service = TestBed.inject(CartService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should start with empty cart', () => {
    expect(service.getItemCount()).toBe(0);
  });

  it('should load cart items from API', () => {
    const mockItems = [
      { id: 1, productId: 1, productName: 'Test', quantity: 2, price: 10, imageUrl: null },
      { id: 2, productId: 2, productName: 'Test 2', quantity: 3, price: 20, imageUrl: null }
    ];

    service.loadCart().subscribe(items => {
      expect(items.length).toBe(2);
    });

    const req = httpMock.expectOne('http://localhost:5284/api/cart');
    expect(req.request.method).toBe('GET');
    req.flush(mockItems);

    expect(service.getItemCount()).toBe(5);
  });

  it('should calculate item count correctly', () => {
    const mockItems = [
      { id: 1, productId: 1, productName: 'A', quantity: 4, price: 10, imageUrl: null },
      { id: 2, productId: 2, productName: 'B', quantity: 1, price: 5, imageUrl: null }
    ];

    service.loadCart().subscribe();
    httpMock.expectOne('http://localhost:5284/api/cart').flush(mockItems);

    expect(service.getItemCount()).toBe(5);
  });

  it('should clear cart locally', () => {
    const mockItems = [
      { id: 1, productId: 1, productName: 'A', quantity: 2, price: 10, imageUrl: null }
    ];

    service.loadCart().subscribe();
    httpMock.expectOne('http://localhost:5284/api/cart').flush(mockItems);
    expect(service.getItemCount()).toBe(2);

    service.clearLocal();
    expect(service.getItemCount()).toBe(0);
  });
});