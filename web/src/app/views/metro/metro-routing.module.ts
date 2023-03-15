import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ScheduleAddEditComponent } from './pages/schedule/schedule-add-edit/schedule-add-edit.component';
import { ScheduleComponent } from './pages/schedule/schedule.component';
import { StationComponent } from './pages/station/station.component';

const routes: Routes = [
    {
      path: '',
      data: {
        title: 'Metro'
      },
      children: [
        {
          path: 'station',
          component: StationComponent,
          data: {
            title: 'Stations',
            reuse: false
          }
        },
        {
          path: 'schedule',
          component: ScheduleComponent,
          data: {
            title: 'Schedules',
            reuse: false
          }
        },
        {
          path: 'schedule/add',
          component: ScheduleAddEditComponent,
          data: {
            title: 'Add Schedules',
            reuse: false
          }
        },
        {
          path: 'schedule/edit/:id',
          component: ScheduleAddEditComponent,
          data: {
            title: 'Edit Schedules',
            reuse: false
          }
        },
      ]
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MetroRoutingModule {}