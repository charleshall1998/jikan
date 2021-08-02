import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WatchDetailPageComponent } from './watch-detail-page.component';

describe('WatchDetailPageComponent', () => {
  let component: WatchDetailPageComponent;
  let fixture: ComponentFixture<WatchDetailPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WatchDetailPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WatchDetailPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
