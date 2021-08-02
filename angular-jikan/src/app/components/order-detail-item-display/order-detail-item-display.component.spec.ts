import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderDetailItemDisplayComponent } from './order-detail-item-display.component';

describe('OrderDetailItemDisplayComponent', () => {
  let component: OrderDetailItemDisplayComponent;
  let fixture: ComponentFixture<OrderDetailItemDisplayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrderDetailItemDisplayComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderDetailItemDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
