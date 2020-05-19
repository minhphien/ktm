import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KudosAcrossTeamComponent } from './kudos-across-team.component';

describe('KudosAcrossTeamComponent', () => {
  let component: KudosAcrossTeamComponent;
  let fixture: ComponentFixture<KudosAcrossTeamComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KudosAcrossTeamComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KudosAcrossTeamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
