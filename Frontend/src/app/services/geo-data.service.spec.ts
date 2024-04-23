import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { GeoDataService } from './geo-data.service';
import { Country } from '../models/country';
import { Province } from '../models/province';

describe('GeoDataService', () => {
  let service: GeoDataService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [GeoDataService]
    });

    service = TestBed.inject(GeoDataService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify(); // Verify that there are no outstanding requests.
  });

  it('should retrieve countries', () => {
    const mockCountries: Country[] = [
      { id: '1', name: 'USA', provinces: [] },
      { id: '2', name: 'Canada', provinces: [] }
    ];

    service.getCountries().subscribe(countries => {
      expect(countries.length).toBe(2);
      expect(countries).toEqual(mockCountries);
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/countries`);
    expect(req.request.method).toEqual('GET');
    req.flush(mockCountries);
  });

  it('should retrieve provinces by country ID', () => {
    const mockProvinces: Province[] = [
      { id: '101', name: 'California', countryId: '1' },
      { id: '102', name: 'Quebec', countryId: '2' }
    ];
    const countryId = '1';

    service.getProvincesByCountryId(countryId).subscribe(provinces => {
      expect(provinces.length).toBe(1);
      expect(provinces[0].name).toEqual('California');
    });

    const req = httpTestingController.expectOne(`${service['apiUrl']}/provinces/${countryId}`);
    expect(req.request.method).toEqual('GET');
    req.flush(mockProvinces.filter(p => p.countryId === countryId));
  });
});
