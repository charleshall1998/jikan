import { Component, OnDestroy, OnInit } from '@angular/core';
import { Order } from 'src/app/models/Order';
import { SubscriptionsContainer } from 'src/app/models/Subscriptions-Container';
import { JikanService } from 'src/app/service/jikan.service';

@Component({
  selector: 'app-orders-list',
  templateUrl: './orders-list.component.html',
  styleUrls: ['./orders-list.component.css']
})
export class OrdersListComponent implements OnInit, OnDestroy {

  orders : Order[] = [];
  containsOrders : boolean = false;
  subs : SubscriptionsContainer = new SubscriptionsContainer();

  constructor(private jikanService : JikanService) { }

  ngOnInit(): void {
    this.subs.add = this.jikanService.getAllOrders().subscribe(list => {
      this.orders = list;
      this.containsOrders = this.orders.length > 0;
    });
  }

  ngOnDestroy(): void {
    this.subs.dispose();
  }

}
