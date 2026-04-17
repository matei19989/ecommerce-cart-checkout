import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-register',
  imports: [FormsModule, RouterLink],
  templateUrl: './register.html'
})
export class Register {
  name = '';
  email = '';
  password = '';
  error = signal('');

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    this.authService.register({ name: this.name, email: this.email, password: this.password }).subscribe({
      next: () => this.router.navigate(['/products']),
      error: (err) => this.error.set(err.error?.message || 'Registration failed')
    });
  }
}