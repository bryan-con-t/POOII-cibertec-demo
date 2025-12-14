import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Product } from '../../models/product';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.css',
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.productService.listar().subscribe({
      next: data => {
        console.log('Productos recibidos:', data);  // 👈 Verifica los datos
        this.products = data;
      },
      error: err => console.error('Error en la carga de productos:', err)
    });
  }
}
