-- Criação da base de dados -- DROP DATABASE beneficio_contrib
/*
CREATE DATABASE beneficio_contrib;

DROP TABLE IF EXISTS vinculo_contribuinte_beneficio;
DROP TABLE IF EXISTS contribuinte;
DROP TABLE IF EXISTS regime_tributacao;
DROP TABLE IF EXISTS beneficio;
*/

-- Criação das tabelas
CREATE TABLE beneficio(
    id_codigo serial4 CONSTRAINT pk_beneficio PRIMARY KEY,
    nome VARCHAR(200) NOT NULL,
    porcentagem_desconto NUMERIC(18,2) NOT NULL
);

CREATE TABLE regime_tributacao(
    id_codigo INT NOT NULL CONSTRAINT pk_contribuinte_regime PRIMARY KEY,
    abreviacao VARCHAR(50) NOT NULL,
    descricao VARCHAR(200) NOT NULL
);

CREATE TABLE contribuinte(
    id_codigo serial4 CONSTRAINT pk_contribuinte PRIMARY KEY,
    cnpj VARCHAR(14) NOT NULL CONSTRAINT uq_contribuinte_cnpj UNIQUE,
    razao_social VARCHAR(200) NOT NULL,
    data_abertura TIMESTAMP NOT NULL,
    es_regime_tributacao INT NOT NULL,
    CONSTRAINT fk_regime_tributacao FOREIGN KEY (es_regime_tributacao) REFERENCES regime_tributacao(id_codigo)
);

CREATE TABLE vinculo_contribuinte_beneficio(
    id_codigo serial4 CONSTRAINT pk_contribuinte_desconto PRIMARY KEY,
    es_contribuinte INT NOT NULL,
    es_beneficio INT NOT NULL,
    CONSTRAINT fk_contribuinte FOREIGN KEY (es_contribuinte) REFERENCES contribuinte(id_codigo),
    CONSTRAINT fk_beneficio FOREIGN KEY (es_beneficio) REFERENCES beneficio(id_codigo),
    CONSTRAINT uq_vinculo_contribuinte_beneficio UNIQUE (es_contribuinte, es_beneficio)
);

-- Inserts padrão antes de usar
INSERT INTO regime_tributacao(id_codigo, abreviacao, descricao)
VALUES
(5,'MEI','Micro Empreendedor Individual'),
(6,'MEEPP','Micro Empresa ou Empresa de Pequeno Porte'),
(7,'Variável','Tributação por Faturamento Variável');


