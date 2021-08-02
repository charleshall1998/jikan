import { Component, OnDestroy, OnInit } from '@angular/core';
import { Watch } from 'src/app/models/Watch';
import { JikanService } from 'src/app/service/jikan.service';
import { ActivatedRoute } from '@angular/router';
import { SubscriptionsContainer } from 'src/app/models/Subscriptions-Container';


@Component({
  selector: 'app-watch-detail-page',
  templateUrl: './watch-detail-page.component.html',
  styleUrls: ['./watch-detail-page.component.css']
})
export class WatchDetailPageComponent implements OnInit, OnDestroy {

  watch : Watch;
  id : number = 0;
  subs : SubscriptionsContainer = new SubscriptionsContainer();

  constructor(private jikanService : JikanService, private route : ActivatedRoute) { }

  ngOnInit(): void {
    this.subs.add = this.route.params.subscribe(paramType => {
      this.id = paramType.id;
    });
    
    this.subs.add = this.jikanService.getWatchById(this.id).subscribe(watch => {
      this.watch = watch;
    });
  }

  ngOnDestroy(): void {
    this.subs.dispose();
  }

}
