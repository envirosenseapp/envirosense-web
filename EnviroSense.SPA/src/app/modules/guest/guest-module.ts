import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GuestRoutingModule } from './guest-routing-module';
import {MainComponent} from './main';
import {HomeComponent} from './home';
import {SignInComponent} from './sign-in';
import {SignUpComponent} from './sign-up';
import {RouterLink, RouterOutlet} from '@angular/router';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzStatisticModule } from 'ng-zorro-antd/statistic';
import { ReactiveFormsModule } from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { ApiModule } from '../api';

@NgModule({
  declarations: [
    MainComponent,
    HomeComponent,
    SignInComponent,
    SignUpComponent,
  ],
  imports: [
    CommonModule,
    GuestRoutingModule,
    RouterOutlet,
    RouterLink,
    ReactiveFormsModule,
    NzInputModule,
    NzCheckboxModule,
    NzButtonModule,
    NzDividerModule,
    NzCardModule,
    NzStatisticModule,
    ApiModule,
  ],
})
export class GuestModule { }