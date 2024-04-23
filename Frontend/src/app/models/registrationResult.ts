import { RegistrationError } from './registrationError';

export class RegistrationResult {
  constructor(
    public success: boolean,
    public errors: RegistrationError[]
  ) {}
}
