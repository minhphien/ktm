import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { TableKudosSentComponent } from './table-kudos-sent.component';


describe('TableKudosSentComponent', () => {
  let component: TableKudosSentComponent;
  let fixture: ComponentFixture<TableKudosSentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TableKudosSentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TableKudosSentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
