namespace $safeprojectname$.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }                 // id do item
        public int IdPedido { get; set; }           // id do pedido
        public int IdProduto { get; set; }          // id do produto
        public int Quantidade { get; set; }         // quantidade pedida
        public decimal ValorUnitario { get; set; }  // preço unitário do produto
        public decimal ValorTotal { get; set; }     // total do item (ValorUnitario * Quantidade)
    }
}
