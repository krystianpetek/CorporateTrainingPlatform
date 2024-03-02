# Endpoint and credentials for sign-in
$signInUrl = "https://localhost:7283/users-module/users/sign-in"
$signInPayload = @{
    email    = "service-account@garagegenius.com"
    password = "garageGenius"
} | ConvertTo-Json

# Use Invoke-RestMethod to sign in and extract the bearer token
# Note: Adding -SkipCertificateCheck for ignoring SSL errors on localhost. Remove in production.
$response = Invoke-RestMethod -Uri $signInUrl -Method Post -Body $signInPayload -ContentType "application/json"
$token = $response.accessToken

if (-not $token) {
    Write-Host "Failed to get token"
    exit 1
}

# Function to sign up users with different roles
Function SignUpUser($url, $email, $role) {
    $signUpPayload = @{
        email    = $email
        password = "garageGenius"
        role     = $role
    } | ConvertTo-Json

    # Use Invoke-RestMethod to sign up using the obtained bearer token
    try {
        $response = Invoke-RestMethod -Uri $url -Method Post -Body $signUpPayload -ContentType "application/json" -Headers @{Authorization = "Bearer $token" }
        Write-Output $response.customerId
    }
    catch {
        Write-Error $_.Exception.Message
    }
}

# Endpoint for sign-up
$signUpUrl = "https://localhost:7283/users-module/users/sign-up"

# Sign up users with different roles
SignUpUser $signUpUrl "administrator@garagegenius.com" "administrator"
SignUpUser $signUpUrl "manager@garagegenius.com" "manager"
SignUpUser $signUpUrl "employee@garagegenius.com" "employee"
SignUpUser $signUpUrl "customer@garagegenius.com" "customer"
