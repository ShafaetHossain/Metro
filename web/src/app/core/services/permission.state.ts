import { Action, Selector, State, StateContext } from '@ngxs/store';
import { SetPermission } from './permission.actions';
import { PermissionModel } from './permission.model';
@State<PermissionModel[]>({
  name: 'permissions',
  defaults: [],
})
export class PermissionState{

  @Action(SetPermission)
  setPermission(ctx:StateContext<PermissionModel[]>, payload: any){
    ctx.setState(payload.payload);
  }
}
