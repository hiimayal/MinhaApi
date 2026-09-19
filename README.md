# 🛒 Minha API

API REST para gerenciamento de produtos e categorias, desenvolvida com **C# e ASP.NET Core**. O projeto foi criado com foco em estudos de desenvolvimento backend, aplicando conceitos de APIs REST, banco de dados relacional, Entity Framework Core, autenticação, autorização e testes automatizados.


##📚 Sobre o projeto


Esta API é um projeto de estudos criado para experimentar novas tecnologias,
conceitos e ferramentas que vou aprendendo ao longo da minha evolução em
desenvolvimento backend.

A ideia é utilizar esta mesma API como um ambiente de prática, aplicando
continuamente novos conhecimentos e tecnologias na prática, evoluindo o projeto
conforme novos conceitos são aprendidos.


## 🚀 Tecnologias Utilizadas

- **C#** - Linguagem utilizada no desenvolvimento da aplicação
- **.NET 8** - Plataforma utilizada para desenvolvimento da API
- **ASP.NET Core** - Construção da API REST
- **Entity Framework Core** - ORM utilizado para comunicação com o banco de dados
- **SQL Server** - Banco de dados relacional
- **JWT** - Autenticação e autorização dos usuários
- **BCrypt** - Hash das senhas dos usuários
- **xUnit** - Testes unitários e de integração
- **Swagger** - Documentação e testes dos endpoints
- **Postman** - Testes das requisições HTTP
- **Git/GitHub** - Versionamento e gerenciamento do código

## Funcionalidades

- 📦 Produtos
- - ✅ Listagem de produtos
- - ✅ Busca de produto por ID
- - ✅ Cadastro de produtos
- - ✅ Atualização completa de produtos
- - ✅ Atualização parcial de produtos
- - ✅ Exclusão de produtos
- - ✅ Validação de dados
- - ✅ Verificação de categoria existente
- - ✅ Verificação de produto duplicado

- 👤 Usuários
- - ✅ Cadastro de usuários
- - ✅ Hash de senha utilizando BCrypt
- - ✅ Login
- - ✅ Geração de token JWT
- - ✅ Autenticação
- - ✅ Autorização baseada em roles
- - ✅ Perfis de acesso `Cliente` e `Admin`

- 🔐 Autenticação e Autorização
- - ✅ Autenticação baseada em JWT
- - ✅ Autorização baseada em roles
- - ✅ Perfil `Cliente`
- - ✅ Perfil `Admin`
- - ✅ Proteção de endpoints
- - ✅ Permitir operações administrativas somente para usuários `Admin`

- 🛡️ Tratamento de Erros
- - ✅ Middleware para tratamento global de exceções
- - ✅ Retorno de `400 Bad Request` para dados inválidos
- - ✅ Retorno de `401 Unauthorized` para requisições não autenticadas
- - ✅ Retorno de `403 Forbidden` para usuários sem permissão
- - ✅ Retorno de `404 Not Found` para recursos inexistentes
- - ✅ Retorno de `409 Conflict` para dados duplicados
- - ✅ Retorno de `500 Internal Server Error` para erros inesperados

- 🧪 Testes
- - ✅ Testes unitários com xUnit
- - ✅ Testes de integração
- - ✅ Testes de endpoints HTTP
- - ✅ Utilização de EF Core InMemory nos testes

- ## 📂 Estrutura do Projeto

📁 **Dtos**  
　📁 Produtos  
　📁 Usuarios  

📁 **Endpoints**  
　📄 ProdutoEndpoints.cs  
　📄 UsuarioEndpoints.cs  

📁 **Exceptions**  
　📄 EmailJaCadastradoException.cs  

📁 **Interfaces**  
　📄 IProdutoService.cs  
　📄 IUsuarioService.cs  

📁 **Middlewares**  
　📄 ExceptionMiddleware.cs  

📁 **Models**  
　📄 Produto.cs  
　📄 Categoria.cs  
　📄 Usuario.cs  

📁 **Services**  
　📄 ProdutoService.cs  
　📄 UsuarioService.cs  
　📄 JwtService.cs  

📁 **Migrations**  

📁 **Tests**  

📄 **Program.cs**  
📄 **MinhaApi.csproj**  
📄 **README.md**


## 🛠️ Como Executar o Projeto
```md
1️⃣ Clone o Repositório

    git clone https://github.com/hiimayal/MinhaApi.git
    cd MinhaApi

 2️⃣ Configure o Banco de Dados

Certifique-se de ter o **SQL Server** instalado e configure a connection string da aplicação.

3️⃣ Instale as Dependências

    dotnet restore

4️⃣ Execute as Migrations

    dotnet ef database update

5️⃣ Execute a API

    dotnet run

6️⃣ Acesse o Swagger

Após iniciar a aplicação, acesse o endereço HTTPS exibido no terminal.

Exemplo:

    https://localhost:7030/swagger

O Swagger permite visualizar e testar os endpoints da API, incluindo os endpoints protegidos por autenticação JWT.