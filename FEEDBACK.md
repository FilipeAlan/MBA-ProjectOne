# Feedback do Instrutor

#### 17/10/24 - Revisão Inicial - Eduardo Pires

## Pontos Positivos:

- Boa separação de responsabilidades.
- Demonstrou conhecimento em Identity e JWT
- Bom uso de mapeador AutoMapper
- Arquitetura enxuta de acordo com a complexidade do projeto
- Mostrou entendimento do ecossistema de desenvolvimento em .NET

## Pontos Negativos:

- Aparentemente o projeto está incompleto na parte das aplicações Web, as controllers não fazem controle de usuário, é possível excluir um post de outro usuário.
- Não vi sentido em uma camada apenas para IoC, dava para transformar a camada Data em "Core ou Application" e manter ela para atender todo o projeto.

## Sugestões:

- Será fornecido feedback na data final.

## Problemas:

- Não consegui executar a aplicação de imediato na máquina. É necessário que o Seed esteja configurado corretamente, com uma connection string apontando para o SQLite.

  **P.S.** As migrations precisam ser geradas com uma conexão apontando para o SQLite; caso contrário, a aplicação não roda.
