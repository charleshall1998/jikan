import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { JikanService } from 'src/app/service/jikan.service';
import { Watch } from '../../models/Watch';
import { ActivatedRoute } from '@angular/router';
import { SubscriptionsContainer } from 'src/app/models/Subscriptions-Container';

@Component({
  selector: 'app-type-watch-list',
  templateUrl: './type-watch-list.component.html',
  styleUrls: ['./type-watch-list.component.css']
})
export class TypeWatchListComponent implements OnInit, OnDestroy {

  watches : Watch[] = [];
  @Input() type : string = "";
  subs : SubscriptionsContainer = new SubscriptionsContainer();

  constructor(private jikanService : JikanService, private route : ActivatedRoute) { }

  ngOnInit(): void {  
    this.subs.add = this.jikanService.getWatchesByType(this.type).subscribe(list => {
      this.watches = list;
    });
  }

  ngOnDestroy(): void {
    this.subs.dispose();
  }

}
