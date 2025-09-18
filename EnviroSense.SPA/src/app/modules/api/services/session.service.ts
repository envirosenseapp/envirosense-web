import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  private readonly TOKEN_KEY = 'auth_token';

  constructor() { }

  public getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  public isSignedIn(): boolean {
    let token = this.getToken();

    return token != null && token != ""
  }

  public setToken(token: string): void {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  public clear(): void {
    localStorage.removeItem(this.TOKEN_KEY);
  }
}
