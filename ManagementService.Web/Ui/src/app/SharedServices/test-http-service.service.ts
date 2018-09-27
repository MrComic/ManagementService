import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {map} from 'rxjs/internal/operators';

@Injectable()
export class TestHttpServiceService {

  constructor(private httpservice:HttpClient) {

  }

  getValues():Observable<Array<string>>{

    return this.httpservice.get<string[]>('/api/BaseData')

  }

}
