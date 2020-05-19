import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { KudosByTeamComponent } from './kudos-by-team.component';

describe('KudosByTeamComponent', () => {
  let component: KudosByTeamComponent;
  let fixture: ComponentFixture<KudosByTeamComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ KudosByTeamComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(KudosByTeamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
