const PROXY_CONFIG = [
  {
    context: [
      "/health",
      "/users-module",
      "/vehicles-module",
      "/customers-module",
      "/notifications-module",
      "/reservation-module"
    ],
    target: "https://localhost:7283",
    secure: false,
    ws: false
  },
  {
    context: [
      "/notifications",
    ],
    target: "https://localhost:7283",
    secure: false,
    ws: true
  },
]

module.exports = PROXY_CONFIG;
