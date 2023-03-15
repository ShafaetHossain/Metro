import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Store } from '@ngxs/store';
import { AddNewTab } from './tab-management.action';

@Injectable({
  providedIn: 'root'
})
export class RoutingService {

  constructor(
    private router: Router,
    private store: Store
    ) { }

    navigate(url: string, title: string, isNewTab = true) {
      this.router.navigate([url]);
      if (isNewTab) {
        this.store.dispatch(
          new AddNewTab({
            title: title,
            routerLink: url,
            routeKey: this.router.url,
          })
        );
      }
    }
}
