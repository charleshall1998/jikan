import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TypeWatchPageComponent } from './type-watch-page.component';

describe('TypeWatchPageComponent', () => {
  let component: TypeWatchPageComponent;
  let fixture: ComponentFixture<TypeWatchPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TypeWatchPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TypeWatchPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
