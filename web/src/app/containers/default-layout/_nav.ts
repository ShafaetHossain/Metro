import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Dashboard',
    url: '/dashboard',
    iconComponent: { name: 'cil-speedometer' },
    badge: {
      color: 'info',
      text: 'NEW'
    }
  },
  {
    name: 'Components',
    title: true
  },
  {
    name: 'Metro',
    url: '/metro',
    iconComponent: { name: 'cil-bell' },
    children: [
      {
        name: 'Station',
        url: '/metro/station'
      },
      {
        name: 'Schedule',
        url: '/metro/schedule'
      }
    ]
  },
];
