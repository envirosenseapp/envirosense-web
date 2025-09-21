import {Component, inject} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {AuthService, SignInRequest, SessionService} from '../../api';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {ComponentWithLoading, loadingAndErrorPipe} from '../../../core';
import {NzMessageService} from 'ng-zorro-antd/message';

@Component({
  selector: 'app-guest-sign-in',
  standalone: false,
  templateUrl: './sign-in.component.html',
})
export class SignInComponent extends ComponentWithLoading {
  private readonly fb = inject(FormBuilder);
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly messageService = inject(NzMessageService);
  private readonly sessionService = inject(SessionService);

  form: FormGroup;

  constructor() {
    super();
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      remember: [true],
    });
  }

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }


    const credentials: SignInRequest = {
      email: this.form.value.email,
      password: this.form.value.password,
    };

    this.startLoading();
    this.authService.signIn(credentials)
      .pipe(
        loadingAndErrorPipe(this),
      )
      .subscribe(this.handleSignInSuccess.bind(this));
  }

  private handleSignInSuccess(response: { token: string }): void {
    // Store token
    this.sessionService.setToken(response.token);

    // Show success notification
    this.messageService.success(
      'You have successfully signed in to EnviroSense.'
    );

    // Redirect to dashboard
    this.router.navigate(['/dashboard']);
  }
}
