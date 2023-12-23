using Api_Ban_Ve_Xe.Models.Momo;
using Api_Ban_Ve_Xe.Models.Order;

namespace Api_Ban_Ve_Xe.Services;

public interface IMomoService
{
    Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfoModel model);
    MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
}