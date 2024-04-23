export class User {
  constructor(
    public email: string,
    public password: string,
    public countryId: string,
    public provinceId: string
  ) {}
}
