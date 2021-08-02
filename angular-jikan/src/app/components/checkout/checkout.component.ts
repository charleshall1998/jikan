import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CartService } from 'src/app/service/cart.service';
import { JikanService } from 'src/app/service/jikan.service';
import { Order } from '../../models/Order';
import { User } from 'src/app/models/User';
import { OrderDetail } from '../../models/OrderDetail';
import { AuthService } from 'src/app/service/auth.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent implements OnInit {

  subTotal : number = this.cartService.getSubTotal();
  tax : number = this.cartService.getTax();
  total : number = this.subTotal + this.tax;
  date : Date;
  orderDetails : OrderDetail[] = [];
  userToAdd : User;

  constructor(private cartService : CartService, private jikanService : JikanService, private router : Router, private authService : AuthService) { }

  ngOnInit(): void {
    this.userToAdd  = {
      id : 0,
      username : this.authService.userCreds?.username,
      email : "",
      name : "",
      passwordHash : [],
      passwordSalt : []
    }
  }

  /**
   * Submits the user-entered form for checkout to add an order.
   *
   * @param checkoutForm - The NgForm being submitted.
   *
   */
  onSubmit(checkoutForm : NgForm): void {

    if(checkoutForm.valid) {
      const order = checkoutForm.value;

      let toAdd : Order = {
        total : this.total,
        date : new Date(),
        deliveryAddress : order.deliveryAddress,
        orderDetails : this.setOrderDetails(),
        name : order.firstName + " " + order.lastName,
        email : order.email,
        city : order.city,
        postalCode : order.postalCode,
        purchaser : this.userToAdd
      } 

      this.jikanService.addOrder(toAdd).subscribe((_) => {this.router.navigate(["/orders"])});
      this.cartService.clearCart();
    }
  }

  /**
   * Sets the OrderDetail array for a particular order by getting each watch id and its associated quantity.
   *
   * @returns An OrderDetail array.
   *
   */
  setOrderDetails(): OrderDetail[] {
    let orderDetails : OrderDetail [] = [];

    for(var i = 0; i < this.cartService.items.length; i++) {
        let orderDetail : OrderDetail = { 
          watchId : this.cartService.items[i].id, 
          quantity :  this.cartService.quantities[i]
        }
      orderDetails.push(orderDetail);
    }

    return orderDetails;
  }

}
