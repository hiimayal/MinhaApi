# 🛒 Minha API

API REST para gerenciamento de produtos e categorias, desenvolvida com **C# e ASP.NET Core**. O projeto foi criado com foco em estudos de desenvolvimento backend, aplicando conceitos de APIs REST, banco de dados relacional, Entity Framework Core, autenticação, autorização e testes automatizados.

## 📚 Sobre o projeto

Esta API é um projeto de estudos criado para experimentar novas tecnologias,
conceitos e ferramentas que vou aprendendo ao longo da minha evolução em
desenvolvimento backend.

A ideia é utilizar esta mesma API como um ambiente de prática, aplicando
continuamente novos conhecimentos e tecnologias na prática, evoluindo o projeto
conforme novos conceitos são aprendidos.

## 🚀 Tecnologias Utilizadas

- **C#** - Linguagem utilizada no desenvolvimento da aplicação
- **.NET 8** - Plataforma utilizada para construção da API
- **ASP.NET Core** - Desenvolvimento da API REST
- **Entity Framework Core** - Acesso e manipulação do banco de dados
- **SQL Server** - Banco de dados relacional
- **JWT** - Autenticação e autorização
- **BCrypt** - Hash das senhas dos usuários
- **xUnit** - Testes automatizados
- **Swagger** - Documentação e testes dos endpoints
- **Postman** - Testes das requisições HTTP
- **Git/GitHub** - Versionamento do projeto

## 🎯 Funcionalidades

### 📦 Produtos

- ✅ Listagem de produtos
- ✅ Busca de produto por ID
- ✅ Cadastro de produtos
- ✅ Atualização completa de produtos
- ✅ Atualização parcial de produtos
- ✅ Exclusão de produtos
- ✅ Validação de dados
- ✅ Verificação de categoria existente
- ✅ Verificação de produto duplicado

### 👤 Usuários

- ✅ Cadastro de usuários
- ✅ Hash de senha utilizando BCrypt
- ✅ Login
- ✅ Geração de token JWT
- ✅ Autenticação
- ✅ Autorização baseada em roles
- ✅ Perfis de acesso `Cliente` e `Admin`

### 🔐 Controle de acesso

Os endpoints de gerenciamento de produtos possuem controle de acesso baseado na role do usuário.

- `Cliente` → pode consultar produtos
- `Admin` → pode criar, atualizar e excluir produtos

Requisições protegidas utilizam o token JWT através do header:
Authorization: Bearer {token}

📂 MinhaApi
 ├── 📁 Dtos
 │   ├── 📁 Produtos
 │   └── 📁 Usuarios
 │
 ├── 📁 Endpoints
 │   ├── ProdutoEndpoints.cs
 │   └── UsuarioEndpoints.cs
 │
 ├── 📁 Exceptions
 │   └── EmailJaCadastradoException.cs
 │
 ├── 📁 Interfaces
 │   ├── IProdutoService.cs
 │   └── IUsuarioService.cs
 │
 ├── 📁 Middlewares
 │   └── ExceptionMiddleware.cs
 │
 ├── 📁 Models
 │   ├── Produto.cs
 │   ├── Categoria.cs
 │   └── Usuario.cs
 │
 ├── 📁 Services
 │   ├── ProdutoService.cs
 │   ├── UsuarioService.cs
 │   └── JwtService.cs
 │
 ├── 📁 Migrations
 │
 ├── 📁 Tests
 │
 ├── 📄 Program.cs
 ├── 📄 MinhaApi.csproj
 └── 📄 README.md


🗄️ Banco de Dados
Categoria
    │
    │ 1:N
    ▼
Produto

Usuario

## 🎯 Funcionalidades

### 📦 Gerenciamento de Produtos

- ✅ Listar todos os produtos
- ✅ Buscar produto por ID
- ✅ Cadastrar produto
- ✅ Atualizar produto
- ✅ Atualizar parcialmente um produto
- ✅ Excluir produto
- ✅ Associar produto a uma categoria
- ✅ Validar dados de entrada
- ✅ Verificar se a categoria existe
- ✅ Impedir cadastro de produtos duplicados

### 👤 Gerenciamento de Usuários

- ✅ Cadastrar usuário
- ✅ Validar dados do cadastro
- ✅ Criptografar senhas utilizando BCrypt
- ✅ Realizar login
- ✅ Gerar token JWT
- ✅ Autenticar usuários através do token

### 🔐 Autenticação e Autorização

- ✅ Autenticação baseada em JWT
- ✅ Autorização baseada em roles
- ✅ Perfil `Cliente`
- ✅ Perfil `Admin`
- ✅ Proteção de endpoints
- ✅ Permitir operações administrativas somente para usuários `Admin`

### 🛡️ Tratamento de Erros

- ✅ Middleware para tratamento global de exceções
- ✅ Retorno de `400 Bad Request` para dados inválidos
- ✅ Retorno de `401 Unauthorized` para requisições não autenticadas
- ✅ Retorno de `403 Forbidden` para usuários sem permissão
- ✅ Retorno de `404 Not Found` para recursos inexistentes
- ✅ Retorno de `409 Conflict` para dados duplicados
- ✅ Retorno de `500 Internal Server Error` para erros inesperados

### 🧪 Testes

- ✅ Testes unitários com xUnit
- ✅ Testes de integração
- ✅ Testes de endpoints HTTP
- ✅ Utilização de EF Core InMemory nos testes