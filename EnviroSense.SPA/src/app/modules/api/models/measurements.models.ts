import {BaseFilters} from './core.models';

export interface Measurement {
  id: string;
  deviceId: string;
  temperature: number | null;
  humidity: number | null;
  recordingDate: Date;
  createdAt: Date;
}

export interface MeasurementFilters extends BaseFilters {
  deviceId: string | null;
}
