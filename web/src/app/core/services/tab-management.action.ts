import { TabModel } from "./tab-management.model";

export class FetchAllTab {
  static readonly type = '[TabManagement] Fetch All';
}
export class GetTabCount{
  static readonly type = '[TabManagement] Tab Count';
}

export class AddNewTab {
  static readonly type = '[TabManagement] Add new tab';
  constructor(public payload: TabModel) {}
}

export class RemoveTab {
  static readonly type = '[TabManagement] Remove Tab';
  constructor(public payload: string) {}
}

export class InactiveAllTabs{
  static readonly type = '[TabManagement] Inactive All Tab';
}