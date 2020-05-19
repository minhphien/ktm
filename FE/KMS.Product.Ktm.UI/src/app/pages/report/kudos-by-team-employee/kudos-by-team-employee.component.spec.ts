import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KudosByTeamEmployeeComponent } from './kudos-by-team-employee.component';

describe('KudosByTeamEmployeeComponent', () => {
  let component: KudosByTeamEmployeeComponent;
  let fixture: ComponentFixture<KudosByTeamEmployeeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KudosByTeamEmployeeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KudosByTeamEmployeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
