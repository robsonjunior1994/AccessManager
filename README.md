### **AccessManager: Sistema de Gerenciamento de Acessos**

#### O que é o AccessManager?

O **AccessManager** é uma solução centralizada para gerenciar a autenticação e autorização de usuários em diversas aplicações. A ideia é eliminar a necessidade de construir um sistema de gerenciamento de usuários do zero para cada novo projeto. Com o AccessManager, eu posso reutilizar para controlar quem pode acessar recursos específicos de qualquer aplicação.

---

#### Por que usar o AccessManager?

O principal objetivo do AccessManager é **garantir a segurança e o controle de acesso**. Ao centralizar o gerenciamento de usuários e permissões, a aplicação:

* **Reduz o tempo de desenvolvimento:** Reutilize um sistema de autenticação e autorização completo em vez de recriá-lo.
* **Melhora a segurança:** Como é um sistema focado nessa função, ao desenvolve-lo, estou colocando mais empenho em um objetivo apenas.
* **Simplifica a gestão de usuários:** Posso gerenciar os usuários de várias aplicações ou apenas retutilizar ele em cada projeto.

---

#### Como o AccessManager funciona?

O sistema é construído com base nas seguintes entidades:

**Classes:**
* **User:** Representa o usuário final.
    * `id`: Identificador único.
    * `name`: Nome completo.
    * `email`: Email do usuário, usado para login.
    * `password`: Senha criptografada.
    * `date_criacao`, `data_atualizacao`, `data_exclusao`: Para controle e auditoria.
    * `active`: Status do usuário (`true` ou `false`).
    * `roles`: Lista das permissões atribuídas ao usuário.
    * `companyId`: Referência à empresa à qual o usuário pertence.
* **Company:** Representa a empresa ou organização.
    * `id`: Identificador único.
    * `name`: Nome da empresa.
    * `number`: CNPJ ou CPF.
    * `date_criacao`, `date_atualizacao`, `date_exclusao`: Para controle e auditoria.
    * `active`: Status da empresa (`true` ou `false`).
    * `users`: Lista dos usuários que pertencem à empresa.

---

#### Funções da API

**Cadastro de Usuário** – Ao criar um User, será solicitado a criação de um uma company e esse usuário tem a regra de superuser dessa company.

**Autorização** – Usuário realiza login enviando suas credenciais e recebe um JWT como resposta.

**Autenticação** – Para acessar recursos, o usuário envia o JWT em cada requisição; a API valida o token e autoriza ou nega o acesso.

**Recuperação de Senha** (função interna – apenas superuser).

**Cadastro de Usuário vinculado a uma Company** – Possibilidade de criar usuários com diferentes perfis (ex.: usuário padrão) (função interna – apenas superuser).

**Atualização de Usuário** (função interna – apenas superuser).

**Exclusão de Usuário** (função interna – apenas superuser).
