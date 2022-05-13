import { Component } from '@angular/core';
import { HttpService } from './http.service';
import { InventoryItem } from './Models/InventoryItem';
import { Product } from './Models/Product';
import { Store } from './Models/Store';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Store';
  displayFirstButton: boolean = true;
  displayChooseAStore: boolean = false;
  stores: Store[] = [];
  constructor(private http: HttpService){}
  
  getAllStores() {
    this.http.getAllStores().subscribe((res: Store[]) => {
      this.stores = res;
    })
  }
}