import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing-module';
import { ApiModule } from '../api';
import {RouterModule} from '@angular/router';
import {NzLayoutModule} from 'ng-zorro-antd/layout';
import {NzMenuModule} from 'ng-zorro-antd/menu';
import {NzIconModule} from 'ng-zorro-antd/icon';
import {NzToolTipModule} from 'ng-zorro-antd/tooltip';
import {NzButtonModule} from 'ng-zorro-antd/button';
import {MainComponent} from './main';
import {DashboardComponent} from './dashboard';
import {NzCardModule} from 'ng-zorro-antd/card';
import {NzSpinModule} from 'ng-zorro-antd/spin';
import {NzGridModule} from 'ng-zorro-antd/grid';
import {NzPaginationModule} from 'ng-zorro-antd/pagination';
import {NzStatisticModule} from 'ng-zorro-antd/statistic';
import { CreateDeviceComponent } from './create-device/create-device.component';

@NgModule({
  imports: [
    CommonModule,
    DashboardRoutingModule,
    RouterModule,
    NzLayoutModule,
    NzMenuModule,
    NzIconModule,
    NzButtonModule,
    NzToolTipModule,
    NzCardModule,
    NzSpinModule,
    NzGridModule,
    NzPaginationModule,
    NzStatisticModule,
  ],
  declarations:[
    MainComponent,
    DashboardComponent,
    CreateDeviceComponent,
  ],
})
export class DashboardModule { }
