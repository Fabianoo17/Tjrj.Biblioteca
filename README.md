# 📚 TJRJ.Biblioteca (BookSaver)

Sistema Web em **ASP.NET Core MVC (Razor)** com **API em camadas** para cadastro e gestão de livros, autores, assuntos e preços por forma de compra, incluindo relatório baseado em **VIEW no SQL Server**.

---

## 🏗️ Arquitetura

O projeto segue uma arquitetura em camadas:

* **Tjrj.Biblioteca.Api** – API REST (Controllers, validações e exposição de endpoints)
* **Tjrj.Biblioteca.Application** – Casos de uso, serviços, DTOs e validações (FluentValidation)
* **Tjrj.Biblioteca.Infra** – Acesso a dados (EF Core), DbContext, repositórios e mapeamentos
* **Tjrj.Biblioteca.IoC** – Registro centralizado de dependências
* **Tjrj.Biblioteca.Web** – Front-end MVC Razor consumindo a API

---

## ✅ Funcionalidades

* CRUD de **Autores**
* CRUD de **Assuntos**
* CRUD de **Livros** (com múltiplos autores, assuntos e preços por forma de compra)
* **Relatório “Livros por Autor”** baseado em VIEW no SQL Server
* Validações com **FluentValidation**
* Consumo da API via `HttpClient` tipado

---

## 📦 Pré-requisitos

* **.NET 8 SDK**
* **SQL Server** (Express/Developer ou superior)
* (Opcional) **IIS + .NET Hosting Bundle** para publicação em produção

---

## 🗄️ Implantação do Banco (SQL Server)

Na pasta **/scripts** do projeto existem os seguintes arquivos:

```
scripts/
├── 01_create_database.sql   (gerado pelo EF Core)
├── 03_vw_relatorio_livros_por_autor.sql    (VIEW do relatório)
└── 02_dados_iniciais_forma_compra.sql            (dados iniciais)
```

### Passos

1. Abrir o **SQL Server Management Studio (SSMS)**
2. Executar o scripts


* Criar o banco **TJRJ** (se não existir)
* Criar todas as tabelas e constraints
* Criar a VIEW `dbo.vw_relatorio_livros_por_autor`
* Inserir dados iniciais em `Forma_Compra`

### Validação rápida

```sql
USE TJRJ;
SELECT TOP 10 * FROM dbo.Forma_Compra;
SELECT TOP 10 * FROM dbo.vw_relatorio_livros_por_autor;
```

---

## 🔧 Configuração da API

No projeto **Tjrj.Biblioteca.Api**, em `appsettings.Production.json`, ajuste a connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TJRJ;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

(Se usar usuário/senha, ajuste conforme necessário.)

---

## 🚀 Publicação da API

### Gerar publish

```bash
dotnet publish .\Tjrj.Biblioteca.Api\ -c Release -o .\publish\api
```

### Executar localmente

```bash
cd publish\api
set ASPNETCORE_URLS=http://localhost:5000
dotnet Tjrj.Biblioteca.Api.dll
```

A API ficará disponível em:

* [http://localhost:5000](http://localhost:5000)
* [http://localhost:5000/swagger](http://localhost:5000/swagger) (se Swagger estiver habilitado)

---

## 🌐 Publicação da Web (MVC Razor)

No projeto **Tjrj.Biblioteca.Web**, ajuste `appsettings.Production.json`:

```json
"ApiSettings": {
  "BaseUrl": "http://localhost:5000/"
}
```

### Gerar publish

```bash
dotnet publish .\Tjrj.Biblioteca.Web\ -c Release -o .\publish\web
```

### Executar localmente

```bash
cd publish\web
set ASPNETCORE_URLS=http://localhost:5002
dotnet Tjrj.Biblioteca.Web.dll
```

Acesse: **[http://localhost:5002](http://localhost:5002)**

---

## 🧪 Testes pós-implantação

### API

* `GET /api/autores`
* `GET /api/assuntos`
* `GET /api/livros`
* `GET /api/relatorios/livros-por-autor`

### Web

* Navegar em **Autores, Assuntos, Livros e Relatório**
* Criar/editar livros com múltiplos preços por forma de compra

---

## 📁 Estrutura do Repositório

```
Tjrj.Biblioteca.sln
├── src/
│   ├── Tjrj.Biblioteca.Api
│   ├── Tjrj.Biblioteca.Application
│   ├── Tjrj.Biblioteca.Infra
│   ├── Tjrj.Biblioteca.IoC
│   └── Tjrj.Biblioteca.Web
└── scripts/
```

---

## ✍️ Observações

* As **migrations do EF Core** estão no projeto **Infra**
* O relatório é baseado em **VIEW no SQL Server**, conforme exigido
* A Web consome a API via `HttpClient` tipado e serviços dedicados

---

## 📬 Contato

Fabiano da Silva Barbosa
Desenvolvedor .NET
61-983361654