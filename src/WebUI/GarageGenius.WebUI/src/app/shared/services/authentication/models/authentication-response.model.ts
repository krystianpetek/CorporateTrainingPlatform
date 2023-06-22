import { Role } from './role.model';

export interface AuthenticationResponseModel {
  userId: string;
  accessToken: string;
  expiry: Date;
  role: Role;
  claims: {
    permissions: Array<string>;
  };
}
