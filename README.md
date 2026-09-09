# 🛒 Loja API - API de Gerenciamento de Produtos

Uma API REST desenvolvida em C# com ASP.NET Core para gerenciamento de produtos e categorias, utilizando SQL Server como banco de dados.

O projeto foi desenvolvido com foco em boas práticas de organização, separação de responsabilidades, persistência de dados, validação de informações e construção de APIs REST.

## 🚧 Status do Projeto

🟢 **Em desenvolvimento**

A primeira versão da API possui as principais operações de CRUD, integração com banco de dados SQL Server, relacionamento entre produtos e categorias, DTOs, validações, tratamento global de exceções, operações assíncronas, interfaces e injeção de dependência.

Novas funcionalidades e melhorias poderão ser adicionadas conforme a evolução do projeto.

---

## 📌 Funcionalidades

### 📦 Produtos

- Listagem de produtos
- Busca de produto por ID
- Cadastro de produtos
- Atualização completa de produtos
- Atualização parcial de produtos
- Exclusão de produtos
- Validação dos dados recebidos
- Verificação de categoria existente
- Verificação de produtos duplicados

### 🗂️ Categorias

- Associação de produtos às categorias
- Relacionamento entre produtos e categorias através de chave estrangeira
- Consulta da categoria relacionada ao produto

### 🛡️ Validações e tratamento de erros

- Validação de campos obrigatórios
- Validação de preço
- Validação de categoria
- Retorno de `400 Bad Request` para dados inválidos
- Retorno de `404 Not Found` para recursos inexistentes
- Retorno de `409 Conflict` para conflitos de dados
- Retorno de `500 Internal Server Error` para erros inesperados
- Middleware global para tratamento de exceções

### ⚡ Processamento assíncrono

As operações de acesso ao banco de dados utilizam `async/await`, evitando bloqueios desnecessários durante operações de I/O.

### 🧩 Organização e arquitetura

- Separação entre Endpoints, Services, Models e DTOs
- Interfaces para definição de contratos
- Injeção de dependência
- Entity Framework Core para acesso ao banco
- Separação entre entidades do banco e objetos de entrada/saída da API

---

## 🛠️ Tecnologias utilizadas

### Back-end

- C#
- .NET 8
- ASP.NET Core
- Entity Framework Core
- LINQ
- REST API
- JSON

### Banco de dados

- Microsoft SQL Server
- SQL Server Management Studio (SSMS)

### Ferramentas

- Visual Studio Code
- Postman
- Git
- GitHub

---

## 🏗️ Arquitetura do projeto

O projeto utiliza uma organização baseada na separação de responsabilidades:

```text
MinhaApi/
├── Controllers/
│   └── ProdutoEndpoints.cs
│
├── Dtos/
│   ├── CriarProdutoDto.cs
│   ├── AtualizarProdutoDto.cs
│   ├── ProdutoAtualizadoParcialmenteDto.cs
│   └── ProdutoDto.cs
│
├── Interfaces/
│   └── IProdutoService.cs
│
├── Middlewares/
│   └── ExceptionMiddleware.cs
│
├── Models/
│   └── Produto.cs
│
├── Services/
│   └── ProdutoService.cs
│
├── LojaDbContext.cs
├── Program.cs
└── appsettings.json