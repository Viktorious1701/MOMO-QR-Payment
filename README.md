# Momo API QR Code Integration Example

<div style="background-color: #f0f0f0; padding: 20px; border-radius: 10px; margin-bottom: 20px;">
  <h2 style="color: #4a4a4a; text-align: center;">🚀 Quick Start Guide</h2>
  <p style="color: #666; text-align: center;">Integrate Momo QR payments in your application with ease!</p>
</div>

## Table of Contents

- [Introduction](#introduction)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Configuration](#configuration)
- [Example](#example)
- [Troubleshooting](#troubleshooting)
- [Contributing](#contributing)
- [License](#license)

## Introduction

This project demonstrates how to integrate Momo's QR code payment system into your application. With this integration, you can easily generate QR codes for payments, process transactions, and handle callbacks from the Momo API.

<div style="background-color: #e6f7ff; border-left: 5px solid #1890ff; padding: 15px; margin: 20px 0;">
  <h3 style="color: #1890ff; margin-top: 0;">💡 Why Use Momo QR Payments?</h3>
  <ul style="color: #333;">
    <li>Fast and secure transactions</li>
    <li>Seamless integration with mobile apps</li>
    <li>Supports both online and offline payments</li>
    <li>Widely used in Vietnam</li>
  </ul>
</div>

## Prerequisites

Before you begin, ensure you have the following:

- A Momo merchant account
- API credentials (Partner Code, Access Key, Secret Key)
- .NET Core 3.1 or later
- Basic understanding of RESTful APIs

## Installation

1. Clone this repository:
   ```
   git clone https://github.com/yourusername/momo-api-qr-example.git
   ```
2. Navigate to the project directory:
   ```
   cd momo-api-qr-example
   ```
3. Install dependencies:
   ```
   dotnet restore
   ```

## Usage

To use this example in your project:

1. Set up your Momo API credentials in the `appsettings.json` file.
2. Use the `MomoService` class to generate QR codes and process payments.
3. Implement the callback endpoint to handle payment notifications.

## Configuration

Update your `appsettings.json` file with your Momo API credentials:

```json
{
  "MomoAPI": {
    "PartnerCode": "YOUR_PARTNER_CODE",
    "AccessKey": "YOUR_ACCESS_KEY",
    "SecretKey": "YOUR_SECRET_KEY",
    "ApiEndpoint": "https://test-payment.momo.vn/gw_payment/transactionProcessor"
  }
}
```

## Example

Here's a simple example of how to generate a QR code for a payment:

```csharp
public async Task<string> GenerateQRCode(decimal amount, string orderId)
{
    var request = new PaymentRequest
    {
        PartnerCode = _config.PartnerCode,
        AccessKey = _config.AccessKey,
        RequestId = Guid.NewGuid().ToString(),
        Amount = amount,
        OrderId = orderId,
        OrderInfo = "Payment for Order " + orderId,
        ReturnUrl = "https://your-return-url.com",
        NotifyUrl = "https://your-notify-url.com",
        RequestType = "captureMoMoWallet"
    };

    var response = await _momoService.CreatePayment(request);
    return response.QrCodeUrl;
}
```

<div style="background-color: #f6ffed; border: 1px solid #b7eb8f; padding: 15px; border-radius: 5px; margin: 20px 0;">
  <h3 style="color: #52c41a; margin-top: 0;">✅ Best Practices</h3>
  <ul style="color: #333;">
    <li>Always validate and sanitize input data</li>
    <li>Use HTTPS for all API communications</li>
    <li>Implement proper error handling and logging</li>
    <li>Regularly update your integration with the latest Momo API version</li>
  </ul>
</div>

## Troubleshooting

If you encounter any issues:

1. Double-check your API credentials
2. Ensure your callback URLs are accessible and properly configured
3. Check the Momo API documentation for any recent changes
4. Contact Momo support if the issue persists

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

<div style="text-align: center; margin-top: 40px; padding: 20px; background-color: #f9f9f9; border-radius: 10px;">
  <img src="https://via.placeholder.com/150" alt="Momo Logo" style="width: 150px; height: auto;">
  <p style="color: #888; font-style: italic;">Powered by Momo Payment Platform</p>
</div>
