import { Component, Input, OnInit } from '@angular/core';
import { Watch } from 'src/app/models/Watch';
import { JikanService } from 'src/app/service/jikan.service';

@Component({
  selector: 'app-watch-display',
  templateUrl: './watch-display.component.html',
  styleUrls: ['./watch-display.component.css']
})
export class WatchDisplayComponent implements OnInit {

  @Input() watch : Watch;

  constructor() { }

  ngOnInit(): void {
  }

}
