import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TableKudosReceivedComponent } from './table-kudos-recevied.component';


describe('TableKudosReceivedComponent', () => {
  let component: TableKudosReceivedComponent;
  let fixture: ComponentFixture<TableKudosReceivedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TableKudosReceivedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TableKudosReceivedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
