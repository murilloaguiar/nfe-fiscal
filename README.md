Para lidar com falhas em produção é ideal verificar os registros de logs armazenados no banco de dados exclusivo. Apenas para teste e desenvolvimento estou salvando no sqlite a cada ação, batch 1.

### Se um job de exportação levasse 10 minutos, como você evitaria timeout no frontend?
Para evitar timeout de um job que leva 10 min, coloquei o job em uma fila no banco de dados para ser executada assincronamente. Assim o usuário e aplicação fica livre para executarem outras ações. A ideia é notificar o front end de que o job foi concluído ou não e assim o usuário tomar a ação após a conclusão.

### Além de logs estruturados, que outros pilares de observabilidade você implementaria em produção?
A utilização de um software de monitoramento como Zabbix em conjunto com o Grafana por serem open source.

### Se amanhã você tivesse que entregar esse módulo para outra equipe manter, o que faria diferente?
Documentaria melhor o código e o banco de dados.