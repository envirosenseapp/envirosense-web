import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AuthService ,SessionService} from './services';
import { ApiHttpClient } from './http';

@NgModule({
  imports: [
    HttpClientModule,
  ],
  providers: [
    AuthService,
    SessionService,
    ApiHttpClient,
  ],
  exports: [
    HttpClientModule,
  ],
})
export class ApiModule { }
