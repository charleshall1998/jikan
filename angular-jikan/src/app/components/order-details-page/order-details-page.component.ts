import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Order } from 'src/app/models/Order';
import { SubscriptionsContainer } from 'src/app/models/Subscriptions-Container';
import { Watch } from 'src/app/models/Watch';
import { JikanService } from 'src/app/service/jikan.service';

@Component({
  selector: 'app-order-details-page',
  templateUrl: './order-details-page.component.html',
  styleUrls: ['./order-details-page.component.css']
})
export class OrderDetailsPageComponent implements OnInit, OnDestroy {

  order : Order;
  id : number = 0;
  items : Watch[] = [];
  quantities : number[] = [];
  subs : SubscriptionsContainer = new SubscriptionsContainer();

  constructor(private jikanService : JikanService, private route : ActivatedRoute) { }

  ngOnInit(): void {
    this.subs.add = this.route.params.subscribe(paramType => {
      this.id = paramType.id;
    });
    
    this.subs.add = this.jikanService.getOrderById(this.id).subscribe(order => {
      this.order = order;    
    });

    this.subs.add = this.jikanService.getWatchesByOrderId(this.id).subscribe(watches => {
      this.items = watches;
    });

    this.subs.add = this.jikanService.getWatchQuantityByOrderId(this.id).subscribe(quantities => {
      this.quantities = quantities;
    });

  }

  ngOnDestroy(): void {
    this.subs.dispose();
  }

}
