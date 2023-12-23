namespace Api_Ban_Ve_Xe.Models.Momo
{
    public class MomoCallbackModel
    {
        public string PartnerCode { get; set; }
        public string AccessKey { get; set; }
        public string RequestId { get; set; }
        public decimal Amount { get; set; }
        public string OrderId { get; set; }
        public string OrderInfo { get; set; }
        public string OrderType { get; set; }
        public string TransId { get; set; }
        public string Message { get; set; }
        public string LocalMessage { get; set; }
        public DateTime ResponseTime { get; set; }
        public int ErrorCode { get; set; }
        public string PayType { get; set; }
        public string ExtraData { get; set; }
        public string Signature { get; set; }
    }
}
