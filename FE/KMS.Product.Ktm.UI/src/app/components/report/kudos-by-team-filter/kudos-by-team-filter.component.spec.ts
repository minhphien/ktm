import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KudosByTeamFilterComponent } from './kudos-by-team-filter.component';

describe('KudosByTeamFilterComponent', () => {
  let component: KudosByTeamFilterComponent;
  let fixture: ComponentFixture<KudosByTeamFilterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KudosByTeamFilterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KudosByTeamFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
