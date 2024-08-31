<h1 align="center">
      Qual é a Resposta
</h1>

<h4 align="center"> 
	Status do Projeto: em desenvolvimento
</h4>

<p align="center">
 <a href="#-sobre-o-projeto">Sobre</a> •
 <a href="#%EF%B8%8F-funcionalidades">Funcionalidades</a> •
 <a href="#-como-executar-o-projeto">Como executar</a> • 
 <a href="#-tecnologias">Tecnologias</a> • 
 <a href="#user-content--licença">Licença</a>
</p>

## 💻 Sobre o projeto

"Qual é a Resposta" é uma API desenvolvida em .NET 8 que consome o ChatGPT para obter respostas a partir das perguntas enviadas, seja com alternativas ou sem. O projeto adota a Onion Architecture, garantindo um design modular e sustentável, com foco em boas práticas de desenvolvimento e Clean Code.

A aplicação utiliza uma variedade de tecnologias modernas, incluindo:

- **C#** para a lógica de negócios.
- **Entity Framework Core** para mapeamento objeto-relacional (ORM).
- **SQL Server** como banco de dados.
- **AutoMapper** para mapeamento de objetos.
- **Serilog** para logging estruturado.
- **Polly** para implementação de resiliência e políticas de retry.
- **SignalR** para comunicação em tempo real.
- **Hangfire** para gerenciamento de tarefas em segundo plano.
- **FluentValidation** para validação de dados.
- **BenchmarkDotNet** para análise de desempenho.

---

## ⚙️ Funcionalidades

- [x] Obter respostas a partir das perguntas enviadas, seja com alternativas ou sem.
- [x] Testes para autenticação e autorização com Identity .Net 8 - EASY AUTH
- [x] Comunicação em tempo real com SignalR.
- [x] Execução de tarefas em segundo plano com Hangfire.
- [x] Logging estruturado e detalhado com Serilog.
- [x] Resiliência e políticas de retry implementadas com Polly.
- [x] Validação de dados robusta utilizando FluentValidation.

---

## 🚀 Como executar o projeto

Este projeto é composto pela API backend:

### Pré-requisitos

Antes de começar, você precisará ter instalado em sua máquina as seguintes ferramentas:

- [Git](https://git-scm.com)
- [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

Além disso, é recomendado ter um editor para trabalhar com o código, como [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/) ou [VSCode](https://code.visualstudio.com/).

### 🎲 Rodando a API

```bash

# Clone este repositório
git clone https://github.com/PauloDiegoDeOliveira/qual-e-a-resposta

# Acesse a pasta do projeto no terminal/cmd
cd qual-e-a-resposta

# Execute a aplicação
dotnet run --project QualEaResposta.Api

# A API estará disponível na porta:44304 - acesse http://localhost:44304

```

---

## 🎯 Executando Benchmarks

Para executar os benchmarks e avaliar o desempenho da aplicação, siga os passos abaixo:

```bash

# Navegue até o diretório de benchmarks
cd QualEaResposta.Benchmarks

# Compile o projeto em modo Release
dotnet build -c Release

# Execute o benchmark
dotnet run -c Release

```

---

## 🛠 Tecnologias

As seguintes ferramentas e bibliotecas foram utilizadas na construção do projeto:

#### **Backend**

- [C#](https://docs.microsoft.com/pt-br/dotnet/csharp/)
- **Entity Framework Core** para mapeamento objeto-relacional (ORM).
- **SQL Server** como banco de dados.
- **AutoMapper** para mapeamento de objetos.
- **Serilog** para logging estruturado.
- **Polly** para implementação de resiliência e políticas de retry.
- **SignalR** para comunicação em tempo real.
- **Hangfire** para gerenciamento de tarefas em segundo plano.
- **FluentValidation** para validação de dados.
- **BenchmarkDotNet** para análise de desempenho.

---

#### [](https://github.com/tgmarinho/Ecoleta#utilit%C3%A1rios)**Utilitários**

- **IDE: Visual Studio**
- **Editor: Visual Studio Code**
- **Teste de API: Postman**

## 📚 Conceitos e Arquitetura

- **Onion Architecture** - Arquitetura que promove separação de preocupações e alta testabilidade.
- **Clean Code** - Padrões para um código legível, eficiente e de fácil manutenção.

---

## 📝 Licença

Este projeto está sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.

---

Desenvolvido por Paulo Diego de Oliveira 🐭 (1984-2024).
