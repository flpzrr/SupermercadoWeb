-- =============================================
-- BANCO DE DADOS: SUPERMERCADO
-- Criado por: Felipe Pinheiro
-- =============================================

DROP DATABASE IF EXISTS supermercado;
CREATE DATABASE supermercado CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE supermercado;

-- ========================
-- TABELA DE PRODUTOS
-- ========================
CREATE TABLE produto (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    preco DECIMAL(10,2) NOT NULL,
    quantidade INT NOT NULL CHECK (quantidade >= 0)
);

-- ========================
-- TABELA DE FORMAS DE PAGAMENTO
-- ========================
CREATE TABLE forma_pagamento (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(50) NOT NULL
);

-- ========================
-- TABELA DE PEDIDOS
-- ========================
CREATE TABLE pedido (
    id INT AUTO_INCREMENT PRIMARY KEY,
    status VARCHAR(50) DEFAULT 'Em aberto',
    data_criacao DATETIME DEFAULT CURRENT_TIMESTAMP,
    id_forma_pagamento INT,
    valor_frete DECIMAL(10,2) DEFAULT 0.00,
    valor_desconto DECIMAL(10,2) DEFAULT 0.00,
    subtotal DECIMAL(10,2) DEFAULT 0.00,
    total DECIMAL(10,2) DEFAULT 0.00,
    observacao TEXT,
    endereco_entrega VARCHAR(255),
    data_entrega DATE,
    CONSTRAINT fk_pedido_forma
        FOREIGN KEY (id_forma_pagamento)
        REFERENCES forma_pagamento(id)
);

-- ========================
-- TABELA DE ITENS DO PEDIDO
-- ========================
CREATE TABLE item_pedido (
    id INT AUTO_INCREMENT PRIMARY KEY,
    id_pedido INT NOT NULL,
    id_produto INT NOT NULL,
    quantidade INT NOT NULL CHECK (quantidade > 0),
    valor_unitario DECIMAL(10,2) NOT NULL,
    valor_total DECIMAL(10,2)
        GENERATED ALWAYS AS (quantidade * valor_unitario) STORED,
    CONSTRAINT fk_item_pedido
        FOREIGN KEY (id_pedido)
        REFERENCES pedido(id)
        ON DELETE CASCADE,
    CONSTRAINT fk_item_produto
        FOREIGN KEY (id_produto)
        REFERENCES produto(id)
        ON DELETE CASCADE
);

-- ========================
-- FORMAS DE PAGAMENTO
-- ========================
INSERT INTO forma_pagamento (nome) VALUES
('Dinheiro'),
('Cartão de Crédito'),
('Pix'),
('Boleto');

-- ========================
-- PRODUTOS INICIAIS
-- ========================
INSERT INTO produto (nome, preco, quantidade) VALUES
('Arroz 5kg', 25.90, 30),
('Feijão 1kg', 8.50, 50),
('Macarrão 500g', 4.20, 45),
('Açúcar 1kg', 4.80, 60),
('Café 500g', 12.90, 40),
('Suco Dell Vale Laranja', 10.90, 100);

-- ========================
-- TESTES
-- ========================
SELECT * FROM produto;
SELECT * FROM forma_pagamento;
SELECT * FROM pedido;
SELECT * FROM item_pedido;
