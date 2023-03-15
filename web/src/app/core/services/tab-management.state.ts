import { Injectable } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Action, Selector, State, StateContext } from "@ngxs/store";
import { environment } from "../../../environments/environment";
import { AddNewTab, InactiveAllTabs, RemoveTab } from "./tab-management.action";
import { Tab } from "./tab-management.model";

@State<Tab>({
  name: 'tabs',
  defaults: {
    tabs:[
      {
        routerLink:'/dashboard',
        title:'Dashboard',
        routeKey:'/dashboard'
      }
    ],
    activeTab:{
        routerLink:'dashboard',
        title:'Dashboard',
        routeKey:'/dashboard'
      }
  },
})


@Injectable()
export class TabState {
  env = environment;
  constructor(private route: ActivatedRoute){

  }
  @Selector()
  static tabCount(state: Tab) {
    return state.tabs;
  }
  @Action(InactiveAllTabs)
  inactiveAllTabs(ctx:StateContext<Tab>){
    let inactiveAll = {isActive:false};
  }
  @Action(AddNewTab)
  addNewTab(ctx:StateContext<Tab>, payload: any){
    const state = ctx.getState();
    let isExist = state.tabs.filter(x=>x.routerLink == payload.payload.routerLink).length;
      if(isExist > 0){
        ctx.setState({tabs:[...state.tabs],activeTab:payload.payload});
      }
      else{
        ctx.setState({tabs:[...state.tabs,payload.payload],activeTab:payload.payload});
      }
  }
}