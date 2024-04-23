import { Province } from './province';

export class Country {
  constructor(
    public id: string,
    public name: string,
    public provinces: Province[]
  ) {}
}
