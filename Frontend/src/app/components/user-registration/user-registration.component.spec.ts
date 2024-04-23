import { ComponentFixture, TestBed, tick } from '@angular/core/testing';
import { ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { UserRegistrationComponent } from './user-registration.component';
import { GeoDataService } from 'app/services/geo-data.service';
import { RegistrationService } from 'app/services/registration.service';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatStepperModule } from '@angular/material/stepper';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { of } from 'rxjs';
import { RegistrationResult } from 'app/models/registrationResult';

describe('UserRegistrationComponent', () => {
  let component: UserRegistrationComponent;
  let fixture: ComponentFixture<UserRegistrationComponent>;
  let geoDataServiceMock: any;
  let registrationServiceMock: any;

  beforeEach(async () => {
    geoDataServiceMock = jasmine.createSpyObj('GeoDataService', ['getCountries', 'getProvincesByCountryId']);
    registrationServiceMock = jasmine.createSpyObj('RegistrationService', ['registerUser']);

    await TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        HttpClientTestingModule,
        MatInputModule,
        MatListModule,
        MatFormFieldModule,
        MatButtonModule,
        MatSelectModule,
        MatStepperModule,
        MatCheckboxModule,
        NoopAnimationsModule,
        UserRegistrationComponent
      ],
      providers: [
        { provide: GeoDataService, useValue: geoDataServiceMock },
        { provide: RegistrationService, useValue: registrationServiceMock },
        FormBuilder
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    geoDataServiceMock.getCountries.and.returnValue(of([{ id: '1', name: 'USA' }, { id: '2', name: 'Canada' }]));
    geoDataServiceMock.getProvincesByCountryId.and.returnValue(of([{ id: '101', name: 'California' }]));
  });

  
  it('should load countries on init', () => {
    geoDataServiceMock.getCountries.and.returnValue(of([{ id: '1', name: 'USA' }]));
    component.loadCountries();
    expect(geoDataServiceMock.getCountries).toHaveBeenCalled();
    expect(component.countries.length).toBe(1);
    expect(component.countries[0].name).toEqual('USA');
  });

  it('should load provinces when a country is selected', () => {
    geoDataServiceMock.getProvincesByCountryId.and.returnValue(of([{ id: '101', name: 'California' }]));
    component.onCountryChange('1');
    expect(geoDataServiceMock.getProvincesByCountryId).toHaveBeenCalledWith('1');
    expect(component.provinces.length).toBe(1);
    expect(component.provinces[0].name).toEqual('California');
  });

  it('should submit registration form', () => {
    component.firstFormGroup.controls['login'].setValue('test@example.com');
    component.firstFormGroup.controls['password'].setValue('123456');
    component.firstFormGroup.controls['confirmPassword'].setValue('123456');
    component.firstFormGroup.controls['agreeToWorkForFood'].setValue(true);
    component.secondFormGroup.controls['country'].setValue('1');
    component.secondFormGroup.controls['province'].setValue('101');
  
    registrationServiceMock.registerUser.and.returnValue(of(new RegistrationResult(true, [])));
  
    component.onSubmit();
    expect(registrationServiceMock.registerUser).toHaveBeenCalled();
  });

  it('should show error message if login is empty', () => {
    component.firstFormGroup.controls['login'].setValue('');
    fixture.detectChanges();
    const passwordInput = fixture.debugElement.nativeElement.querySelector('input[formControlName="password"]');
    const loginInput = fixture.debugElement.nativeElement.querySelector('input[formControlName="login"]');
    loginInput.dispatchEvent(new Event('input'));
    fixture.detectChanges();

    loginInput.dispatchEvent(new Event('focus'));  // focus on login field.
    loginInput.dispatchEvent(new Event('blur'));  // lost focus.
    fixture.detectChanges();

    const errors = fixture.debugElement.nativeElement.querySelector('mat-error');
    expect(errors.textContent).toContain('Login is required');
  });
  
  it('should show error message if login is not a valid email', () => {
    component.firstFormGroup.controls['login'].setValue('invalid-email');
    fixture.detectChanges();
    const loginInput = fixture.debugElement.nativeElement.querySelector('input[formControlName="login"]');
    loginInput.dispatchEvent(new Event('input'));
    fixture.detectChanges();

    fixture.whenStable().then(() => { // Wait UI.
        fixture.detectChanges();
        const errors = fixture.debugElement.nativeElement.querySelector('mat-error');
        expect(errors.textContent).toContain('Login must be valid E-Mail');
      });
  });

  it('should show error if password is too short', () => {

    const passwordInput = fixture.debugElement.nativeElement.querySelector('input[formControlName="password"]');
    passwordInput.dispatchEvent(new Event('focus'));  // focus on password field.

    component.firstFormGroup.controls['password'].setValue('123');
    fixture.detectChanges();

    const button = fixture.debugElement.nativeElement.querySelector('button');
    button.dispatchEvent(new Event('click'));

    fixture.whenStable().then(() => { // Wait UI.
        fixture.detectChanges();
        const errors = fixture.debugElement.nativeElement.querySelectorAll('mat-error');
        expect(errors[1].textContent).toContain('Password must be at least 6 characters long');
      });
  });
  
  it('should display an error if the agree checkbox is not checked', () => {
    component.firstFormGroup.controls['agreeToWorkForFood'].setValue(false);
    component.onFirstStepPressed(); 
    fixture.detectChanges();
    const errors = fixture.debugElement.nativeElement.querySelector('mat-error');
    expect(errors.textContent).toContain('It is required');
  });
});
