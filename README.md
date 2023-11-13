# [appMusic](https://github.com/jotabtw/appMusic/tree/main)

O appMusic tem como objetivo aplicar conhecimentos em prática sobre como realizar um CRUD em ASP.Net (.Net Framework) e também fazer conexão com o banco de dados SQL Server. O projeto ainda está em desenvolvimento, mas no momento já possui conceitos importantes para um CRUD e também para o SGBD.

Falando primeiramente do SGBD, antes de começar o projeto achei que seria necessário apenas os tratamento de algumas colunas que poderiam receber valoras Nulo, mas diante do método Post, vi a necessidade de ligar as tabelas com uma trigger, conceito qual só havia ouvido falar e foi necessário aplicar, isso se deve a possuir uma tabela chamada musica que recebe o tempo de uma musica e uma tabela playlist que recebe a duração total das músicas naquela playlist, assim, tive colocara um gatilho entre elas, o Trigger. 

Outro conceito qual busquei compreender foi o da anotação ActionName, pois me vi diante de uma necessidade, qual seria ter um método que buscasse quais músicas estariam presentes em determinada playlist. Foi necessário o uso da anotação, pois já existiam métodos do verbo Get que utilizavam o mesmo parâmetro que viria a ser usado no método para receber as músicas de determinada playlist, junto a isso, também foi possível criar um Model apenas para retornar os dados dessa requisição, já que seriam retornados dados de duas tabelas do banco de dados.

- O projeto ainda esta em desenvolvimento, o conteúdo sobre Música em Controllers, Models e Repositories já estão finalizados.
- Há um método Put no ambiente de música, porém, acredito que não há a necessidade da existência de um no contexto atual.

Algumas pastas importantes do projeto:

## [Configurations](https://github.com/jotabtw/appMusic/tree/main/playlist-api/Configurations)
Tem como intuito retornar a string para conexão do banco de dados, qual utilizaremos como valor de um parâmetro posteriormente;

## [Controllers](https://github.com/jotabtw/appMusic/tree/main/playlist-api/Controllers)
Os controladores serão responsáveis pelas ações dos verbos, havendo também as validações necessárias para caso aconteça algum erro, seja pela passagem de parâmetros, pelo formato do JSON no corpo da requisição ou pelas regras criadas para cada método.

## [Models](https://github.com/jotabtw/appMusic/tree/main/playlist-api/Models) 
Os modelos serão responsáveiis por criar o modelo da classe e quais propriedades terão;

## [Repositories](https://github.com/jotabtw/appMusic/tree/main/playlist-api/Repositories)
O repositório será onde encapsularemos os métodos em que o controlador vai realizar, assim, isolando o acesso aos dados 
