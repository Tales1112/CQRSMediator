<div class="entry-content" bis_skin_checked="1">
		<h2 style="text-align: justify;">CQRS é uma daquelas siglas que está cada vez mais presente em nossas leituras, muitas vezes encontramos o CQRS sendo citado em conteúdos sobre DDD ou padrões de arquitetura escaláveis.</h2>
<p style="text-align: justify;">CQRS é um conceito muito importante e você precisa conhecer. Eu costumo dizer que todo arquiteto possui uma “caixa de ferramentas” e o CQRS é o tipo de ferramenta que precisa estar presente na sua caixa.</p>
<h2 style="text-align: justify;"><strong>O que é CQRS?</strong></h2>
<p style="text-align: justify;">CQRS significa&nbsp;Command Query Responsibility Segregation. Como o nome já diz, é sobre separar a responsabilidade de escrita e leitura de seus dados.</p>
<p style="text-align: justify;">CQRS é um <strong>pattern</strong>, um&nbsp;padrão arquitetural assim como Event Sourcing, Transaction Script e etc. O CQRS <span style="text-decoration: underline;">não é um estilo arquitetural</span>&nbsp;como desenvolvimento em camadas, modelo client-server, REST e etc.</p>
<h2 style="text-align: justify;">Onde posso aplicar o CQRS?</h2>
<p style="text-align: justify;">Antes de entrar neste ponto vamos entender os cenários clássicos do dia a dia e depois veremos como o CQRS poderia ser aplicado como solução.</p>
<p style="text-align: justify;">Hoje em dia não desenvolvemos mais aplicações&nbsp;para 10 usuários simultâneos, a maioria das novas aplicações&nbsp;nascem com premissas de escalabilidade, performance e disponibilidade. Como uma aplicação pode funcionar bem com 10 ou 10.000&nbsp;usuários simultaneamente? É uma tarefa complexa criar um modelo conceitual que atenda essas necessidades.</p>
<p style="text-align: justify;">Imagine um sistema de SAC onde diversos atendentes num call-center consultam e modificam as informações de um cadastro de clientes enquanto outra área operacional da empresa também trabalha com os mesmos dados simultaneamente. Os dados do cliente são modificados constantemente e nenhuma das áreas tem tempo e paciência para esperar os possíveis “locks” da aplicação, o cliente quer ser atendido com agilidade.</p>
<p style="text-align: justify;">No mesmo cenário a aplicação pode possuir picos diários ou sazonais de acessos. Como impedir o tal “gargalo” e como manter a disponibilidade da aplicação em qualquer situação?</p>
<p style="text-align: justify;">– Ah! Vamos escalar nossa aplicação em N&nbsp;servidores. Podemos&nbsp;migrar para a nuvem (cloud-computing ex. <a href="https://azure.microsoft.com" target="_blank">Azure</a>) e criar um script de elasticidade (<a href="https://azure.microsoft.com/en-us/documentation/articles/best-practices-auto-scaling/" target="_blank">Autoscaling</a>) para escalar conforme a demanda.</p>
<p style="text-align: justify;">O conceito de escalabilidade da aplicação vai resolver alguns problemas de disponibilidade como por exemplo suportar muitos usuários simultaneamente sem comprometer a performance da aplicação.</p>
<p style="text-align: justify;">Mas será que só escalar os servidores de aplicação resolve todos os nossos problemas?</p>
<p style="text-align: justify;"><strong>Problema # 1</strong></p>
<p style="text-align: justify;">Deadlocks, timeouts e lentidão, seu banco pode estar em chamas.</p>
<p style="text-align: justify;"><img loading="lazy" class="aligncenter wp-image-2662" title="CQRS Banco em Chamas" src="https://i1.wp.com/www.eduardopires.net.br/wp-content/uploads/2016/07/CQRS_BancoEmChamas.jpg?resize=372%2C247&amp;ssl=1" alt="CQRS Banco em Chamas" width="372" height="247"></p>
<p style="text-align: justify;">Escalar a aplicação não é uma garantia de que a aplicação vai estar sempre disponível. Não podemos esquecer que neste suposto cenário todo processo depende também da disponibilidade do banco de dados.</p>
<p style="text-align: justify;">Escalar o banco de dados pode ser muito mais complexo (e caro) do que escalar servidores de aplicação. E geralmente é devido o consumo do banco de dados que as aplicações apresentam problemas de performance.</p>
<p style="text-align: justify;"><strong>Problema # 2</strong></p>
<p style="text-align: justify;">Para se obter um dado muitas vezes é necessário passar por um conjunto complexo de regras de negócio que irá&nbsp;filtrar a informação antes dela ser exibida, além disso existem os ORM’s que mapeiam o banco de dados em objetos de domínio realizando consultas com joins em diferentes tabelas para retornar todo conjunto de dados necessários.<br>
Tudo isto custa um tempo precioso até que o usuário receba a informação esperada.</p>
<p style="text-align: justify;"><strong>Problema # 3</strong></p>
<p style="text-align: justify;">Um conjunto limitado de dados é consultado e alterado constantemente por uma grande quantidade de usuários simultaneamente conectados. Um dado exibido na tela já pode ter sido alterado por outro. Numa visão realista é possível afirmar que toda informação exibida já pode estar obsoleta.</p>
<h2 style="text-align: justify;">Ponto de partida</h2>
<p style="text-align: justify;">Ter N servidores consumindo um único banco de dados que serve de leitura e gravação pode ocasionar muitos locks nos dados e com isso ocasionar diversos problemas de performance, assim como todo o processo da regra de negócio que vai obter os dados de exibição cobra um tempo a mais no processamento. No final ainda temos que considerar que o dado exibido já pode estar desatualizado.</p>
<p style="text-align: justify;">É este o ponto de partida do CQRS. Já que uma informação exibida não é necessariamente a informação atual então <span style="text-decoration: underline;">a obtenção deste dado para a exibição não necessita ter sua performance afetada devido a gravação, possíveis locks ou disponibilidade do banco</span>.</p>
<p style="text-align: justify;">O CQRS prega a divisão de responsabilidade de gravação e escrita de forma <span style="text-decoration: underline;">conceitual e física</span>. Isto significa que além de ter meios separados para gravar e obter um dado os bancos de dados também são diferentes. As consultas são feitas de forma <span style="text-decoration: underline;">síncrona</span> em uma base desnormalizada separada e as gravações de forma <span style="text-decoration: underline;">assíncrona</span> em um banco normalizado.</p>
<p style="text-align: justify;"><img loading="lazy" class="aligncenter wp-image-2665 size-full" title="CQRS Fluxo Simples" src="https://i0.wp.com/www.eduardopires.net.br/wp-content/uploads/2016/07/CQRS_FluxoSimples.jpg?resize=437%2C314&amp;ssl=1" alt="CQRS Fluxo Simples" width="437" height="314"></p>
<p style="text-align: justify;">Este é um fluxo simplificado do CQRS que&nbsp;não leva em consideração as camadas de aplicação, domínio e infra, comandos / eventos e enfileiramento de mensagens.</p>
<p style="text-align: justify;"><span style="text-decoration: underline;">O CQRS não é um padrão arquitetural de alto-nível</span>, podemos entender como uma forma de componentizar parte de sua aplicação.&nbsp;Podemos entender então que a utilização do CQRS não precisa estar presente em todos os processos de sua aplicação. Numa modelagem baseada em DDD um <a href="https://www.eduardopires.net.br/2016/03/ddd-bounded-context/" target="_blank">Bounded Context</a> pode implementar o CQRS enquanto os demais não.</p>
<p style="text-align: justify;">Não existe uma única maneira de implementar o CQRS na sua aplicação, pode ser feito de uma forma simples ou muito complexa, depende da necessidade. Independente de como for implementado o CQRS sempre acarreta&nbsp;numa complexidade extra e por isso é necessário avaliar os cenários em que realmente são necessários trabalhar com este padrão.</p>
<h2 style="text-align: justify;">Entendendo melhor o CQRS</h2>
<p style="text-align: justify;">A ideia básica é segregar as responsabilidades da aplicação em:</p>
<ul style="text-align: justify;">
<li style="text-align: justify;">Command – Operações que modificam o estado dos dados na aplicação.</li>
<li style="text-align: justify;">Query – Operações que recuperam informações dos dados na aplicação.</li>
</ul>
<p style="text-align: justify;">Numa arquitetura de N camadas poderíamos pensar em separar as responsabilidades em&nbsp;CommandStack e QueryStack.</p>
<p style="text-align: justify;"><strong>QueryStack</strong></p>
<p style="text-align: justify;">A QueryStack é muito mais simples que a CommandStack, afinal a responsabilidade dela é recuperar dados praticamente prontos para exibição. Podemos entender que a QueryStack é uma camada síncrona que recupera os dados de um banco de leitura desnormalizado.</p>
<p style="text-align: justify;">Este banco desnormalizado pode ser um NoSQL como <a href="https://www.mongodb.com/" target="_blank">MongoDB</a>, <a href="http://redis.io/" target="_blank">Redis</a>, <a href="https://ravendb.net/" target="_blank">RavenDB</a> etc.<br>
O conceito de desnormalizado pode ser aplicado com “one table per view” ou seja uma consulta “flat” que retorna todos os dados necessários para ser exibido em uma view (tela) específica.</p>
<p style="text-align: justify;">O uso de consultas “flats” em um banco desnormalizado&nbsp;evita&nbsp;a necessidade de joins, tornando as consultas muito mais rápidas. É preciso&nbsp;aceitar que haverá a duplicidade de dados para poder atender este modelo.</p>
<p style="text-align: justify;"><strong>CommandStack</strong></p>
<p style="text-align: justify;">O CommandStack por sua vez é potencialmente assíncrono. É nesta separação&nbsp;que estão as entidades, regras de negócio, processos e etc. Numa abordagem DDD podemos entender que o Domínio pertence a esta parte da aplicação.</p>
<p style="text-align: justify;">O CommandStack segue uma abordagem <em>behavior-centric</em>&nbsp;onde toda intenção de negócio é inicialmente disparada pela UI como um caso de uso. Utilizamos o conceito de Commands para representar uma intenção de negócio.&nbsp;Os Commands são declarados de forma imperativa (ex. FinalizarCompraCommand) e são disparados assincronamente no formato de eventos, são interpretados pelos CommandHandlers e retornam um evento de sucesso ou falha.</p>
<p style="text-align: justify;">Toda vez que um Command é disparado e altera o estado de uma entidade no banco de gravação um processo tem que ser disparado para os agentes que irão atualizar os dados necessários no banco de leitura.</p>
<p style="text-align: justify;"><strong>Sincronização</strong></p>
<p style="text-align: justify;">Existem algumas estratégias para manter as bases de leitura e gravação sincronizadas é necessário escolher a que melhor atende ao seu cenário:</p>
<ul style="text-align: justify;">
<li><span style="text-decoration: underline;">Atualização automática</span> –&nbsp;Toda alteração de estado de um dado no banco de gravação&nbsp;dispara um processo <span style="text-decoration: underline;">síncrono</span> para atualização no banco de leitura.</li>
<li><span style="text-decoration: underline;">Atualização eventual</span> –&nbsp;Toda alteração de estado de um dado no banco de gravação&nbsp;dispara um processo <span style="text-decoration: underline;">assíncrono</span> para atualização no banco de leitura oferecendo uma consistência eventual dos dados.</li>
<li><span style="text-decoration: underline;">Atualização controlada</span> –&nbsp;Um processo periódico e agendado é disparado para sincronizar as bases.</li>
<li><span style="text-decoration: underline;">Atualização sob demanda</span> –&nbsp;Cada consulta verifica a consistência da base de leitura em comparação com a de gravação e força uma atualização caso esteja desatualizada.</li>
</ul>
<p style="text-align: justify;">A <span style="text-decoration: underline;">atualização eventual</span> é uma das estratégias mais utilizadas, pois parte do princípio que todo dado exibido já pode estar desatualizado, portanto não é necessário impor um processo síncrono de atualização.</p>
<p style="text-align: justify;"><strong>Enfileiramento</strong></p>
<p style="text-align: justify;">Muitas implementações de CQRS podem exigir um “Bus” para processamento de Commands e Events. Nesse caso teremos uma implementação conforme a seguinte ilustração.</p>
<p style="text-align: justify;"><img loading="lazy" class="aligncenter wp-image-2671 size-full" title="CQRS BUS" src="https://i1.wp.com/www.eduardopires.net.br/wp-content/uploads/2016/07/CQRS_BUS.jpg?resize=584%2C357&amp;ssl=1" alt="CQRS BUS" width="570" height="348"></p>
<p style="text-align: justify;">Existem diversas&nbsp;opções de Bus para .NET</p>
<ul style="text-align: justify;">
<li><a href="https://azure.microsoft.com/en-us/documentation/articles/service-bus-dotnet-get-started-with-queues/" target="_blank">Microsoft Azure Service Bus Queue</a></li>
<li><a href="http://particular.net/nservicebus" target="_blank">NServiceBus</a></li>
<li><a href="https://github.com/rebus-org/Rebus" target="_blank">Rebus</a>&nbsp;(Free)</li>
<li><a href="https://github.com/MassTransit/MassTransit" target="_blank">MassTransit</a>&nbsp;(Free)</li>
</ul>
<h2 style="text-align: justify;"><strong>Vantagens de implementar o CQRS</strong></h2>
<p style="text-align: justify;">A implementação do CQRS quebra o conceito monolítico clássico de uma implementação de arquitetura em N camadas onde todo o processo de escrita e leitura passa pelas&nbsp;mesma camadas e concorre entre si no processamento de regras de negócio e uso de banco de dados.</p>
<p style="text-align: justify;">Este tipo de abordagem aumenta a disponibilidade e escalabilidade da aplicação e a melhoria na performance surge principalmente pelos&nbsp;aspectos:</p>
<ul style="text-align: justify;">
<li>Todos comandos são assíncronos e processados em fila, assim diminui-se o tempo de espera.</li>
<li>Os processos que envolvem regras de negócio existem apenas no sentido da inclusão ou alteração do estado das informações.</li>
<li>As consultas na QueryStack&nbsp;são feitas de forma separada e independente e não dependem do processamento da CommandStack.</li>
<li>É possível escalar separadamente os processos da CommandStack e da QueryStack.</li>
</ul>
<p style="text-align: justify;">Uma outra vantagem de utilizar o CQRS é que toda representação do seu domínio será mais expressiva e&nbsp;reforçará a utilização da linguagem ubíqua nas intenções de negócio.</p>
<p style="text-align: justify;">Toda a implementação do CQRS pattern pode ser feito manualmente, sendo&nbsp;necessário escrever diversos tipos de classes para cada aspecto, porém é possível encontrar alguns frameworks de CQRS que vão facilitar um pouco a implementação e reduzir o tempo de codificação.</p>
<p style="text-align: justify;">Apesar da minha preferência ser sempre codificar tudo por conta própria eu encontrei alguns frameworks bem interessantes que servem inclusive para estudo e melhoria do entendimento no assunto.</p>
<ul style="text-align: justify;">
<li><a href="https://github.com/lokad/lokad-cqrs/" target="_blank">Lokad-CQRS</a></li>
<li><a href="https://github.com/pjvds/ncqrs" target="_blank">NCQRS</a></li>
<li><a href="https://github.com/gautema/CQRSlite" target="_blank">CQRS Lite</a></li>
</ul>
<h2 style="text-align: justify;">Mitos sobre o CQRS</h2>
<p style="text-align: justify;"><strong>#1 Mito – CQRS e Event Sourcing devem ser implementados juntos.</strong></p>
<p style="text-align: justify;">O Event Sourcing é um outro pattern assim como o CQRS. É uma abordagem que nos permite guardar todos os estados assumidos por uma uma entidade desde sua criação.&nbsp;O Event Sourcing tem uma forte ligação com o CQRS e é facilmente implementado uma vez que temos também o CQRS, porém é possível implementar Event Sourcing independente do CQRS e vice-versa.</p>
<p style="text-align: justify;">Escreverei sobre Event Sourcing em breve num outro artigo.</p>
<p style="text-align: justify;"><strong>#2&nbsp;Mito – CQRS requer consistência eventual</strong></p>
<p style="text-align: justify;">Negativo. Como abordado anteriormente o CQRS pode trabalhar com uma consistência imediata e síncrona.</p>
<p style="text-align: justify;"><strong>#3 Mito – CQRS&nbsp;depende de Fila/Bus/Queues</strong></p>
<p style="text-align: justify;">CQRS é dividir as responsabilidades de Queries e Commands, a necessidade de enfileiramento vai surgir dependendo de sua implementação, principalmente se for utilizar a estratégia de consistência eventual.</p>
<p style="text-align: justify;"><strong>#4 Mito – CQRS é fácil</strong></p>
<p style="text-align: justify;">Não é fácil. O CQRS também não é uma ciência de foguetes. A implementação vai&nbsp;exigir uma complexidade extra em sua aplicação&nbsp;além de um claro entendimento do domínio e da linguagem ubíqua.</p>
<p style="text-align: justify;"><strong>#5 Mito – CQRS é arquitetura</strong></p>
<p style="text-align: justify;">Não é! Conforme foi abordado o CQRS é um pattern arquitetural e pode ser implementado em uma parte específica da sua aplicação para um determinado conjunto de dados apenas.</p>
		</div>
