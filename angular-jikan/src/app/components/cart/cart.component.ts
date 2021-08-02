import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/service/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  items = this.cartService.getItems();
  cartFull : boolean = false;

  constructor(private cartService : CartService) { }

  ngOnInit(): void {
    this.cartFull = this.cartService.getItems().length > 0;
  }

  /**
   * Clears all items from the cart.
   */
  clearCart(): void {
    this.items = this.cartService.clearCart();
    this.cartFull = false;
  }

}
