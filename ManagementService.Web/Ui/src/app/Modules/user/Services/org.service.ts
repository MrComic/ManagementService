import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {OrgModel} from '../Models/OrgModel';
import {Observable} from 'rxjs';

@Injectable()
export class OrgService {

  constructor(private http:HttpClient) {

  }


  GetOrgs():Observable<OrgModel[]>{
    return this.http.get<OrgModel[]>('/api/org/Get');
  }
}
