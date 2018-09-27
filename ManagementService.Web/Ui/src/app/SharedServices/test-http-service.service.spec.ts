import { TestBed, inject } from '@angular/core/testing';

import { TestHttpServiceService } from './test-http-service.service';

describe('TestHttpServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TestHttpServiceService]
    });
  });

  it('should be created', inject([TestHttpServiceService], (service: TestHttpServiceService) => {
    expect(service).toBeTruthy();
  }));
});
