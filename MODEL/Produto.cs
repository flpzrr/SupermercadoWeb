namespace $safeprojectname$.Models
{
    public class Produto
    {
        public int Id { get; set; }           // id do produto
        public string Nome { get; set; }      // nome do produto
        public decimal Preco { get; set; }    // preço do produto
        public int Quantidade { get; set; }   // quantidade em estoque
    }
}
