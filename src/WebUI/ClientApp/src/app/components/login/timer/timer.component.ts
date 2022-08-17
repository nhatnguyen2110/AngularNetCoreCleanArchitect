import {
  Component,
  Input,
  OnInit,
  Output,
  EventEmitter,
  OnDestroy,
} from "@angular/core";
import {
  map,
  Observable,
  of,
  range,
  Subscription,
  take,
  timer,
  zip,
} from "rxjs";

@Component({
  selector: "timer",
  template: `{{ timerValue }}s`,
  styles: [],
})
export class TimerComponent implements OnInit, OnDestroy {
  @Input() value: number;
  @Output("onComplete") timerOver: EventEmitter<any> = new EventEmitter<any>();
  private subscription: Subscription;
  timerValue: number = 0;
  constructor() {}
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngOnInit() {
    this.subscription = timer(0, 1000)
      .pipe(
        take(this.value),
        map((x) => this.value - x)
      )
      .subscribe({
        next: (seconds) => {
          //console.log(seconds);
          this.timerValue = seconds;
        },
        error: () => this.timerOver.emit("TIMER ERROR"),
        complete: () => this.timerOver.emit("TIMER OVER"),
      });
  }
}
