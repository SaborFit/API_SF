using MySql.Data.MySqlClient;
using SaborFit.DTOs;

namespace SaborFit.DAOs
{
    public class RestaurantesDAO
    {
        public List<RestauranteDTO> ListarRestaurantes()
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT*FROM Restaurantes";

            var comando = new MySqlCommand(query, conexao);
            var dataReader = comando.ExecuteReader();

            var restaurantes = new List<RestauranteDTO>();

            while (dataReader.Read())
            {
                var restaurante = new RestauranteDTO();
                restaurante.ID = int.Parse(dataReader["ID"].ToString());
                restaurante.Nome = dataReader["Nome"].ToString();
                restaurante.Cnpj = dataReader["CNPJ"].ToString();
                restaurante.Endereco = dataReader["Endereco"].ToString();
                restaurante.Numero = dataReader["Numero"].ToString();
                restaurante.Bairro = dataReader["Bairro"].ToString();
                restaurante.Cidade = dataReader["Cidade"].ToString();
                restaurante.Uf = dataReader["Uf"].ToString();
                restaurante.Cep = dataReader["Cep"].ToString();
                restaurante.Complemento = dataReader["Complemento"].ToString();
                restaurante.Imagem = dataReader["Imagem"].ToString();
                restaurante.Email = dataReader["Email"].ToString();
                restaurante.Telefone = dataReader["Telefone"].ToString();
                restaurante.Especialidade = dataReader["Especialidade"].ToString();
                restaurante.RazaoSocial = dataReader["RazaoSocial"].ToString();
                restaurante.Banco = dataReader["Banco"].ToString();
                restaurante.Agencia = dataReader["Agencia"].ToString();
                restaurante.Conta = dataReader["Conta"].ToString();

                restaurantes.Add(restaurante);
            }
            conexao.Close();

            return restaurantes;
        }
    }
}
