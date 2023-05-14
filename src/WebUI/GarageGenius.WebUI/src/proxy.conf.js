const PROXY_CONFIG = [
  {
    context: [
      "/health/",
    ],
    target: "https://localhost:7283",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
