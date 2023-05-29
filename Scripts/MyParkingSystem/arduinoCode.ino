#include <ESP8266WiFi.h>
#include <Servo.h>
//#include<Uduino.h>
// Uduino uduino("myNodemcu");
const char WiFiPassword[] = "THAPATHALI";
const char AP_NameChar[] = "IOTTHAPATHALI" ;
#define trig 12
#define echo 14
Servo myservo;
WiFiServer server(80);

String header = "HTTP/1.1 200 OK\r\nContent-Type: text/html\r\n\r\n";
String html_1 = "<!DOCTYPE html><html><head><title>LED 
Control</title></head><body><div id='main'><h2>LED Control</h2>";
String html_2 = "<form id='F1' action='servoon'><input class='button' type='submit' 
value='servoon' ></form><br>";
String html_3 = "<form id='F2' action='servooff'><input class='button' type='submit' 
value='servooff' ></form><br>";
String html_4 = "</div></body></html>";

String request = "";
void setup() 
{
 pinMode(trig, OUTPUT); // for output
 pinMode(echo, INPUT); // 
 
 boolean conn = WiFi.softAP(AP_NameChar, WiFiPassword);
 server.begin();
 myservo.attach(2); // servo connected to pin no.2
 Serial.begin(9600);
 uduinoSetup();
}
void uduinoSetup(){
 // uduino.addCommand("myCommand", doSomething);	
}
 
void doSomething() {
  int numberOfParameters = uduino.getNumberOfParameters();
  
  if (numberOfParameters == 0)
    return;

  char * firstParameter = uduino.getParameter(0);
  int parameterAsInt = uduino.charToInt(firstParameter);

  char * secondParameter = uduino.getParameter(1);

  // ... do something with the values, or get other parameters
}

void loop() 
{
/* UNITY CODE 
  uduino.update();
  delay(10); // Delay of your choice or to match Unity's Read Timout

  /*
  if (uduino.isConnected()) {

    //put most of ur unity code here to connect wid unity properly

    // Important: If you uduino.print values outside this loop,
    // the board will not be correclty detected on Unity !
  }*/
int Duration, distance;
 digitalWrite( trig,LOW); // stays low for 2 sec
delay(2);
//uduino.delay(2);  // use this insted for maintaining Unity connection
digitalWrite( trig,HIGH); // stays high for 10 sec
delay(10);
digitalWrite( trig,LOW);
delay(2);
 Duration = pulseIn(echo, HIGH); // prints duration in pin 14 
 distance= Duration*0.034/2;
 if ( distance < 10 && distance > 5 )  
 {
 Serial.print(" some one on gate \t");
 delay(1000);
 Serial.print(distance);
 //while Serial.print works to send data to Unity 
 // Use uduino.print for better compatibility
 // uduino.printValue("distance", distance);
 }
 // Check if a client has connected
 WiFiClient client = server.available();
 if (!client) { 
 return; 
 }
 // Read the first line of the request
 request = client.readStringUntil('\r');
 if ( request.indexOf("servoon") > 0 ) 
 { 
 myservo.write(0);
 // uduino.printValue("gate", 0); //0 for close ,1 for open
 }
 else if ( request.indexOf("servooff") > 0 ) 
 {
 myservo.write(180); // turn 180 deg
 // uduino.printValue("gate", 1);
 }
 
 client.flush();
 client.print( header );
 client.print( html_1 );
 client.print( html_2 );
 client.print( html_3 );
 client.print( html_4);
 delay(5);
 // The client will actually be disconnected when the function returns and 'client' 
object is detroyed
}


void printValue(string dataHeader, int value) {
  uduino.println(dataHeader+ ":"+value );
}