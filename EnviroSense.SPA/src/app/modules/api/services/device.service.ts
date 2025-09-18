import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiHttpClient } from '../http';
import {PagedApiResult, Device, DeviceFilters} from '../models';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DeviceService {
  private readonly http = inject(ApiHttpClient);

  constructor() { }

  list(filters: DeviceFilters): Observable<PagedApiResult<Device>> {
    return this.http.get<PagedApiResult<Device>>(`${environment.api}/devices`, filters);
  }
}
