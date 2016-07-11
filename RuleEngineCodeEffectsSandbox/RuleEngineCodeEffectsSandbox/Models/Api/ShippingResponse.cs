namespace RuleEngineCodeEffectsSandbox.Models.Api
{
    public class ShippingResponse<T>
    {
        public bool IsValid { get; set; }
        public T ResponseModel { get; set; }
    }
}