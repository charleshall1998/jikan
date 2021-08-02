import { Component, OnDestroy, OnInit } from '@angular/core';
import { Watch } from '../../models/Watch';
import { JikanService } from 'src/app/service/jikan.service';
import { SubscriptionsContainer } from 'src/app/models/Subscriptions-Container';


@Component({
  selector: 'app-watches-list',
  templateUrl: './watches-list.component.html',
  styleUrls: ['./watches-list.component.css']
})
export class WatchesListComponent implements OnInit, OnDestroy {

  watches : Watch[] = [];
  subs : SubscriptionsContainer = new SubscriptionsContainer();

  constructor(private jikanService : JikanService) { }

  ngOnInit(): void {
    this.subs.add = this.jikanService.getAllWatches().subscribe(list => {
      this.watches = list;
    });
  }

  ngOnDestroy(): void {
    this.subs.dispose();
  }

}
