-- drop database SaborFit ----
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

CREATE TABLE Pedidos (
	id INT PRIMARY KEY AUTO_INCREMENT,
	idUser INT,
	nomeUsuario VARCHAR (50),
	cpf VARCHAR (11),
	valorTotal DOUBLE,
	idEndereco INT,
    status varchar (50),
	idRestaurante INT,
	cnpj VARCHAR (15),
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

INSERT INTO Marcadores (nome) VALUES ("Sem Lactose");
INSERT INTO Marcadores (nome) VALUES ("Sem Glúten");
INSERT INTO Marcadores (nome) VALUES ("Orgânico");
INSERT INTO Marcadores (nome) VALUES ("Vegetariano");
INSERT INTO Marcadores (nome) VALUES ("Vegano");

-- SELECT * FROM Marcadores; ----

Select * FROM CLIENTES;

-- SELECT * FROM Marcadores; ----
select * from Produtos inner join Categorias where Produtos.categoria = Categorias.id;

INSERT INTO Categorias (nome) VALUES ('Suco');
INSERT INTO Categorias (nome) VALUES ('Lanche Natural');


select * from  Categorias;




INSERT INTO Produtos (nome, tipo, descricao, ingredientes, categoria, preco, peso, quantidade, desconto, cnpj, idRestaurante)
VALUES ('Sanduíche Natural', 'Sanduíche', 'Um delicioso sanduíche preparado com ingredientes frescos e saudáveis.', 'Pão integral, peito de frango grelhado, alface, tomate, cenoura, maionese light', 
(SELECT id FROM Categorias WHERE nome = 'Lanche Natural'), 9.99, 200, 10, 0, '12345678901234', 1);

