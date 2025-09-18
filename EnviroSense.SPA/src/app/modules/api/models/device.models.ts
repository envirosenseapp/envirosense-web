import {BaseFilters} from './core.models';

export interface DeviceFilters extends BaseFilters {

}

export interface Device {
  id: string;
  name: string;
  updatedAt: Date;
  createdAt: Date;
}
