# 📚 Articly

[![Continuous Integration](https://github.com/Willian-Brito/aiko-learning-server/actions/workflows/ci.yaml/badge.svg)](https://github.com/Willian-Brito/aiko-learning-server/actions/workflows/ci.yaml)

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/articly/refs/heads/main/docs/prints/logo.png" alt="logo" />
</div>

## 💻 Sobre o projeto
O **Articly** é um SaaS para gestão de artigos, desenvolvido para facilitar a criação, organização e acompanhamento de conteúdos de forma intuitiva e escalável. 

O Articly foi projetado para atender às necessidades de equipes e criadores de conteúdo que desejam **centralizar a gestão de artigos**, acompanhar métricas em tempo real e organizar categorias e permissões de forma eficiente.

## 🛠️ Tecnologias Utilizadas
- **Backend: Linguagens e Frameworks**
  - C#
  - .NET

- **Banco de Dados**
  - PostgreSQL
  - MongoDB

- **ORM e Data Access**
  - Entity Framework
  - Dapper

- **Testes  Automatizados**
  - xUnit
  - Moq
  - Fluent Assertions

- **Sincronização de Estatísticas**
  - FluentScheduler

- **Comunicação em Tempo Real**
  - SignalR

- **Frontend: Linguagens e Frameworks**
  - Html
  - Css
  - Javascript
  - Vue.js
  - BootstrapVue

- **Logs e Monitoramento**
  - Prometheus
  - Grafana

## ⚙️ Funcionalidades
- 🔑 **Autenticação e Autorização**
  - [x] Registro de novos usuários
  - [x] Login
  - [x] Logout

- 📝 **Gestão de Conteúdo**
  - [x] Cadastro e filtragem de categorias e artigos
  - [x] Gerenciamento de usuários e permissões

- 🔁 **Sincronização**
  - [x] Sincronização de estatísticas

- 💬 **Chat**
  - [x] Chat em tempo real

## 📁 Estrutura de Pastas

```
📦 articly
├── app/
│   │
│   ├── 🌐 client/               # Aplicação Vue.js
│   │   ├── public/              # Arquivos estáticos (favicon, index.html base etc.)
│   │   ├── src/                 
│   │   │   ├── assets/          # Imagens, ícones, fontes
│   │   │   ├── components/      # Componentes Vue reutilizáveis
│   │   │   ├── template/        # Layouts (Sidebar, Navbar, DashboardLayout etc.)
│   │   │   ├── pages/           # Páginas (Login, Home, Articles, Categories etc.)
│   │   │   ├── router/          # Configuração do Vue Router
│   │   │   ├── store/           # Vuex/Pinia (gerenciamento de estado)
│   │   │   ├── services/        # Comunicação com backend (Axios, APIs)
│   │   │   ├── utils/           # Helpers e funções utilitárias
│   │   │   └── main.js          # Ponto de entrada
│   │   └── package.json
│   │
│   └── 🖥 server/                # API em .NET
│       ├── src/
│       │   ├── Application/     # Casos de uso, CQRS, validações
│       │   ├── Domain/          # Entidades, agregados, repositórios (interfaces)
│       │   ├── Infrastructure/  # EF Core, Dapper, acesso a dados, Jobs, Segurança, serviços externos
│       │   ├── Tests/           # Testes unitários e de integração
│       │   └── Api/             # Controllers, middlewares, autenticação JWT
│       └── Articly.sln
│
├── docs/                    # Documentação (arquitetura, ADRs, manuais de dev)
├── .github/workflows/       # CI/CD (actions para build, testes e deploy)
├── docker-compose.yml       # Subir frontend + backend juntos localmente
├── 📄 README.md
└── ©️ LICENSE
```

## 📦 Padrões de Design Implementados
- [x] SOLID
- [x] APIs REST
- [x] Clean Architecture
- [x] CQRS
  - [x] Leitura: Dapper (MongoDB)
  - [x] Escrita: Entity Framework (PostgreSQL)
- [x] Repository Pattern
- [x] Unit of Work
- [x] Auditoria de Dados
- [x] Global Error Handler
- [x] Jobs para Sincronização de Estatísticas
- [x] Paginação de APIs
- [x] Testes Unitários
- [ ] Testes de Integração
- [ ] Testes e2e
- [x] Rate Limiting
- [x] Conteinerização
- [x] CI/CD Pipelines
  - [x] Versionamento
  - [x] Build
  - [x] Testes Unitários
  - [x] Gerar Docker Image
  - [x] Subir Imagem no Docker Hub
- [x] Security Pipeline (DevSecOps) 
  - [x] SAST - Static Application Security Testing
  - [x] SCA - Software Composition Analysis
  - [x] IaC - Infra as Code
  - [x] Security Container Scan
  - [x] DAST - Dynamic Application Security Testing
  - [x] Integração com DefectDojo
- [ ] Logs e Monitoramento

## 🛡️ Práticas DevSecOps
Este projeto foi desenvolvido com foco em DevSecOps, garantindo segurança em todas as etapas do ciclo de vida da aplicação (SDLC). Foram implementadas ferramentas de análise de código, dependências, infraestrutura e execução, além de integração com uma plataforma centralizada de gerenciamento de vulnerabilidades.

### ⚙️ Pipeline de Segurança (CI/CD)

Abaixo está um resumo das etapas executadas em cada build:

1. **Execução do Horusec (SAST):** Identificar vulnerabilidades no código-fonte antes da aplicação ser compilada ou executada.
2. **Execução do Dependency-Check (SCA):** Detectar bibliotecas e dependências vulneráveis.
3. **Análise de IaC com KICS:** Analisar arquivos de configuração e infraestrutura (Terraform, Kubernetes, Docker) para encontrar falhas de segurança antes do provisionamento.
4. **Varredura de containers com Trivy:** Analisar imagens Docker em busca de vulnerabilidades em pacotes do sistema operacional e bibliotecas de aplicação.
5. **Testes dinâmicos com OWASP ZAP (DAST):** Testar a aplicação em execução para detectar falhas como SQL Injection, XSS e exposição de dados sensíveis.
6. **Upload dos relatórios no DefectDojo:** Centralizar a gestão das vulnerabilidades no DefectDojo, garantindo uma visão unificada dos riscos identificados pelas ferramentas. Para isso, a pipeline utiliza uma [GitHub Action desenvolvida por mim](https://github.com/Willian-Brito/defect-dojo-action), que envia automaticamente os relatórios para o DefectDojo por meio da sua API.


#### 🛠️ Secure Pipeline
<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/articly/refs/heads/main/docs/prints/secure-pipeline.jpeg" />
</div>

#### 📊 Dashboard no DefectDojo
<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/articly/refs/heads/main/docs/prints/defectdojo.png" />
</div>

## 🔧 Instalação
>Se você deseja executar o projeto localmente sem o uso de containers, siga as instruções abaixo.
Caso prefira utilizar Docker, vá diretamente para a seção "Como executar o projeto".

1. **Pré-requisitos**: 
   - Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
    [Git](https://git-scm.com), [Node.js](https://nodejs.org/en/), [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0), [PostgreSQL](https://www.postgresql.org/download/) e [MongoDB](https://www.mongodb.com/try/download/community). 
    Além disto é bom ter um editor para trabalhar com o código como [VSCode](https://code.visualstudio.com/).

2. **Configuração do Banco de Dados**:
   - Configure o PostgreSQL e MongoDB, criando as bases de dados necessárias.
   - Atualize as strings de conexão no arquivo de configuração da aplicação.

3. **Instalando as Dependências**:
   ```bash
   $ dotnet restore
    ```

## 🚀 Como executar o projeto

> **Requisito:** É necessário ter o docker instalado em seu sistema operacional (Linux, Windows ou Mac)

Este projeto é divido em duas partes:
1. Backend ([Server](https://github.com/Willian-Brito/aiko-learning-server)) 
2. Frontend ([Client](https://github.com/Willian-Brito/articly))

#### 🛜 Criar Rede Docker
```bash
# Criar rede aiko-network para comunicação do front-end e back-end
$ docker network create aiko-network

# Verificando se a rede foi criada corretamente
$ docker network ls
```

#### 🖥 Rodando o Backend

```bash
# Clone este repositório
$ git clone https://github.com/Willian-Brito/aiko-learning-server

# Vá para a pasta server
$ cd aiko-learning-server

# Criar rede aiko-network para comunicação do front-end e back-end
$ docker network create aiko-network

# Execute o docker compose
$ docker-compose up --build

# O servidor inciará na porta:5066 - acesse http://localhost:5066/swagger/index.html
```

> Certifique-se de que as portas **5432 (PostgreSQL)** e **27017 (MongoDB)** não estejam sendo utilizadas por outros serviços em sua máquina local.

#### 🌐 Rodando o Frontend

```bash
# Clone este repositório
$ git clone https://github.com/Willian-Brito/articly

# Vá para a pasta da aplicação Front End
$ cd articly

# Execute o docker compose
$ docker-compose up --build

# A aplicação será aberta na porta:8081 - acesse http://localhost:8081
```

#### ✅ Executando os Testes do Backend
```bash
 $  dotnet test
```
**VS Code**
<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/articly/refs/heads/main/docs/prints/vs-code-tests.png" alt="vs code" />
</div>

**Terminal**
<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/articly/refs/heads/main/docs/prints/terminal-tests.png" alt="terminal" />
</div>

## 🎨 Layout 

#### Demo
<div align="center">
  <img src="https://github.com/Willian-Brito/articly/blob/main/src/assets/gif/demo.gif?raw=true" alt="demo" />
</div>

#### Login

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/articly/refs/heads/main/docs/prints/login-dark.png" alt="login" />
</div>

#### Registrar

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/articly/refs/heads/main/docs/prints/register-dark.png" alt="registrar usuário" />
</div>

#### Home

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/articly/refs/heads/main/docs/prints/home-dark.png" alt="home do sistema" />
</div>

#### Perfil

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/articly/refs/heads/main/docs/prints/profile-dark.png" alt="listagem de artigos" />
</div>

#### Listagem de Artigos

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/articly/refs/heads/main/docs/prints/articles-dark.png" alt="listagem de artigos" />
</div>

#### Gestão de Artigos

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/articly/refs/heads/main/docs/prints/admin-articles-dark.png" alt="gestão de artigos" />
</div>

#### Chat
<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/articly/refs/heads/main/docs/prints/chat-dark.png" alt="chat em tempo real" />
</div>

#### Swagger: Rotas do Backend

<div align="center">
  <img src="https://raw.githubusercontent.com/Willian-Brito/articly/refs/heads/main/docs/prints/swagger.png" alt="rotas do backend" />
</div>

## 📝 Licença

Este projeto esta sobe a licença [MIT](https://github.com/Willian-Brito/articly/blob/main/LICENSE).

Feito com ❤️ por Willian Brito 👋🏽 [Entre em contato!](https://www.linkedin.com/in/willian-ferreira-brito/)