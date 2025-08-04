

Install the nghttp2 client to test HTTP/2 endpoints.

```bash
sudo apt-get update && sudo apt-get install -y nghttp2-client

```

Use `h2load` to test the HTTP/1.1 endpoint for creating orders with a specific customer ID.
```bash
h2load -D 30s --warm-up-time 5s -c 5 https://localhost:5001/orders\?customerId\=1 --h1 -t 5
```

Use `h2load` to test HTTP/1.1 endpoint for creating orders, reading the URIs from requests.txt.
```bash
h2load -D 30s --warm-up-time 5s -c 5 -i requests.txt --h1 -t 5
```
Results from the HTTP/1.1 test:
```bash
finished in 35.00s, 2113.07 req/s, 13.95MB/s
requests: 63392 total, 63392 started, 63392 done, 63392 succeeded, 0 failed, 0 errored, 0 timeout
status codes: 63392 2xx, 0 3xx, 0 4xx, 0 5xx
traffic: 418.42MB (438744593) total, 7.96MB (8349457) headers (space savings 0.00%), 476.36MB (499498265) data
                     min         max         mean         sd        +/- sd
time for request:     1.06ms     17.62ms      2.36ms       641us    73.50%
time for connect:        0us         0us         0us         0us     0.00%
time to 1st byte:        0us         0us         0us         0us     0.00%
req/s           :     422.06      423.49      422.60        0.55    80.00%
```

Use `h2load` to test the HTTP/2 endpoint for creating orders, reading the URIs from requests.txt.
```bash
h2load -D 30s --warm-up-time 5s -c 5 -i requests.txt -t 5 -m 10
```
Results from the HTTP/2 test:
```bash
finished in 35.01s, 2441.17 req/s, 15.81MB/s
requests: 73235 total, 73235 started, 73235 done, 73235 succeeded, 0 failed, 0 errored, 0 timeout
status codes: 73240 2xx, 0 3xx, 0 4xx, 0 5xx
traffic: 474.31MB (497349293) total, 340.96KB (349140) headers (space savings 95.89%), 553.67MB (580560697) data
                     min         max         mean         sd        +/- sd
time for request:     3.73ms    120.42ms     20.42ms      6.72ms    72.95%
time for connect:        0us         0us         0us         0us     0.00%
time to 1st byte:        0us         0us         0us         0us     0.00%
req/s           :     483.65      491.58      488.18        3.16    60.00%
```