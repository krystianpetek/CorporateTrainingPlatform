export enum AuthenticationResultStatus {
  Success,
  Failed,
  Redirected
}

export type IAuthenticationResult =
  SuccessAuthenticationResult |
  FailureAuthenticationResult |
  RedirectAuthenticationResult;

export interface SuccessAuthenticationResult {
  status: AuthenticationResultStatus.Success;
  state: any;
}

export interface FailureAuthenticationResult {
  status: AuthenticationResultStatus.Failed;
  message: string;
}

export interface RedirectAuthenticationResult {
  status: AuthenticationResultStatus.Redirected;
}
