import { Component, Input, OnInit } from "@angular/core";
import { Menu } from "src/app/models/menu.model";

@Component({
  selector: "app-slider-menu",
  templateUrl: "./slider-menu.component.html",
  styleUrls: ["./slider-menu.component.scss"],
})
export class SliderMenuComponent implements OnInit {
  @Input() menu: Menu = [];
  constructor() {}

  ngOnInit(): void {}
  onRedirect(link: string, isNewWindow: boolean) {
    if(isNewWindow)
      window.open(link, "_blank");
    else
      window.location.href = link;
  }
}
