import {Injectable} from '@angular/core';
import {HotToastService} from '@ngneat/hot-toast';

@Injectable({providedIn: 'root'})
export class AlertService {
  toastrService: HotToastService;

  constructor(private _toastrService: HotToastService) {
    this.toastrService = _toastrService;
  }

  success(message: string) {
    this._toastrService.success(message, {
      style: {
        border: '1px solid #ddd',
        padding: '16px',
        color: '#1E1E1E',
        background: '#E8F6F0',

      },
      position:'top-right',
      iconTheme: {
        primary: '#49CC90',
        secondary: '#FFFAEE',

      },
    });
  }

  error(message: string) {
    this._toastrService.error(message, {
      style: {
        border: '1px solid #ddd',
        padding: '16px',
        color: '#eb4039',
        background: '#ffc2b3',

      },
      position:'top-right',
      iconTheme: {
        primary: '#eb4039',
        secondary: '#FFFAEE',

      },
    });
  }
  
  errorDismissible(message: string) {
    this._toastrService.error(message, {
      style: {
        border: '1px solid #ddd',
        padding: '16px',
        color: '#eb4039',
        background: '#ffc2b3',

      },
      position:'top-right',
      autoClose: false,
      dismissible: true,
      iconTheme: {
        primary: '#eb4039',
        secondary: '#FFFAEE',
      },
    });
  }

  warningToast(message: string) {
    this._toastrService.warning(message, {
      style: {
        border: '1px solid #ddd',
        padding: '16px',
      },
      position:'top-right',
    });
  }

}
