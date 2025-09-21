import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiHttpClient } from '../http';
import {PagedApiResult, Device, DeviceFilters, CreateDevice} from '../models';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DeviceService {
  private readonly http = inject(ApiHttpClient);

  constructor() { }

  create(create: CreateDevice): Observable<Device> {
    return this.http.post<Device>(`${environment.api}/devices`, create);
  }

  getById(id: string): Observable<Device> {
    return this.http.get<Device>(`${environment.api}/devices/`+id);
  }

  list(filters: DeviceFilters): Observable<PagedApiResult<Device>> {
    return this.http.get<PagedApiResult<Device>>(`${environment.api}/devices`, filters);
  }
}
