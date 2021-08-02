import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SubscriptionsContainer } from 'src/app/models/Subscriptions-Container';
import { AuthService } from 'src/app/service/auth.service';
import { CartService } from 'src/app/service/cart.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit, OnDestroy {

  totalItem = 0;
  signedIn : boolean = false;
  subs : SubscriptionsContainer = new SubscriptionsContainer();

  constructor(private cartService : CartService, private authService : AuthService, private router : Router) { }

  ngOnInit(): void {
    this.subs.add = this.cartService.getCount().subscribe(
      count => {
        this.totalItem = count;
      }
    );

    this.signedIn = this.authService.isSignedIn();

    this.subs.add = this.authService.loggedInEvent.subscribe((signedIn) => this.signedIn = signedIn);
  }

  /**
   * Signs out the user who is currently signed in.
   */
  signOut(): void {
    this.authService.signOut();
    this.cartService.clearCart();
  }

  ngOnDestroy(): void {
    this.subs.dispose();
  }
}
