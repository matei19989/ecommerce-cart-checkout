export interface Product {
  id: number;
  name: string;
  description: string | null;
  price: number;
  imageUrl: string | null;
  category : string | null;
  inStock : boolean;
}

export interface CartItem {
    id : number;
    productId : number;
    productName : string;
    quantity : number;
    price : number;
    imageUrl : string | null;
}

export interface RegisterRequest {
    username : string;
    email : string;
    password : string;
}

export interface LoginRequest {
    email : string;
    password : string;
}

export interface AuthResponse {
    token : string;
    name : string;
    email : string;
}

export interface CheckoutRequest {
    shippingAddress : string;
}

export interface Order {
    id : number;
    createdAt : string;
    totalPrice : number;
    items : OrderItem[];
    shippingAddress : string;
}

export interface OrderItem {
    id: number;
    orderId: number;
    productId: number;
    quantity: number;
    unitPrice : number;
}