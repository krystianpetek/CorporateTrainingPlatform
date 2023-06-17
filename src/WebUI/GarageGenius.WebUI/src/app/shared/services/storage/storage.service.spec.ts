import { TestBed } from '@angular/core/testing';

import {
  IStorageService,
  BaseStorageService,
} from './models/base-storage.service';
import { StorageService } from './storage.service';

fdescribe('StorageService', () => {
  let service: IStorageService;
  const key = 'key';
  const value = 'value';

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StorageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('[getKey] should return null value when key in storage is not initialized', () => {
    expect(service.getKey(key)).toEqual(null);
  });

  it('[setKey, getKey] should set value and then return this value from storage', () => {
    service.setKey(key, value);
    expect(service.getKey(key)).toEqual(value);
  });

  it('[setKey, deleteKey, getKey] should set value then delete value and return empty value from storage', () => {
    service.setKey(key, value);
    expect(service.getKey(key)).toEqual(value);

    service.deleteKey(key);
    expect(service.getKey(key)).toEqual(null);
  });
});
