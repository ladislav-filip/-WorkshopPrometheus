# Prometheus a Grafana pro NEXT a nejen pro něj
Video: https://drive.google.com/file/d/189XdNZ_ynrfA05kORYxcwW9buiu_CEtu/view?usp=drive_link

## Monitoring - potřeby

* znát chování aplikace/řešení z vyššího pohledu
* logování je pro tento účel příliš detailní
* potřeba monitorovat nejen aplikaci, ale i prostředky
* potřeba monitovovat ne události, ale procesy (např. počty odeslanýc mailů za nějaký interval)
* potřeba sledovat trendy - tedy, že dochází k nárustu nebo poklesu
* potřeba efektní a efektivní vizualizace dat

## Použitelné nástroje

Mimo zde přezentované Grafany a Promethea
* Datadog - komerční řešení (praktocky jsem se s ním nesetkal)
* ApplicationInsight - řešení od MS v Azure (krátce použito na PPS)
* Amazon Cloudwatch - aktuálně snad používané v PPS
* Grafana Agent - rozšiřje funkcionalitu Promethea, dodává podporu OpenTelemetry

## Prometheus

* open source nástroj pro sběr dat
* jde o tzv. "timeseries" databázi, tedy databázi specializovanou na zpracovaní dat ve vztahu k času
* data sbírá ve formě "metrik", které jsou reprezenovány jak klíč = hodnota
* sběr dat je "pull", tedy sám Prometheus rozhoduje, kdy se na data zeptá
* metriky musí zdroj poskytovat ve formě API plain/text

## Grafana

* jde o vizualizační nástroj/aplikaci
* je to open source řešení
* jedná se o webovou aplikaci
* vizualizace probíha formou různých widgetů (grafy, teploměry, histogramy,...)
* je rozšiřitelná pomocí mnoha pluginů (i vlastnoručně psaných)
* má řadu napojení na datové zdroje (Oracle, MySQL, Influx, API,...)
* jako cílovku zde vidím vizualizaci metrik, nebo vizualizaci statistik či nabíranýc dat od běžících strojů/čidel

# Docker zprovoznění

Vytvoření prostředí pro simulaci provozu:
* přítrava sítě, spustit: ```docker network create fullcom```
* simulace prostředí poskytujícího metriky - NGINX
  * URL metric: http://localhost:8080/metrics-1.txt
* služba Prometheus s nastavením jobs
  * URL: http://localhost:9090
* služba Grafana napojena na Prometheus
  * URL: http://localhost:3000
  * Login/Heslo: admin/fullcom

## Simulace metrik

* textový soubor metrics-1.txt
* generátor náhodných hodnot v Powershell

## Nastavení Prometheus

* soubor prometheus.yml
* napojení simulovaných metrik + konfigurace
* spuštění simulátoru metrik

## Nastavení Grafana

* přidání datového zdroje Prometheus
  * URL zdroje: http://prometheus:9090
* vytvoření dashboardu
  * panel s grafem nad "workshop_long_running_gauge", název "LongRunning"
  * last 5 minut, refresh 10s
  * panel s Gaugem nad "workshop_long_running_gauge", název "LongRunning now"
  * panel s grafem nad "workshop_visit_count_total", název "Visitors", změna barvy/linky
  * uložení dashboardu, název Fullcom

## Prometheus detail

* široce používaný a společně s OpenTelemetry takový standard
* existuje na to spousta dokumentace
* velká podpora napříč technologiemi (.NET, Java, Go, Java)
* velice jednoduchá implementace
* podpora a vhodnost použití v cloudu a kontejnerech

### Vlastnosti

* funguje jako sběrač - pull metrik s kientů
* formát metrik je čistý plaintext
* metriky jsou ve tvaru klíč/hodnota
* data ukládá pouze krátkodobě, defaul je 15 dnů
* konvence pojmenování metrik:
  * slova se oddělují podtržítkem
  * používají se malá písmena
  * název by měl obsahovat identifikaci sledované entity a identifikaci hodnoty/aspektu
    * <prefix>.<podnázev>.<aspekt>
  * příklad: next_longrunning_processes_total => počet aktuálně běžících dlouhotrvajících procesů
  * pojmenování typu metrik:
    * "_total" => sumarizační hodnoty, např. počet requestů, hodnota pouze roste
    * "_gauge" => stavové hodnoty, mužou se zvětsovat i zmenšovat, např. procento využití RAM, CPU,...
    * "_duration_seconds" => pro histogramy, např. doba zpracování funkce, requestu,...

### Rozšíření

* Grafana - vizualizace dat
* Alertmanager - nástroj pro rozesílání upozornění
* Thanos - umožní dlouhodobé ukládání dat

### Exportery

* slouží pro sběr metrik tam, kde není přímá implementace
* existuje exportery snad na všechno
* sbírají data a převádějí jej do formátu čitelného pro Prometheus
* zajímávé exportery pro nás:
  * [Oracle DB Exporter](https://github.com/iamseth/oracledb_exporter) - umožní sledovat podrobné vytížení
  * [MySQL server Exporter](https://github.com/prometheus/mysqld_exporter)
  * [Node Exporter](https://github.com/prometheus/node_exporter) - poskytuje metriky HW/OS, jsko CPU, RAM,...
  * [Prometheus Tomcat Exporter](https://github.com/nlighten/tomcat_exporter) - exporter pro Apache Tomcat, tedy možnost pro Uniface

## .NET aplikace

* nuget balíček [Prometheus.Client](https://github.com/prom-client-net/prom-client?tab=readme-ov-file)
* konfigurace Program.cs
* nastavení metrik v endpointech HelperExtensions
* připojení do Promethea - nový job
* napojení v Grafana

## NEXT řešení

* vytížení CPU
* spotřeba RAM
* statistika dlouhotrvající requestů
* počty HTTP chyb s možností rozdělení dle stavového kódu
* nasazeno Polabské a Bohušovice
