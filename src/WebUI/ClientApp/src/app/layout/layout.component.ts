import {
  Component,
  HostListener,
  OnInit,
  ViewEncapsulation,
} from "@angular/core";
import { Menu } from "../models/menu.model";

@Component({
  selector: "app-layout",
  templateUrl: "./layout.component.html",
  styleUrls: ["./layout.component.scss"],
  encapsulation: ViewEncapsulation.Emulated,
})
export class LayoutComponent implements OnInit {
  isCollapsed = false;
  title = "MY APPLICATION";
  currentYear = new Date().getFullYear();
  fullScreen: boolean = false;
  elem: any;
  document: any;
  fixHeader: boolean = false;
  menu: Menu = [
    {
      title: "Home",
      icon: "bar-chart",
      link: "/home",
    },
    {
      title: "Admin",
      icon: "setting",
      expanded: true,
      subMenu: [
        {
          title: "Locations",
          icon: "global",
          link: "/location-settings",
        },
        {
          title: "Users",
          icon: "team",
          link: "/users",
        },
      ],
    },
    {
      title: "Token",
      icon: "key",
      link: "/token",
    },
    {
      title: "API Swagger",
      icon: "api",
      link: "/api/index.html?url=/api/specification.json",
      isExternal: true,
      isOpenAsNewWindow: true,
    },
  ];
  constructor() {}

  ngOnInit(): void {
    this.elem = document.documentElement;
    this.document = document;
    
  }
  goFullScreen(fullScreen: boolean) {
    if (fullScreen) {
      this.openFullscreen();
    } else {
      this.closeFullscreen();
    }
  }

  openFullscreen() {
    if (this.elem.requestFullscreen) {
      this.elem.requestFullscreen();
    } else if (this.elem.mozRequestFullScreen) {
      /* Firefox */
      this.elem.mozRequestFullScreen();
    } else if (this.elem.webkitRequestFullscreen) {
      /* Chrome, Safari and Opera */
      this.elem.webkitRequestFullscreen();
    } else if (this.elem.msRequestFullscreen) {
      /* IE/Edge */
      this.elem.msRequestFullscreen();
    }
  }

  /* Close fullscreen */
  closeFullscreen() {
    if (this.document.exitFullscreen) {
      this.document.exitFullscreen();
    } else if (this.document.mozCancelFullScreen) {
      /* Firefox */
      this.document.mozCancelFullScreen();
    } else if (this.document.webkitExitFullscreen) {
      /* Chrome, Safari and Opera */
      this.document.webkitExitFullscreen();
    } else if (this.document.msExitFullscreen) {
      /* IE/Edge */
      this.document.msExitFullscreen();
    }
  }
  @HostListener("document:fullscreenchange", ["$event"])
  @HostListener("document:webkitfullscreenchange", ["$event"])
  @HostListener("document:mozfullscreenchange", ["$event"])
  @HostListener("document:MSFullscreenChange", ["$event"])
  fullScreenChange() {
    this.fullScreen = !this.fullScreen;
  }
  toggleFixHeader() {
    this.fixHeader = !this.fixHeader;
  }
}
