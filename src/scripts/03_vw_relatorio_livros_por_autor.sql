SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER VIEW dbo.vw_relatorio_livros_por_autor
AS
SELECT
    a.CodAu                               AS CodAu,
    a.Nome                                AS AutorNome,

    l.Codl                                AS Codl,
    l.Titulo                              AS Titulo,
    l.Editora                             AS Editora,
    l.Edicao                              AS Edicao,
    l.AnoPublicacao                       AS AnoPublicacao,

    -- Assuntos do livro agregados em uma única coluna
    COALESCE(STRING_AGG(s.Descricao, ', '), '') AS Assuntos
FROM dbo.Autor a
INNER JOIN dbo.Livro_Autor la
    ON la.Autor_CodAu = a.CodAu
INNER JOIN dbo.Livro l
    ON l.Codl = la.Livro_Codl
LEFT JOIN dbo.Livro_Assunto ls
    ON ls.Livro_Codl = l.Codl
LEFT JOIN dbo.Assunto s
    ON s.CodAs = ls.Assunto_CodAs
GROUP BY
    a.CodAu, a.Nome,
    l.Codl, l.Titulo, l.Editora, l.Edicao, l.AnoPublicacao;