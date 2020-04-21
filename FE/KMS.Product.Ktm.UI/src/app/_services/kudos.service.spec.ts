import { TestBed } from '@angular/core/testing';

import { KudosService } from './kudos.service';

describe('KudosService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: KudosService = TestBed.get(KudosService);
    expect(service).toBeTruthy();
  });
});
