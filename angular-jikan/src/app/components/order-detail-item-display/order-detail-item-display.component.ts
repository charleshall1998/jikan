import { Component, Input, OnInit } from '@angular/core';
import { Watch } from 'src/app/models/Watch';
import { CartService } from 'src/app/service/cart.service';

@Component({
  selector: 'app-order-detail-item-display',
  templateUrl: './order-detail-item-display.component.html',
  styleUrls: ['./order-detail-item-display.component.css']
})
export class OrderDetailItemDisplayComponent implements OnInit {

  @Input() watch : Watch;

  constructor(private cartService : CartService) { }

  ngOnInit(): void {
  }

}
