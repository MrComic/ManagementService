import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';
declare var $ :any;

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => {
    console.log(err);
    $('body').append(`
      <div class="container">
          <div class="row">
            <div class="col-lg-12">
            <br/><br/>
                <div class="alert alert-danger" role="alert">
                            مشکلی در راه اندازی برنامه وجود دارد
                </div>
            </div>
          </div>
      </div>
    `)
  });
