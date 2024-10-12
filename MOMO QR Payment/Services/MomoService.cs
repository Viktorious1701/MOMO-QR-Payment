using Microsoft.Extensions.Options;
using MOMO_QR_Payment.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;


namespace MOMO_QR_Payment.Services
{
    public class MomoService : IMomoService
    {
        private readonly MomoConfig _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public MomoService(IOptions<MomoConfig> config, IHttpClientFactory httpClientFactory)
        {
            _config = config.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<MomoResponseModel> CreatePaymentAsync(OrderInfoModel model)
        {
            model.OrderId = DateTime.UtcNow.Ticks.ToString();
            model.OrderInfo = "Course Payment: " + model.OrderInfo;

            var rawData = $"partnerCode={_config.PartnerCode}&accessKey={_config.AccessKey}&requestId={model.OrderId}&amount={model.Amount}&orderId={model.OrderId}&orderInfo={model.OrderInfo}&returnUrl={_config.ReturnUrl}&notifyUrl={_config.NotifyUrl}&extraData=";

            var signature = ComputeHmacSha256(rawData, _config.SecretKey);

            var requestData = new
            {
                accessKey = _config.AccessKey,
                partnerCode = _config.PartnerCode,
                requestType = _config.RequestType,
                notifyUrl = _config.NotifyUrl,
                returnUrl = _config.ReturnUrl,
                orderId = model.OrderId,
                amount = model.Amount.ToString(),
                orderInfo = model.OrderInfo,
                requestId = model.OrderId,
                extraData = "",
                signature = signature
            };

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync(_config.MomoApiUrl, requestData);
            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<MomoResponseModel>(responseContent);
        }

        private string ComputeHmacSha256(string message, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                var hashBytes = hmac.ComputeHash(messageBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
