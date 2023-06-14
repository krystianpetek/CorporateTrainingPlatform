import { TestBed } from '@angular/core/testing';

import { AuthenticationService, AuthenticationServiceBase } from './authentication.service';

describe('AuthenticationService', () => {
  let service: AuthenticationServiceBase;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthenticationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
