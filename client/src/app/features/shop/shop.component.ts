import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Pagination } from '../../shared/models/pagination';
import { Product } from '../../shared/models/product';
import { MatCard } from '@angular/material/card';

@Component({
  selector: 'app-shop',
  imports: [MatCard],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
  private shopService = inject(ShopService);
  pagination: Pagination<Product> = { pageIndex: 0, pageSize: 0, count: 0, items: [] };

  ngOnInit(): void {
    this.shopService.getProducts().subscribe({
      next: response => { this.pagination = response.value; console.log(response) },
      error: error => console.error(error),
      complete: () => console.log('complete')
    });
  }
}
