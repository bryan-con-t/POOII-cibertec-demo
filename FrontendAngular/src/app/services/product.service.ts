import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Product } from '../models/product';
import { ProductResponse } from '../models/product-response';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private apiUrl = 'http://localhost:7031/api/products';

  constructor(private http: HttpClient) {}

  listar(): Observable<Product[]> {
    return this.http
      .get<ProductResponse>(this.apiUrl)
      .pipe(map(response => response.data));
  }
}
