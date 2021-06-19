namespace API.Models.Transacoes
{
    public class CriacaoPedidoCallbackModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int amount { get; set; }
        public string type { get; set; }
        public CriacaoPedidoCallbackAttributes attributes { get; set; }
        public string qrCodeLink { get; set; }
        public string deepLink { get; set; }
    }

    public class CriacaoPedidoCallbackAttributes
    {
        public int cashbackAmountValue { get; set; }
        public string transactionChangedCallbackUrl { get; set; }
        public CriacaoPedidoCallbackItem[] items { get; set; }
        public CriacaoPedidoCallbackCustompayload customPayload { get; set; }
        public object address { get; set; }
        public bool paymentOnce { get; set; }
        public string riskHubProvider { get; set; }
        public string origin { get; set; }
    }

    public class CriacaoPedidoCallbackCustompayload
    {
        public string isFrom { get; set; }
    }

    public class CriacaoPedidoCallbackItem
    {
        public string description { get; set; }
        public int amount { get; set; }
        public int quantity { get; set; }
        public string sku { get; set; }
    }

}
