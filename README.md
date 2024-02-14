# Prometheus a Grafana pro NEXT a nejen pro něj


## Monitoring - potřeby

* znát chování apliace/řešení z vyššího pohledu
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





