import {Component, Directive, OnDestroy} from '@angular/core';
import {Observable, Subject} from 'rxjs';

@Directive()
export abstract class BaseComponent implements OnDestroy {
  private readonly onDestroy: Subject<void> = new Subject<void>();
  protected readonly onDestroy$: Observable<void> = this.onDestroy;

  public ngOnDestroy(): void {
    this.onDestroy.next();
    this.onDestroy.complete();
  }
}
