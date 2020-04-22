import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateKudosComponent } from './create-kudos.component';

describe('CreateKudosComponent', () => {
  let component: CreateKudosComponent;
  let fixture: ComponentFixture<CreateKudosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateKudosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateKudosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
