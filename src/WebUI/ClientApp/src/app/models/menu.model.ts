export interface MenuItem {
  title?: string;
  icon?: string;
  link?: string;
  expanded?: boolean;
  isExternal?: boolean;
  isOpenAsNewWindow?: boolean;
  subMenu?: MenuItem[];
}

export type Menu = MenuItem[];
