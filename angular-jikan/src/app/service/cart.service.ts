import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { Watch } from '../models/Watch';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  
  items : Watch[] = [];
  quantities : number[] = [];
  count = 0;
  private cartCount = new ReplaySubject<number>(1);
  cartCount$ = this.cartCount.asObservable();
  
  constructor() { }

  /**
   * Increments the cart item count and updates the observable cart count.
   * 
   * @param quantity - The number of items to increment.
   * 
   */
  addCount(quantity: number): void {
    this.count += quantity;
    this.cartCount.next(this.count);
  }

  /**
  * Decrements the cart item count and updates the observable cart count.
  * 
  * @param quantity - The number of items to decrement.
  * 
  */
  removeCount(quantity: number): void {
    if(this.count > 0) {
      this.count -= quantity;
    }
    this.cartCount.next(this.count);
  }

  /**
   * Resets the cart item count to zero and updates the observable cart count.
   */
  clearCount(): void {
    this.count = 0;
    this.cartCount.next(this.count);
  }

  /**
   * Gets the cart item count Observable.
   * 
   * @returns An Observable of the current cart item count.
   * 
   */
  getCount(): Observable<number> {
    return this.cartCount$;
  }

  /**
   * Adds a Watch and its associated quantity to the cart.
   * 
   * @param item - The Watch to be added.
   * @param quantity - The quantity for the associated watch to be added.
   * 
   */
  addToCart(item: Watch, quantity : number): void {
    this.items.push(item);
    this.quantities.push(quantity);
    this.addCount(quantity);
  }

  /**
   * Gets all of the items from the cart.
   * 
   * @returns A Watch array.
   * 
   */
  getItems(): Watch[] {
    return this.items;
  }

  /**
   * Gets all of the quantities from the cart.
   * 
   * @returns A number array.
   * 
   */
  getQuantities(): number[] {
    return this.quantities;
  }

  /**
   * Clears all items and quantities from the cart and resets the cart count.
   * 
   * @returns A Watch array.
   * 
   */
  clearCart(): Watch[] {
    this.items = [];
    this.quantities = [];
    this.clearCount();
    return this.items;
  }

  /**
   * Calculates the sub-total of the items in the cart.
   * 
   * @return The calculated sub-total.
   * 
   */
  getSubTotal(): number {
    var subTotal = 0;
    for(var i = 0; i < this.items.length; i++) {
      subTotal += this.items[i].price * this.quantities[i];
    }
    return subTotal;
  }

  /**
   * Calculates the tax of the items in the cart.
   * 
   * @return The calculated tax.
   * 
   */
  getTax(): number {
    return this.getSubTotal() * 0.07;
  }

  /**
   * Removes a Watch and its associated quantity and decrements the cart item count.
   * 
   * @param toRemove - The Watch to remove from the cart.
   * 
   */
  removeItem(toRemove : Watch): void {
    for(var i = 0; i < this.items.length; i++) {
      if(this.items[i].id == toRemove.id) {
        this.items.splice(i, 1);
        this.removeCount(this.quantities[i]);
        this.quantities.splice(i, 1);
      }
    }
  }

  /**
   * Gets the index of a Watch in the cart.
   * 
   * @param watch - The Watch to get the index of.
   * 
   * @returns The index of the Watch.
   */
  indexOf(watch : Watch): number {
    return this.items.indexOf(watch);
  }
  
}
