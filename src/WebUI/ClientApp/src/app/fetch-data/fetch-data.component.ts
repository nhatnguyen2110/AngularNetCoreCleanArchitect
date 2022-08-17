import { Component } from '@angular/core';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: [];

  constructor() {
    // client.get().subscribe(result => {
    //   this.forecasts = result;
    // }, error => console.error(error));
  }
}
