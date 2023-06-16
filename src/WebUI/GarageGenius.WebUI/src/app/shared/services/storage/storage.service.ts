import { Injectable } from '@angular/core';
import { BaseStorageService } from './models/base-storage.service';

@Injectable({
  providedIn: 'root',
})
export class StorageService extends BaseStorageService {
  private storage: Storage;

  constructor() {
    super();
    this.storage = window.sessionStorage;
  }

  public override getKey<T>(key: string): T {
    const getValue: string | null = this.storage.getItem(key);
    const parsedValue: T = JSON.parse(getValue as string);
    return parsedValue;
  }

  public override setKey<T>(key: string, value: T): boolean {
    this.storage.removeItem(key);
    const serializedJson: string = JSON.stringify(value);
    this.storage.setItem(key, serializedJson);
    if (this.storage.getItem(key) === serializedJson) {
      return true;
    }
    return false;
  }

  public override deleteKey(key: string): boolean {
    this.storage.removeItem(key);
    if (this.storage.getItem(key) === null) {
      return true;
    }
    return false;
  }
}
