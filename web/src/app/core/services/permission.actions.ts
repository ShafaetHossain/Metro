import { PermissionModel } from "./permission.model";

export class SetPermission {
  static readonly type = '[Permissions] Fetch All';
  constructor(public payload: PermissionModel[]){}
}
