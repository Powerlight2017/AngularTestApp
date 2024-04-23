import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { environment } from '../../environments/environment';
import { RegistrationResult } from 'app/models/registrationResult';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {
  private apiUrl = `${environment.apiUrl}/registration`;

  constructor(private http: HttpClient) {}

  registerUser(user: User): Observable<RegistrationResult> {
    return this.http.post<RegistrationResult>(`${this.apiUrl}/register`, user);
  }
}
