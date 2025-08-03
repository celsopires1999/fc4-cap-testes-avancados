Instalar o Grafana K6 no Ubuntu:

```bash
https://grafana.com/docs/k6/latest/set-up/install-k6/

```
```bash
sudo gpg -k
sudo gpg --no-default-keyring --keyring /usr/share/keyrings/k6-archive-keyring.gpg --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys C5AD17C747E3415A3642D57D77C6C491D6AC1D69
echo "deb [signed-by=/usr/share/keyrings/k6-archive-keyring.gpg] https://dl.k6.io/deb stable main" | sudo tee /etc/apt/sources.list.d/k6.list
sudo apt-get update
sudo apt-get install k6
```

Criar um arquivo de teste de carga `load_test.js`:
```bash
k6 run load_test.js
```

Rodar o teste de carga e salvar os resultados em um arquivo JSON:
```bash
k6 run --summary-mode full --out json=results.json load_test.js
```

Ler os resultados do teste de carga em outro terminal:
```bash
tail -f results.json
```

Ver os resultados do teste de carga no dashboard do Grafana. Após executar o comando um web dashboard será iniciado. 
Veja a mensagem com o endereço do dashboard no terminal.
```bash
K6_WEB_DASHBOARD=true k6 run load_test.js
```

Rodar o bechmarking com o Grafana K6:
```bash
k6 run --summary-mode full benchmarking.js
```
