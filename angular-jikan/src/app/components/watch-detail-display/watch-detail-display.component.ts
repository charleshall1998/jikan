import { Component, Input, OnInit } from '@angular/core';
import { Watch } from 'src/app/models/Watch';

@Component({
  selector: 'app-watch-detail-display',
  templateUrl: './watch-detail-display.component.html',
  styleUrls: ['./watch-detail-display.component.css']
})
export class WatchDetailDisplayComponent implements OnInit {

  @Input() watch : Watch;

  constructor() { }

  ngOnInit(): void {
  }

}
