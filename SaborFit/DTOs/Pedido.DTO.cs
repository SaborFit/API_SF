namespace SaborFit.DTOs
{
    public class PedidoDTO
    {
        public int ID { get; set; }
        public ClienteDTO cliente { get; set; }
        public string NomeUsuario { get; set; }
        public string CPF { get; set; }
        public double ValorTotal { get; set; }
        public EnderecoDTO endereco { get; set; }
        public int IdStatus { get; set; }
        public RestauranteDTO restaurante { get; set; }
        public string CNPJ { get; set; }
        public List<ProdutoDTO> Produtos { get; set; }
    }
}
