import {Component, inject} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {SignUpRequest, AuthService, SignInRequest, SessionService} from '../../api';
import {ComponentWithLoading, loadingAndErrorPipe} from '../../../core';
import {Router} from '@angular/router';
import {NzMessageService} from 'ng-zorro-antd/message';

@Component({
  selector: 'app-guest-sign-up',
  standalone: false,
  templateUrl: './sign-up.component.html',
})
export class SignUpComponent extends ComponentWithLoading {
  private readonly fb = inject(FormBuilder);
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly messageService = inject(NzMessageService);
  form: FormGroup;

  constructor() {
    super();
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', [Validators.required]],
      agree: [false, [Validators.requiredTrue]],
    }, { validators: this.passwordsMatch });
  }

  private passwordsMatch(group: FormGroup) {
    const pass = group.get('password')?.value;
    const confirm = group.get('confirmPassword')?.value;
    return pass === confirm ? null : { notSame: true };
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    console.log('Sign up', this.form.value);
    const credentials: SignUpRequest = {
      email: this.form.value.email,
      password: this.form.value.password,
      confirmPassword: this.form.value.confirmPassword
    };
    this.startLoading();
    this.authService.signUp(credentials).pipe(
      loadingAndErrorPipe(this)
    )
    .subscribe(
      this.handleSignUpSuccess.bind(this),
    )
  }
  private handleSignUpSuccess(): void {
    // Show success notification
    this.messageService.success(
      'You have successfully signed up to EnviroSense.'
    );
    // Redirect to dashboard
    this.router.navigate(['/sign-in']);
  }
}
