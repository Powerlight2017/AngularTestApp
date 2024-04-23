import { HttpClientModule } from '@angular/common/http';
import { Component, ViewChild } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatSelectModule } from '@angular/material/select';
import { MatStepper, MatStepperModule } from '@angular/material/stepper';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { GeoDataService } from 'app/services/geo-data.service';
import { Country } from 'app/models/country';
import { Province } from 'app/models/province';
import { RegistrationService } from 'app/services/registration.service';
import { User } from 'app/models/user';
import { RegistrationError } from 'app/models/registrationError';
import { passwordStrengthValidator } from 'app/validators/passwordStrengthValidator';
import { RegistrationResult } from 'app/models/registrationResult';

@Component({
  selector: 'user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatInputModule,
    MatListModule,
    MatFormFieldModule,
    MatButtonModule,
    MatSelectModule,
    MatStepperModule,
    MatCheckboxModule,
    HttpClientModule
  ]
})
export class UserRegistrationComponent {
  @ViewChild('stepper') private myStepper: MatStepper | undefined;

  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  firstStepPressed: boolean = false;
  loadingCountries: boolean = false;
  registrationRequestPending: boolean = false;
  errorLoadingCountries: boolean = false;
  countries: Country[] = [];
  provinces: Province[] = [];
  registrationResult: RegistrationResult | undefined;

  constructor(
    private _formBuilder: FormBuilder,
    private userRegistrationService: RegistrationService,
    private geoDataService: GeoDataService
  ) {
    this.firstFormGroup = this._formBuilder.group(
      {
        login: ['', [Validators.required, Validators.email]],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(6),
            passwordStrengthValidator()
          ]
        ],
        confirmPassword: ['', Validators.required],
        agreeToWorkForFood: [false, Validators.requiredTrue]
      },
      { validator: this.passwordMatchValidator }
    );

    this.secondFormGroup = this._formBuilder.group({
      country: ['', Validators.required],
      province: ['', Validators.required]
    });
  }

  passwordMatchValidator(g: FormGroup) {
    const pass = g.controls['password'].value;
    const confirmPass = g.controls['confirmPassword'].value;

    const result = pass === confirmPass ? null : { passwordMismatch: true };
    return result;
  }

  onFirstStepPressed() {
    this.firstStepPressed = true;

    if (this.myStepper!.selectedIndex == 1) {
      // load countries
      this.loadCountries();
    }
  }

  /* Called on each input in either password field */
  onPasswordInput() {
    if (this.firstFormGroup.hasError('passwordMismatch'))
      this.firstFormGroup.controls['confirmPassword'].setErrors([
        { passwordMismatch: true }
      ]);
    else this.firstFormGroup.controls['confirmPassword'].setErrors(null);
  }

  loadCountries() {
    this.loadingCountries = true;
    this.geoDataService.getCountries().subscribe({
      next: (data) => {
        this.countries = data;
        this.errorLoadingCountries = false;
        this.loadingCountries = false;
      },
      error: (err) => {
        this.errorLoadingCountries = true;
        this.loadingCountries = false;
        console.error('Failed to load countries:', err);
      }
    });
  }

  loadProvinces(countryId: string) {
    this.geoDataService.getProvincesByCountryId(countryId).subscribe({
      next: (data) => {
        this.provinces = data;
        this.errorLoadingCountries = false;
        this.loadingCountries = false;
      },
      error: (err) => {
        this.errorLoadingCountries = true;
        this.loadingCountries = false;
        console.error('Failed to load provinces:', err);
      }
    });
  }

  onCountryChange(countryId: string) {
    this.loadProvinces(countryId);

    // reset province value.
    this.secondFormGroup.controls["province"].setValue('');
  }

  onSubmit() {
    if (this.secondFormGroup.valid) {
      this.registrationRequestPending = true;
      this.userRegistrationService
        .registerUser(
          new User(
            this.firstFormGroup.value.login,
            this.firstFormGroup.value.password,
            this.secondFormGroup.value.country,
            this.secondFormGroup.value.province
          )
        )
        .subscribe({
          next: (data) => {
            this.registrationRequestPending = false;
            this.registrationResult = data;
          },
          error: (err) => {
            this.registrationRequestPending = false;
            if (err.error && err.error.errors) {
              this.registrationResult = err.error;
            } else {
              this.registrationResult = new RegistrationResult(false, [
                new RegistrationError(
                  'FailedServerComm',
                  'Failed communicating with the server'
                )
              ]);
            }
          }
        });
    }
  }
}
