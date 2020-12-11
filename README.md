# Templatez CI - API [.NETCore API]

**.NETCore API** application template with continuous integration.


**Migrations**

Na versão 3.0 do dotnet é nescessário instalar global o pacote.

dotnet tool install --global dotnet-ef

Para utilizar os comandos de migração de dados o mesmo deve ser executado dentro da raiz do projeto 'Templatez.Infra.Data.Core' referenciando o projeto da API;
Exemplos:

- (Criar Migration)    => dotnet ef migrations add Initial -s ../Templatez.Api
- (Atualizar Database) => dotnet ef database update -s ../Templatez.Api
