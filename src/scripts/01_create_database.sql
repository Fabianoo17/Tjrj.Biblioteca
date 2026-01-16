IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Assunto] (
    [CodAs] int NOT NULL IDENTITY,
    [Descricao] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_Assunto] PRIMARY KEY ([CodAs])
);
GO

CREATE TABLE [Autor] (
    [CodAu] int NOT NULL IDENTITY,
    [Nome] nvarchar(40) NOT NULL,
    CONSTRAINT [PK_Autor] PRIMARY KEY ([CodAu])
);
GO

CREATE TABLE [Forma_Compra] (
    [Id] int NOT NULL IDENTITY,
    [Descricao] nvarchar(30) NOT NULL,
    CONSTRAINT [PK_Forma_Compra] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Livro] (
    [Codl] int NOT NULL IDENTITY,
    [Titulo] nvarchar(40) NOT NULL,
    [Editora] nvarchar(40) NOT NULL,
    [Edicao] int NOT NULL,
    [AnoPublicacao] nvarchar(4) NOT NULL,
    CONSTRAINT [PK_Livro] PRIMARY KEY ([Codl])
);
GO

CREATE TABLE [Livro_Assunto] (
    [Livro_Codl] int NOT NULL,
    [Assunto_CodAs] int NOT NULL,
    CONSTRAINT [PK_Livro_Assunto] PRIMARY KEY ([Livro_Codl], [Assunto_CodAs]),
    CONSTRAINT [FK_Livro_Assunto_Assunto_Assunto_CodAs] FOREIGN KEY ([Assunto_CodAs]) REFERENCES [Assunto] ([CodAs]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Livro_Assunto_Livro_Livro_Codl] FOREIGN KEY ([Livro_Codl]) REFERENCES [Livro] ([Codl]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Livro_Autor] (
    [Livro_Codl] int NOT NULL,
    [Autor_CodAu] int NOT NULL,
    CONSTRAINT [PK_Livro_Autor] PRIMARY KEY ([Livro_Codl], [Autor_CodAu]),
    CONSTRAINT [FK_Livro_Autor_Autor_Autor_CodAu] FOREIGN KEY ([Autor_CodAu]) REFERENCES [Autor] ([CodAu]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Livro_Autor_Livro_Livro_Codl] FOREIGN KEY ([Livro_Codl]) REFERENCES [Livro] ([Codl]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Livro_Preco] (
    [Livro_Codl] int NOT NULL,
    [FormaCompra_Id] int NOT NULL,
    [Valor] decimal(10,2) NOT NULL,
    CONSTRAINT [PK_Livro_Preco] PRIMARY KEY ([Livro_Codl], [FormaCompra_Id]),
    CONSTRAINT [FK_Livro_Preco_Forma_Compra_FormaCompra_Id] FOREIGN KEY ([FormaCompra_Id]) REFERENCES [Forma_Compra] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Livro_Preco_Livro_Livro_Codl] FOREIGN KEY ([Livro_Codl]) REFERENCES [Livro] ([Codl]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Livro_Assunto_Assunto_CodAs] ON [Livro_Assunto] ([Assunto_CodAs]);
GO

CREATE INDEX [IX_Livro_Autor_Autor_CodAu] ON [Livro_Autor] ([Autor_CodAu]);
GO

CREATE INDEX [IX_Livro_Preco_FormaCompra_Id] ON [Livro_Preco] ([FormaCompra_Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260116013325_InitialCreate', N'8.0.23');
GO

COMMIT;
GO

