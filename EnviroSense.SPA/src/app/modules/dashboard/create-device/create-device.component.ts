import {Component, inject, OnInit} from '@angular/core';
import {ComponentWithLoading, loadingAndErrorPipe} from '../../../core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {CreateDevice, DeviceService, SignInRequest} from '../../api';
import {NzNotificationService} from 'ng-zorro-antd/notification';
import {Router} from '@angular/router';
import {NzMessageService} from 'ng-zorro-antd/message';

@Component({
  selector: 'app-create-device',
  templateUrl: './create-device.component.html',
  standalone: false,
})
export class CreateDeviceComponent extends ComponentWithLoading  {
  private readonly fb = inject(FormBuilder);
  private readonly deviceService = inject(DeviceService);
  private readonly messageService = inject(NzMessageService);
  private readonly router = inject(Router);

  form: FormGroup;

  constructor() {
    super();
    this.form = this.fb.group({
      name: [null, [Validators.required, Validators.maxLength(50)]],
    });
  }

  public submitForm(): void {
    if (!this.form.valid) {
      this.form.markAllAsTouched();
      return
    }

    const createDevice: CreateDevice = {
      name: this.form.value.name,
    };

    this.startLoading();
    this.deviceService.create(createDevice)
      .pipe(
        loadingAndErrorPipe(this),
      )
      .subscribe(res => {
        this.messageService.success(
          'You have successfully create a new device.'
        );

        this.router.navigate(['/dashboard/devices',res.id]);
      });
  }
}
