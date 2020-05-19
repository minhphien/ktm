import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KudosAcrossTeamFilterComponent } from './kudos-across-team-filter.component';

describe('KudosAcrossTeamFilterComponent', () => {
  let component: KudosAcrossTeamFilterComponent;
  let fixture: ComponentFixture<KudosAcrossTeamFilterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KudosAcrossTeamFilterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KudosAcrossTeamFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
