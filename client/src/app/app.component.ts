import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { ShopService } from './core/services/shop.service';
import { Pagination } from './shared/models/pagination';
import { Product } from './shared/models/product';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  private shopService = inject(ShopService);
  title = 'Skinet';
  pagination: Pagination<Product> = { pageIndex: 0, pageSize: 0, count: 0, items: [] };

  ngOnInit(): void {
    this.shopService.getProducts().subscribe({
      next: response => { this.pagination = response.value; console.log(response) },
      error: error => console.error(error),
      complete: () => console.log('complete')
    });
  }
}
