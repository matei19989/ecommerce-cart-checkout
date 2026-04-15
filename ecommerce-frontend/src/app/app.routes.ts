import { Routes } from '@angular/router';
import { ProductList } from './components/product-list/product-list';
import { Cart } from './components/cart/cart';
import { Checkout } from './components/checkout/checkout';
import { Register } from './components/register/register';
import { Login } from './components/login/login';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: 'products', component: ProductList },
  { path: 'cart', component: Cart },
  { path: 'checkout', component: Checkout, canActivate: [authGuard] },
  { path: 'register', component: Register },
  { path: 'login', component: Login }
];