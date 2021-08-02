import { Component, Input, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { Watch } from 'src/app/models/Watch';
import { CartService } from 'src/app/service/cart.service';

@Component({
  selector: 'app-watch-cart-display',
  templateUrl: './watch-cart-display.component.html',
  styleUrls: ['./watch-cart-display.component.css']
})
export class WatchCartDisplayComponent implements OnInit {

  @Input() watch : Watch;
  quantity : number = 0;
  watchTotalPrice : number = 0;

  constructor(private cartService : CartService) { }

  ngOnInit(): void {
    this.quantity = this.cartService.quantities[this.cartService.indexOf(this.watch)];    
    this.watchTotalPrice = this.quantity * this.watch.price;
  }

  /**
   * Removes a Watch and associated quantity to the cart.
   *
   * @param toRemove - The Watch to remove from the cart.
   *
   */
  removeFromCart(toRemove : Watch): void {
    this.cartService.removeItem(toRemove);
  }

  /**
   * Changes the quantity of a watch in the cart and updates the calculated price.
   *
   * @param quantityStr - The string quantity of the Watch being added.
   *
   */
  changeQuantity(quantityStr : string): void {
    const quantityNum = parseInt(quantityStr);
    this.cartService.quantities[this.cartService.indexOf(this.watch)] = quantityNum;
    if(this.quantity < quantityNum) {
      this.cartService.addCount(quantityNum - this.quantity);
    }
    else if(this.quantity > quantityNum) {
      this.cartService.removeCount(this.quantity - quantityNum)
    }
    this.quantity = quantityNum;
    this.watchTotalPrice = quantityNum * this.watch.price;
  }

  


}
