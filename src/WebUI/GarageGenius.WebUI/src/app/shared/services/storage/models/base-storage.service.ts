import { Injectable } from '@angular/core';

/**
 *  Interface for storage service base
 */
export interface IStorageService {
  deleteKey(key: string): boolean;
  getKey<T>(key: string): T;
  setKey<T>(key: string, value: T): boolean;
}

/**
 *  Abstract class for storage service
 */
@Injectable({ providedIn: 'root' })
export abstract class BaseStorageService implements IStorageService {
  public static readonly USER_KEY = 'authenticated-user';
  public static readonly JWT_KEY = 'access-token';
  abstract deleteKey(key: string): boolean;
  abstract getKey<T>(key: string): T;
  abstract setKey<T>(key: string, value: T): boolean;
}
