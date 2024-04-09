namespace SaborFit.DTOs
{
    public class ProdutoDTO
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public string Ingredientes { get; set; }
        public int Categoria { get; set; }
        public double Preco { get; set; }
        public double Peso { get; set; }
        public int Quantidade { get; set; }
        public string Imagem { get; set; }
        public double Desconto { get; set; }
        public string Cnpj { get; set; }
        public RestauranteDTO Restaurante  { get; set; }
    }
}
