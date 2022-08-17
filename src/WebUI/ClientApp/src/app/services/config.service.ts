import { Injectable } from "@angular/core";
import { ConfigsDto, SystemClient } from "src/app/web-api-client";
import { tap } from "rxjs";
import { JSEncrypt } from "jsencrypt";
@Injectable({
  providedIn: "root",
})
export class ConfigService {
  systemConfig: ConfigsDto;
  constructor(private systemClient: SystemClient) {}
  loadConfigs() {
    return this.systemClient.getConfigs().pipe(
      tap((config) => {
        this.systemConfig = config.data;
      })
    );
  }
  encrypt(text: string) {
    var $encrypt = new JSEncrypt();
    $encrypt.setPublicKey(this.systemConfig.publicKeyEncode);
    return $encrypt.encrypt(text).toString();
  }
}
