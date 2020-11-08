CREATE DATABASE Quizzle;
USE Quizzle;

CREATE TABLE PERFIL (
	ID_PERFIL INT AUTO_INCREMENT PRIMARY KEY,
    NOME VARCHAR(40),
    EMAIL VARCHAR(40),
    LOGIN VARCHAR(20),
    SENHA VARCHAR(20)
);

CREATE TABLE LISTAQUIZZES (
	ID_LISTAQUIZ INT AUTO_INCREMENT PRIMARY KEY,
    ID_PERFIL INT
);

CREATE TABLE CATEGORIAS (
	ID_CATEGORIA INT AUTO_INCREMENT PRIMARY KEY,
    DESCRICAO VARCHAR(100),
    IMAGEM VARCHAR(500)
);

INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Animais', '/images/categorias/animais.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Arte', '/images/categorias/arte.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Biografias', '/images/categorias/biografias.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Biologia', '/images/categorias/biologia.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Carros', '/images/categorias/carros.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Celebridades', '/images/categorias/celebridades.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Cinema', '/images/categorias/cinema.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Conhecimentos Gerais', '/images/categorias/conhecimentos.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Esportes', '/images/categorias/esportes.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Física', '/images/categorias/fisica.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Geografia', '/images/categorias/geografia.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('História', '/images/categorias/historia.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Idiomas', '/images/categorias/idiomas.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Jogos', '/images/categorias/jogos.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Literatura', '/images/categorias/literatura.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Matemática', '/images/categorias/matematica.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Música', '/images/categorias/musica.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Química', '/images/categorias/quimica.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Religião', '/images/categorias/religiao.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('TV', '/images/categorias/tv.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Anime', '/images/categorias/anime.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Desenhos Animados', '/images/categorias/desenhos_animados.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Novelas', '/images/categorias/novelas.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Religião', '/images/categorias/religiao.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Reality Shows', '/images/categorias/reality_shows.jpg');
INSERT INTO CATEGORIAS (DESCRICAO, IMAGEM) VALUES ('Séries', '/images/categorias/series.jpg');

CREATE TABLE QUIZZES (
	ID_QUIZ INT AUTO_INCREMENT PRIMARY KEY,
    TITULO VARCHAR(20),
    IMAGEM VARCHAR(500),
    DESCRICAO VARCHAR(100),
	DATACADASTRO DATETIME DEFAULT current_timestamp(),
    ID_LISTAQUIZ INT,
    ID_CATEGORIA INT
);

CREATE TABLE PERGUNTAS (
	ID_PERGUNTA INT AUTO_INCREMENT PRIMARY KEY,
    TITULO VARCHAR(20),
    ID_QUIZ INT
);

CREATE TABLE ALTERNATIVAS (
	ID_ALTERNATIVA INT AUTO_INCREMENT PRIMARY KEY,
    ID_PERGUNTA INT,
    ALTERNATIVA VARCHAR(20)
);

CREATE TABLE RESPOSTA (
	ID_RESPOSTA INT AUTO_INCREMENT PRIMARY KEY,
    ID_PERGUNTA INT,
    ID_ALTERNATIVA INT
);