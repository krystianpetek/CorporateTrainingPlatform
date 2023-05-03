const PROXY_CONFIG = [
  {
    context: [
      "/users-module/",
    ],
    target: "https://localhost:7283",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
