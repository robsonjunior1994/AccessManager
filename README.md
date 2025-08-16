### **AccessManager: Sistema de Gerenciamento de Acessos**

#### O que é o AccessManager?

O **AccessManager** é uma solução centralizada para gerenciar a autenticação e autorização de usuários em diversas aplicações. A ideia é eliminar a necessidade de construir um sistema de gerenciamento de usuários do zero para cada novo projeto. Com o AccessManager, você terá uma base robusta e reutilizável para controlar quem pode acessar recursos específicos de qualquer aplicação.

---

#### Por que usar o AccessManager?

O principal objetivo do AccessManager é **garantir a segurança e o controle de acesso**. Ao centralizar o gerenciamento de usuários e permissões, a aplicação:

* **Reduz o tempo de desenvolvimento:** Reutilize um sistema de autenticação e autorização completo em vez de recriá-lo.
* **Melhora a segurança:** Um sistema unificado é mais fácil de manter, auditar e proteger contra vulnerabilidades.
* **Simplifica a gestão de usuários:** Gerencie usuários, empresas e permissões em um único lugar.

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

#### Tarefas

#01 - Criar função de cadastrar usuário e empresa

O que ?
Criar uma função que permita cadastrar um novo usuário e a empresa associada a ele.

Como ?

Endpint: `/api/user`
Método: `POST`

Parâmetros:
```json
{
  "name": "Nome do usuário",
  "email": "Email do usuário",
  "password": "Senha do usuário",
  "company": {
    "name": "Nome da empresa",
    "number": "CNPJ ou CPF da empresa"
}
```

User receberá a permissão padrão de "superuser" ao ser cadastrado.

User não estará ativo por padrão, até que o usuário confirme o email, 
    - ao cadastrar o usuário e a empresa, faremos o envio da mensagem para uma fila no RabbiMQ para termos o serviço de envio de email ASYC desacoplado do sistema.
    - Faremos o envio de um email de confirmação atráves de um workservice 
    - Após ele clicar no link enviado para o email chamaremos uma rota para atualizar o usuário em nossa base de dados e ativar o usuário.







