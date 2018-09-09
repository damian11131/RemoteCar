#include <ESP8266WiFi.h>

#define SSID "ESP"
#define PORT 80
#define BAUD_RATE 115200

// Led indicating whether client is currently connected or not
#define CONNECTION_LED 2

WiFiServer server(PORT);

void setup() {
  pinMode(CONNECTION_LED, OUTPUT);
  
  Serial.begin(BAUD_RATE);
  WiFi.mode(WIFI_AP);
  WiFi.softAP(SSID);
  server.begin();
}

void loop() {
  WiFiClient client = server.available();
  if (client) {
    digitalWrite(CONNECTION_LED, HIGH);
    while (client.connected()) {
      while (client.available()) {
        String data = client.readStringUntil('\n');
        delay(10);
        Serial.print(data);
      }
    }
    digitalWrite(CONNECTION_LED, LOW);
    client.stop();
  }
}
