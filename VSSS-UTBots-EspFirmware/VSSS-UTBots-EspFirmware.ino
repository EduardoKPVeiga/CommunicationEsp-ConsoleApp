#include <ESP8266WiFi.h>        // Include the Wi-Fi library
#include <WiFiUdp.h>

//Constants
#ifndef STASSID
#define SSID "UtBot - rede linda"
#define PASSWORD "utbotlaser"
#endif
#define pwmMotorA 5
#define pwmMotorB 4
#define dirMotorA 0
#define dirMotorB 2
#define motorSpeed 100

unsigned int localPort = 3600;

// buffers for receiving and sending data
char packetBuffer[UDP_TX_PACKET_MAX_SIZE + 1]; //buffer to hold incoming packet,
char  ReplyBuffer[] = "acknowledged\r\n";       // a string to send back

WiFiUDP Udp;

void setup() {
  Serial.begin(115200);
  pinMode(pwmMotorA, OUTPUT);
  pinMode(pwmMotorB, OUTPUT);
  pinMode(dirMotorA, OUTPUT);
  pinMode(dirMotorB, OUTPUT);
  WiFi.mode(WIFI_STA);
  WiFi.begin(SSID, PASSWORD);
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print('.');
    delay(500);
  }
  Serial.print("Connected! IP address: ");
  Serial.println(WiFi.localIP());
  Serial.printf("UDP server on port %d\n", localPort);
  Udp.begin(localPort);
}

void loop() {
  int packetSize = Udp.parsePacket();
  if (packetSize) {
    /*
    Serial.printf("Received packet of size %d from %s:%d\n    (to %s:%d, free heap = %d B)\n",
                  packetSize,
                  Udp.remoteIP().toString().c_str(), Udp.remotePort(),
                  Udp.destinationIP().toString().c_str(), Udp.localPort(),
                  ESP.getFreeHeap());
    */

    // read the packet into packetBufffer
    int n = Udp.read(packetBuffer, UDP_TX_PACKET_MAX_SIZE);
    // movement
    if(*packetBuffer == 'w')
      forward();
    else if(*packetBuffer == 's')
      backward();
    else if(*packetBuffer == 'd')
      right();
    else if(*packetBuffer == 'a')
      left();
    else
      stop();
      
    //delay(5);
    packetBuffer[n] = 0;
  }
  //stop();
}

void forward(){
  digitalWrite(pwmMotorA, motorSpeed);
  digitalWrite(dirMotorA, LOW);
  digitalWrite(pwmMotorB, motorSpeed);
  digitalWrite(dirMotorB, HIGH);
  //delay(100); 
}
void backward(){
  digitalWrite(pwmMotorA, motorSpeed);
  digitalWrite(dirMotorA, HIGH);
  digitalWrite(pwmMotorB, motorSpeed);
  digitalWrite(dirMotorB, LOW);
  //delay(100);
}
void left(){
  digitalWrite(pwmMotorA, motorSpeed);
  digitalWrite(dirMotorA, LOW);
  digitalWrite(pwmMotorB, motorSpeed);
  digitalWrite(dirMotorB, LOW);
  //delay(100);
}
void right(){
  digitalWrite(pwmMotorA, motorSpeed);
  digitalWrite(dirMotorA, HIGH);
  digitalWrite(pwmMotorB, motorSpeed);
  digitalWrite(dirMotorB, HIGH);
  //delay(100);
}
void stop(){
  digitalWrite(pwmMotorA, LOW);
  digitalWrite(dirMotorA, LOW);
  digitalWrite(pwmMotorB, LOW);
  digitalWrite(dirMotorB, LOW);
}
