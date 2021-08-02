import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WatchDisplayComponent } from './watch-display.component';

describe('WatchDisplayComponent', () => {
  let component: WatchDisplayComponent;
  let fixture: ComponentFixture<WatchDisplayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WatchDisplayComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WatchDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
