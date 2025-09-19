import { Component } from '@angular/core';
import { ComponentWithLoading } from '../../../core';

@Component({
  selector: 'app-create-device',
  templateUrl: './create-device.component.html',
  standalone: false,
})
export class CreateDeviceComponent extends ComponentWithLoading {

  constructor() {
    super();
  }

}
