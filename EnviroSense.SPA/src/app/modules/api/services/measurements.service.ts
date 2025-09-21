import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiHttpClient } from '../http';
import {PagedApiResult, Device, DeviceFilters, CreateDevice, MeasurementFilters, Measurement} from '../models';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MeasurementsService {
  private readonly http = inject(ApiHttpClient);

  constructor() { }

  list(filters: MeasurementFilters): Observable<PagedApiResult<Measurement>> {
    return this.http.get<PagedApiResult<Measurement>>(`${environment.api}/measurements`, filters);
  }
}
