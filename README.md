# BiomedicalDataService
.NET service for receiving data from medical sensors

## Run Locally

Clone the project

```bash
  git clone https://github.com/kawson1/biomedical-data-service.git
```

Go to the project directory

```bash
  cd biomedical-data-service
```

Start container with docker compose file

```bash
  docker compose -p project up -d
```

Default MQTT credentials
```
login: user1
password: user1
```

Default port mapping for MQTT is `1884:1883`


## Usage/Examples of MQTT
Topic subscription
```
docker exec -it <container_id> mosquitto_sub -v -t "example_topic" -u user1 -P user1
```
If you want to publish message to `example_topic` topic 
```
mosquitto_pub -h localhost -p 1884 -t example_topic -m "message" -u user1 -P user1
```

## Data Generator
This project provides REST API controllers and services for the purpose of generating and sending data to MQTT queue.

#### Send message

```http
  POST /api/mqtt/send?topic={topic}&message={message}
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `topic` | `string` | MQTT topic |
| `message` | `string` | MQTT message |


#### Start generating values

```http
  GET /api/mqtt/startgenerator?topic={topic}&minValue={minValue}&maxValue={maxValue}&timeStamp={timeStamp}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `topic` | `string` | MQTT topic |
| `minValue` | `int` | Minimal random value |
| `maxValue` | `int` | Maximal random value |
| `timeStamp` | `int` | Delay between generating (ms) |


#### Stop generating values

```http
  GET /api/mqtt/stopgenerator?topic={topic}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `topic` | `string` | Topic of generator to stop |
