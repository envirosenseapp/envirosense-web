import { Injectable, inject } from '@angular/core';
import { ApiHttpClient } from '../http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import {SignInRequest, SignInResponse} from '../models';
import {SignUpRequest, SignUpResponse} from '../models';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http = inject(ApiHttpClient);

  signIn(credentials: SignInRequest): Observable<SignInResponse> {
    return this.http.post<SignInResponse>(
      `${environment.api}/auth/sign_in`,
      credentials
    );
  }
  signUp(credentials: SignUpRequest): Observable<SignUpResponse> {
    return this.http.post<SignUpResponse>(
      `${environment.api}/auth/sign_up`,
      credentials
    );
  }
}
