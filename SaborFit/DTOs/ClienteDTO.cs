namespace SaborFit.DTOs
{
    public class ClienteDTO
    {
        public int? ID { get; set; }
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string Email { get; set; }
        public string? Telefone { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? CPF { get; set; }
        public string Senha { get; set; }
        public string? Imagem { get; set; }
         public string? Base64 { get; set; }
    }
}
