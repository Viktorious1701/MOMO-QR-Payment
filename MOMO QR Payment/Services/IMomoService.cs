using MOMO_QR_Payment.Models;

namespace MOMO_QR_Payment.Services
{
    public interface IMomoService
    {
        Task<MomoResponseModel> CreatePaymentAsync(OrderInfoModel model);
    }
}
