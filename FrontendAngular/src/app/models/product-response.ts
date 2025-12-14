import { Product } from './product';

export interface ProductResponse {
  data: Product[];
  total: number;
}
