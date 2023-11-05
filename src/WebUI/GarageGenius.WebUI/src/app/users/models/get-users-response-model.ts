export interface GetUsersResponseModel {
  users: Array<GetUserResponseModel>;
}

export interface GetUserResponseModel {
  id: string,
  customerId: string,
  role: string,
  email: string,
  state: string,
  created: Date
}
