import { TestBed } from '@angular/core/testing';

import { RecolectorService } from './recolector.service';

describe('RecolectorService', () => {
  let service: RecolectorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RecolectorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
