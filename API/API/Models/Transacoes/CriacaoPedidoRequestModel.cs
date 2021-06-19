namespace API.Models.Transacoes
{
    public class CriacaoPedidoRequestModel
    {
        public string title { get; set; }
        public string description { get; set; }
        public int amount { get; set; }
        public string type { get; set; }
        public CriacaoPedidoRequestAttributes attributes { get; set; }
    }

    public class CriacaoPedidoRequestAttributes
    {
        public string transactionChangedCallbackUrl { get; set; }
        public CriacaoPedidoRequestItem[] items { get; set; }
        public CriacaoPedidoRequestCustompayload customPayload { get; set; }
        public CriacaoPedidoRequestAddress[] address { get; set; }
        public bool paymentOnce { get; set; }
        public string riskHubProvider { get; set; }
        public string origin { get; set; }
    }

    public class CriacaoPedidoRequestCustompayload
    {
        public string isFrom { get; set; }
    }

    public class CriacaoPedidoRequestItem
    {
        public object ean { get; set; }
        public string sku { get; set; }
        public string description { get; set; }
        public int amount { get; set; }
        public int quantity { get; set; }
    }

    public class CriacaoPedidoRequestAddress
    {
        public string country { get; set; }
        public string street { get; set; }
        public string number { get; set; }
        public object complement { get; set; }
        public string neighborhood { get; set; }
        public string postalCode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string type { get; set; }
        public int amountValue { get; set; }
    }

}
