import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { MetroRoutingModule } from './metro-routing.module';

import {
  ButtonGroupModule,
  ButtonModule,
  CardModule,
  CollapseModule,
  DropdownModule,
  FormModule,
  GridModule,
  NavbarModule,
  NavModule,
  SharedModule,
  UtilitiesModule
} from '@coreui/angular';

import { IconModule } from '@coreui/icons-angular';
import { StationComponent } from './pages/station/station.component';
import { ScheduleComponent } from './pages/schedule/schedule.component';
import { ScheduleAddEditComponent } from './pages/schedule/schedule-add-edit/schedule-add-edit.component';
import { StationAddEditComponent } from './pages/station/station-add-edit/station-add-edit.component';

@NgModule({
  declarations: [
    
  
    StationComponent,
    ScheduleComponent,
    ScheduleAddEditComponent,
    StationAddEditComponent
  ],
  imports: [
    CommonModule,
    MetroRoutingModule,
    ButtonModule,
    ButtonGroupModule,
    GridModule,
    IconModule,
    CardModule,
    UtilitiesModule,
    DropdownModule,
    SharedModule,
    FormModule,
    ReactiveFormsModule,
    NavbarModule,
    CollapseModule,
    NavModule,
    NavbarModule
  ]
})
export class MetroModule {
}
