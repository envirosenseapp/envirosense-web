import { Component, inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { SessionService } from '../../api';

@Component({
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  standalone: false,
})
export class MainComponent {
  private readonly sessionService = inject(SessionService);
  private readonly router = inject(Router);

  isCollapsed = false;

  signOut(): void {
    this.sessionService.clear();
    this.router.navigate(['/guest/sign-in']);
  }
}
