import { TestBed } from '@angular/core/testing';

import { SignalRService } from './signal-r.service';

describe('SignalrService', () => {
  let service: SignalRService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SignalRService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
