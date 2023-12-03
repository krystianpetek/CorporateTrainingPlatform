export const environment = {
  production: false,
  // SignalR
  notificationHubUrl: '/notifications',

  // users
  usersApiUrl: `users-module/`,
  getUsersUrl: `users-module/users/users`,
  postUserUrl: `users-module/users/users`,

  signUpUrl: `users-module/users/sign-up`,
  signInUrl: `users-module/users/sign-in`,
  signOutUrl: `users-module/users/sign-out`,

  // vehicles
  vehiclesApiUrl: `vehicles-module/`,

  // customers
  customersApiUrl: `customers-module/`,

  // reservations
  reservationsApiUrl: `reservations-module/`,

  baseUrl: 'https://localhost:7283/',
};
