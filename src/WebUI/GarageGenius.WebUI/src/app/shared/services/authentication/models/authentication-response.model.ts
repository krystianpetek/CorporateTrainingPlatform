export interface AuthenticationResponseModel {
  userId: string;
  accessToken: string;
  expiry: Date;
  claims: {
    "permissions": Array<string>,
  }
}
