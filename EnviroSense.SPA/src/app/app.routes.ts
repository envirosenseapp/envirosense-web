import { Routes } from '@angular/router';
import { signedInGuard, signedOutGuard } from './guards';

export const routes: Routes = [
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./modules/dashboard').then(m => m.DashboardModule),
    canActivate: [signedInGuard],
  },
  {
    path: '',
    loadChildren: () => import('./modules/guest').then(m => m.GuestModule),
    canActivate: [signedOutGuard],
  },
  { path: '**', redirectTo: '' },
];
