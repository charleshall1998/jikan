import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SubscriptionsContainer } from 'src/app/models/Subscriptions-Container';

@Component({
  selector: 'app-type-watch-page',
  templateUrl: './type-watch-page.component.html',
  styleUrls: ['./type-watch-page.component.css']
})
export class TypeWatchPageComponent implements OnInit, OnDestroy {

  type : string = "";
  subs : SubscriptionsContainer = new SubscriptionsContainer();

  constructor(private route : ActivatedRoute, private router : Router) { 
    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  ngOnInit(): void {
    this.subs.add = this.route.params.subscribe(paramType => {
      this.type = paramType.type;
    });
  }

  ngOnDestroy(): void {
    this.subs.dispose();
  }

}
