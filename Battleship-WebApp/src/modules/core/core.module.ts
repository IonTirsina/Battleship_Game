import { Inject, NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';


@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() @Inject(CoreModule) parentModule: CoreModule | null) {
    if (parentModule) {
      throw new Error(
        `CoreModule can only be imported into AppModule`);
    }
  }
}
