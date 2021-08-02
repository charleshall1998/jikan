import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WatchCartDisplayComponent } from './watch-cart-display.component';

describe('WatchCartDisplayComponent', () => {
  let component: WatchCartDisplayComponent;
  let fixture: ComponentFixture<WatchCartDisplayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WatchCartDisplayComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WatchCartDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
