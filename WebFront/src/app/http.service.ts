import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { InventoryItem } from './Models/InventoryItem';
import { Product } from './Models/Product';
import { Store } from './Models/Store';


@Injectable({
  providedIn: 'root'
})
export class HttpService {
  http: HttpClient;
  constructor( http: HttpClient ) { 
    this.http = http;
  }
  getAllStores(): Observable<any> {
    return this.http.get<Store>('https://localhost:7255/api/Stores');
  }
}
