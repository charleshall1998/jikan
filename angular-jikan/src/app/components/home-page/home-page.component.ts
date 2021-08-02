import { Component, OnDestroy, OnInit } from '@angular/core';
import { SubscriptionsContainer } from 'src/app/models/Subscriptions-Container';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit, OnDestroy {

  signedIn : boolean = false;
  subs : SubscriptionsContainer = new SubscriptionsContainer();

  constructor(private authService : AuthService) { }

  ngOnInit(): void {
    this.subs.add = this.authService.loggedInEvent.subscribe((signedIn) => this.signedIn = signedIn);
  }

  ngOnDestroy(): void {
    this.subs.dispose();
  }
}
