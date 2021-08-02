import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WatchDetailDisplayComponent } from './watch-detail-display.component';

describe('WatchDetailDisplayComponent', () => {
  let component: WatchDetailDisplayComponent;
  let fixture: ComponentFixture<WatchDetailDisplayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WatchDetailDisplayComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WatchDetailDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
