#include <ESP8266WiFi.h>
#include <WebSocketServer.h>

#define SSID "ESP"
#define PORT 80
#define BAUD_RATE 115200

// Led indicating whether client is currently connected or not
#define CONNECTION_LED 2

WiFiServer server(PORT);
WiFiClient client;

void setup() {
  pinMode(CONNECTION_LED, OUTPUT);

  Serial.begin(BAUD_RATE);
  WiFi.mode(WIFI_AP);
  WiFi.softAP(SSID);
  server.begin();
}

void loop() {
  if(WiFi.softAPgetStationNum() != 0) {
    digitalWrite(CONNECTION_LED, HIGH);  
  }
  else {
    digitalWrite(CONNECTION_LED, LOW);  
  } 
  
  client = server.available();

  if (client) {
    while (client.connected()) {
      if (WiFi.softAPgetStationNum() == 0) {
        break;
      }

      if (client.available()) {
        String data = client.readStringUntil('\n');
        Serial.print(data);
      }
    }
    
    delay(100);
    client.stop();
  }
}
