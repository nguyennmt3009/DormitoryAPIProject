using Newtonsoft.Json;

namespace DormitoryUI.ViewModels
{
    public class CommonResponse<T> where T : class
    {
        [JsonProperty(PropertyName = "result-code")]
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public object Error { get; set; }
        public T Data { get; set; }
    }

    public class AccountCustomerCreateVM
    {
        [JsonProperty(PropertyName = "customer_id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "deposit_amount")]
        public decimal Amount { get; set; }
    }
}