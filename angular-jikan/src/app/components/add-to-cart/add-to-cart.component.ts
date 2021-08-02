import { Component, Input, OnInit } from '@angular/core';
import { Watch } from 'src/app/models/Watch';
import { CartService } from 'src/app/service/cart.service';

@Component({
  selector: 'app-add-to-cart',
  templateUrl: './add-to-cart.component.html',
  styleUrls: ['./add-to-cart.component.css']
})
export class AddToCartComponent implements OnInit {

  @Input() watch : Watch;
  quantity : number = 1;

  constructor(private cartService : CartService) { }

  ngOnInit(): void {
  }

  /**
   * Adds a Watch and associated quantity to the cart.
   *
   * @param item - The Watch to add to cart.
   *
   */
  addToCart(item : Watch): void {
    this.cartService.addToCart(item, this.quantity);
  }

  /**
   * Changes the quantity of a Watch.
   *
   * @param quantityStr - The string quantity of the Watch being added.
   *
   */
  changeQuantity(quantityStr : string): void {
    const quantityNum = parseInt(quantityStr);
    this.quantity = quantityNum;
  }

}
