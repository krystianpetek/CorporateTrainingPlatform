import { Injectable } from '@angular/core';

/**
 *  Interface for storage service base
 */
export interface IStorageService {
  cleanStorage(): void;
  getKey<T>(key: string): T;
  setKey<T>(key: string, value: T): void;
}

/**
 *  Abstract class for storage service
 */
@Injectable({ providedIn: 'root' })
export abstract class StorageServiceBase implements IStorageService {
  abstract cleanStorage(): void;
  abstract getKey<T>(key: string): T;
  abstract setKey<T>(key: string, value: T): void;
}

@Injectable({
  providedIn: 'root',
})
export class StorageService extends StorageServiceBase {
  public static readonly USER_KEY = 'authenticated-user';
  public static readonly JWT_KEY = 'access-token';

  public override getKey<T>(key: string): T {
    const value: T | null = window.sessionStorage.getItem(key) as T;
    const deserializedValue: T = JSON.parse(value as string);
    return deserializedValue;
  }
  public override setKey<T>(key: string, value: T): void {
    window.sessionStorage.removeItem(key);
    const serializedJson: string = JSON.stringify(value);
    window.sessionStorage.setItem(key, serializedJson);
  }

  public override cleanStorage(): void {
    window.sessionStorage.clear();
  }
}
