import { TestBed } from '@angular/core/testing';

import { PdfConvertorService } from './pdf-convertor.service';

describe('PdfConvertorService', () => {
  let service: PdfConvertorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PdfConvertorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
