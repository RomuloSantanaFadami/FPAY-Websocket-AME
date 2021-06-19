namespace API.Models.Transacoes
{
    public class CriacaoPedidoResponseModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int amount { get; set; }
        public string type { get; set; }
        public CriacaoPedidoResponseAttributes attributes { get; set; }
        public string qrCodeLink { get; set; }
        public string deepLink { get; set; }
    }

    public class CriacaoPedidoResponseAttributes
    {
        public int cashbackAmountValue { get; set; }
        public string transactionChangedCallbackUrl { get; set; }
        public CriacaoPedidoResponseItem[] items { get; set; }
        public CriacaoPedidoResponseCustompayload customPayload { get; set; }
        public CriacaoPedidoResponseAddress[] address { get; set; }
        public bool paymentOnce { get; set; }
        public string riskHubProvider { get; set; }
        public string origin { get; set; }
    }

    public class CriacaoPedidoResponseCustompayload
    {
        public string isFrom { get; set; }
    }

    public class CriacaoPedidoResponseItem
    {
        public string description { get; set; }
        public int amount { get; set; }
        public int quantity { get; set; }
        public string sku { get; set; }
    }

    public class CriacaoPedidoResponseAddress
    {
        public string country { get; set; }
        public string number { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string postalCode { get; set; }
        public string neighborhood { get; set; }
        public string state { get; set; }
        public string complement { get; set; }
        public string type { get; set; }
        public int amountValue { get; set; }
    }

}
