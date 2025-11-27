namespace $safeprojectname$.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        // Agora são opcionais (string?)
        public string? Status { get; set; }

        public DateTime DataCriacao { get; set; }

        public int IdFormaPagamento { get; set; }

        // Agora é opcional também
        public string? NomeFormaPagamento { get; set; }

        public decimal ValorFrete { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }

        // Agora são opcionais também
        public string? Observacao { get; set; }
        public string? EnderecoEntrega { get; set; }

        public DateTime? DataEntrega { get; set; }
    }
}
