import { Component, OnInit } from "@angular/core";
import { ConfigService } from "src/app/services/config.service";

@Component({
  selector: "app-token",
  templateUrl: "./token.component.html",
  styleUrls: ["./token.component.scss"],
})
export class TokenComponent implements OnInit {
  token: string;
  isCopied: boolean;
  constructor(private configService: ConfigService) {}

  ngOnInit(): void {
    this.isCopied = false;
    this.token = "Bearer " + this.configService.getAccessToken();
  }
  copyToClipboard(): void {
    const selBox = document.createElement("textarea");
    selBox.style.position = "fixed";
    selBox.style.left = "0";
    selBox.style.top = "0";
    selBox.style.opacity = "0";
    selBox.value = this.token;
    document.body.appendChild(selBox);
    selBox.focus();
    selBox.select();
    document.execCommand("copy");
    document.body.removeChild(selBox);
    this.isCopied = true;
  }
}
