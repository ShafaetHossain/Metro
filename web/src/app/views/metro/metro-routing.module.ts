import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
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
            title: 'Schedules'
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