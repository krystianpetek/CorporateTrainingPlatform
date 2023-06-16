import { TestBed } from '@angular/core/testing';

import {
  IStorageService,
  BaseStorageService,
} from './models/base-storage.service';

describe('StorageService', () => {
  let service: IStorageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BaseStorageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
