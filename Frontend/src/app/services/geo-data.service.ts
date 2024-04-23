import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Country } from '../models/country';
import { Province } from '../models/province';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GeoDataService {
  private apiUrl = `${environment.apiUrl}/geodata`;

  constructor(private http: HttpClient) {}

  getCountries(): Observable<Country[]> {
    return this.http.get<Country[]>(`${this.apiUrl}/countries`);
  }

  getProvincesByCountryId(countryId: string): Observable<Province[]> {
    return this.http.get<Province[]>(`${this.apiUrl}/provinces/${countryId}`);
  }
}
