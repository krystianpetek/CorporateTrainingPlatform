const PROXY_CONFIG = [
  {
    context: [
      "/health",
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
