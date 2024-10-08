﻿namespace SaborFit.DTOs
{
    public class EnderecoDTO
    {
        public int ID { get; set; }
        public string? Titulo { get; set; }
        public string? Endereco { get; set; }
        public string? Numero { get; set; }
        public string? Bairro { get; set; }
        public string? Cidade { get; set; }
        public string? UF { get; set; }
        public string? CEP { get; set; }
        public string? Complemento { get; set; }
        public ClienteDTO? cliente { get; set; }
    }
}
