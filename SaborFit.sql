DROP DATABASE if EXISTS SaborFit;

CREATE DATABASE SaborFit;

USE SaborFit;

CREATE TABLE Clientes (
	id INT PRIMARY KEY AUTO_INCREMENT,
	nome VARCHAR (50),
	sobrenome VARCHAR (100),
	email VARCHAR (100),
	telefone VARCHAR (15),
	DataNascimento DATE,
	cpf VARCHAR (11) NOT NULL,
	senha VARCHAR (256) NOT NULL,
	imagem VARCHAR(256)
);

CREATE TABLE Enderecos (
	id INT PRIMARY KEY AUTO_INCREMENT,
	titulo VARCHAR (30),
	endereco VARCHAR (50),
	numero VARCHAR (5),
	bairro VARCHAR (50),
	cidade VARCHAR (50),
	uf VARCHAR (2),
	cep VARCHAR (8),
	complemento VARCHAR (50),
	idUser INT NOT NULL,
	FOREIGN KEY (idUser) REFERENCES Clientes (id)
);


CREATE TABLE Restaurantes (
	id INT PRIMARY KEY AUTO_INCREMENT,
	nome VARCHAR (150),
	cnpj VARCHAR (15) NOT NULL,
	endereco VARCHAR (50),
	numero VARCHAR (5),
	bairro VARCHAR (50),
	cidade VARCHAR (50),
	uf VARCHAR (2),
	cep VARCHAR (8),
	complemento VARCHAR (50),
	imagem VARCHAR (256),
	email VARCHAR (100),
	telefone VARCHAR (15),
	especialidade VARCHAR (50),
	razaoSocial VARCHAR (150),
	banco VARCHAR (100),
	agencia VARCHAR (20),
	conta VARCHAR (20)
);

CREATE TABLE Categorias (
	id INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR (20)
);

CREATE TABLE Produtos (
	id INT PRIMARY KEY AUTO_INCREMENT,
	nome VARCHAR (150),
	tipo VARCHAR (20),
	descricao VARCHAR (200),
    ingredientes varchar (256),
    categoria INT,
	preco DOUBLE,
	peso DOUBLE,
	quantidade INT,
	imagem VARCHAR (256),
	desconto DOUBLE,
	cnpj VARCHAR (15),
	idRestaurante INT,
    FOREIGN KEY (categoria) REFERENCES Categorias (id),
	FOREIGN KEY (idRestaurante) REFERENCES Restaurantes (id)
);

CREATE TABLE Marcadores (
	id INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR (15)
);

CREATE TABLE MarcadorProduto (
	idProduto INT,
    idMarcador INT,
    FOREIGN KEY (idProduto) REFERENCES Produtos (id),
    FOREIGN KEY (idMarcador) REFERENCES Marcadores (id)
);

CREATE TABLE Favoritos (
	id INT PRIMARY KEY AUTO_INCREMENT,
	idUser INT,
	idProduto INT,
	FOREIGN KEY (idUser) REFERENCES Clientes (id),
	FOREIGN KEY (idProduto) REFERENCES Produtos (id)
);
CREATE TABLE StatusPedido (
	id INT PRIMARY KEY AUTO_INCREMENT,
    nome VARCHAR (64)
);
CREATE TABLE Pedidos (
	id INT PRIMARY KEY AUTO_INCREMENT,
	idUser INT,
	nomeUsuario VARCHAR (50),
	cpf VARCHAR (11),
	valorTotal DOUBLE,
	idEndereco INT,
    idStatus INT,
	idRestaurante INT,
	cnpj VARCHAR (15),
	FOREIGN KEY (idStatus) REFERENCES StatusPedido (id),
	FOREIGN KEY (idUser) REFERENCES Clientes (id),
    FOREIGN KEY (idEndereco) REFERENCES Enderecos (id),
	FOREIGN KEY (idRestaurante) REFERENCES Restaurantes (id)
);

CREATE TABLE PedidoProduto (
	idPedido INT,
	idProduto INT,
	quantidade INT,
	FOREIGN KEY (idPedido) REFERENCES Pedidos (id),
	FOREIGN KEY (idProduto) REFERENCES Produtos (id)
);

-- SELECT * FROM Marcadores; ----
-- select * from Produtos inner join Categorias where Produtos.categoria = Categorias.id; --

-- Inserindo dados na tabela Clientes
INSERT INTO Clientes (nome, sobrenome, email, telefone, DataNascimento, cpf, senha, imagem) 
VALUES ('João', 'Silva', 'joao@example.com', '(11) 91234-5678', '1990-05-15', '12345678901', 'senha123', 'joao.jpg');

-- Inserindo dados na tabela Enderecos
INSERT INTO Enderecos (titulo, endereco, numero, bairro, cidade, uf, cep, complemento, idUser) 
VALUES ('Casa', 'Rua das Flores', '123', 'Centro', 'São Paulo', 'SP', '12345678', 'Apto 101', 1);

-- Inserindo dados na tabela Restaurantes
INSERT INTO Restaurantes (nome, cnpj, endereco, numero, bairro, cidade, uf, cep, complemento, imagem, email, telefone, especialidade, razaoSocial, banco, agencia, conta)
VALUES ('Restaurante Teste', '12345678901234', 'Av. das Árvores', '456', 'Centro', 'São Paulo', 'SP', '12345678', 'Loja 2', 'restaurante.jpg', 'restaurante@example.com', '(11) 98765-4321', 'Gastronomia', 'Empresa Teste', 'Banco X', '1234', '56789');

-- Inserindo dados na tabela Categorias
INSERT INTO Categorias (nome) VALUES ('Bebidas'), ('Pratos Principais'), ('Sobremesas');

-- Inserindo dados na tabela Produtos
INSERT INTO Produtos (nome, tipo, descricao, ingredientes, categoria, preco, peso, quantidade, imagem, desconto, cnpj, idRestaurante)
VALUES ('Refrigerante', 'Bebida', 'Refrigerante de cola', 'Água gaseificada, açúcar e aromatizantes', 1, 5.99, 500, 100, 'refrigerante.jpg', 0, '12345678901234', 1),
       ('Pizza Margherita', 'Prato Principal', 'Pizza com molho de tomate, muçarela e manjericão', 'Farinha de trigo, molho de tomate, muçarela, manjericão', 2, 29.99, 800, 50, 'pizza.jpg', 0, '12345678901234', 1),
       ('Mousse de Chocolate', 'Sobremesa', 'Mousse de chocolate meio amargo', 'Chocolate meio amargo, creme de leite, açúcar', 3, 12.99, 300, 20, 'mousse.jpg', 0, '12345678901234', 1);

-- Inserindo dados na tabela Marcadores
INSERT INTO Marcadores (nome) VALUES ('Sem lactose'), ('Sem glúten'), ('Orgânico'), ('Vegetariano'), ('Vegano');

-- Inserindo dados na tabela MarcadorProduto (associando produtos aos marcadores)
INSERT INTO MarcadorProduto (idProduto, idMarcador) VALUES (1, 1), (3, 2);

-- Inserindo dados na tabela StatusPedido
INSERT INTO StatusPedido (nome) VALUES ('Aguardando Pagamento'), ('Em preparo'), ('Aguardando entrega'), ('Entregue');

select*from pedidos;