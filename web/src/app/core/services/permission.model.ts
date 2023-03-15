export interface PermissionModel {
  RoleId: string
  PermissionId: number
  ClientId: string
  Role: Role
  Permission: Permission
}

export interface Role {
  Id: string
  RoleName: string
  IsDeletable: boolean
  DisplayName: string
  RoleOrder: number
  Duration: string
  WorkingDayReopen: boolean
  Users: any
}

export interface Permission {
  Id: number
  Name: string
  DisplayName: string
  ApiResourceId: number
}
