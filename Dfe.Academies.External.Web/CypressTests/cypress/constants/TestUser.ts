export class TestUser {
  constructor(
    public id: string,
    public email: string,
    public adId: string,
    public role: string,
    public name?: string
  ) {}
}
