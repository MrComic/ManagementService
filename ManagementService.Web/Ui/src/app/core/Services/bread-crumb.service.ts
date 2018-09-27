import {EventEmitter, Injectable, OnInit} from '@angular/core';
import {BreadCrumbModel} from '../Models/BreadCrumbModel';

@Injectable({
  providedIn: 'root'
})
export class BreadCrumbService {

  bmodel:BreadCrumbModel =new BreadCrumbModel();

  TitleChanged:EventEmitter<BreadCrumbModel> ;

  constructor() {
     this.bmodel.Title = 'خانه';
     this.bmodel.BreadCrumb = [{ title: 'خانه', route: '' }];
  }

  titleChanged(model:BreadCrumbModel)
  {
    this.TitleChanged.emit(model);
  }

  CreateEvent() {
    this.TitleChanged = new EventEmitter<BreadCrumbModel>();
  }
}
