import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TypeWatchListComponent } from './type-watch-list.component';

describe('TypeWatchListComponent', () => {
  let component: TypeWatchListComponent;
  let fixture: ComponentFixture<TypeWatchListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TypeWatchListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TypeWatchListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
