import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { DebouncePreventClickDirective } from "../directives/debouncePreventClick.directive";
import { ThrottleTimePreventClickDirective } from "../directives/throtteTimePreventClick.directive";
@NgModule({
  imports: [HttpClientModule, FormsModule, ReactiveFormsModule],
  declarations: [
    ThrottleTimePreventClickDirective,
    DebouncePreventClickDirective,
  ],
  exports: [
    ThrottleTimePreventClickDirective,
    DebouncePreventClickDirective,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
})
export class SharedModule {}
