namespace SaborFit.DTOs
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public ClienteDTO ID { get; set; }
        public string NomeUsuario { get; set; }
        public string CPF { get; set; }
        public double ValorTotal { get; set; }
        public EnderecoDTO endereco { get; set; }
        public string Status { get; set; }
        public RestauranteDTO restaurante { get; set; }
        public string CNPJ { get; set; }
        public List<ProdutoDTO> Produtos { get; set; }
    }
}
