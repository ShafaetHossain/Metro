export interface TabModel{
  routerLink : string,
  title: string,
  routeKey:string
}

export interface Tab{
  tabs: TabModel[]
  activeTab: TabModel
}

