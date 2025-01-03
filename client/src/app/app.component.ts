import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./layout/header/header.component";
import { HttpClient } from '@angular/common/http';
import { Product } from './shared/models/product';
import { Pagination } from './shared/models/pagination';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  baseUrl = 'https://localhost:5001/api/'
  private http = inject(HttpClient)
  title = 'Skinet';
  pagination: Pagination<Product> = { pageIndex: 0, pageSize: 0, count: 0, items: [] };

  ngOnInit(): void {
    this.http.get<any>(this.baseUrl + 'products').subscribe({
      next: response => { this.pagination = response.value; console.log(response) },
      error: error => console.error(error),
      complete: () => console.log('complete')
    });
  }
}
