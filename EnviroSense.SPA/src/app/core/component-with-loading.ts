import {BaseComponent} from './base-component';
import {Directive, signal} from '@angular/core';
import {ErrorResponse} from '../modules/api';
import {Observable, tap} from 'rxjs';
import {HttpErrorResponse} from '@angular/common/http';

@Directive()
export abstract class ComponentWithLoading extends BaseComponent {
  protected isLoading = signal(false);
  protected errorMessage = signal<ErrorResponse | null>(null);

  public startLoading(): void {
    this.isLoading.set(true);
  }

  public stopLoading(): void {
    this.isLoading.set(false);
  }

  public setError(err: ErrorResponse | null) {
    this.errorMessage.set(err);
  }
}
