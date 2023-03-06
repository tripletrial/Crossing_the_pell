#include <Arduino.h>
#include <U8x8lib.h>
#include <SPI.h>
#include <Wire.h>
#include <Uduino.h>

#define LED_INTERVAL 200
#define UNITY_INTERVAL 20 //matching 50fps

U8X8_SSD1306_128X64_NONAME_HW_I2C u8x8(/* reset=*/U8X8_PIN_NONE);
unsigned long rpmtime;
float rpmfloat;
unsigned int rpm;
bool tooslow = 1;
bool unityIsConnected = 0;
Uduino uduino("speedSensor");

unsigned long lastLedRefreshMillis = 0;
unsigned long lastUnityRefreshMillis = 0;

void setup() {
  Serial.begin(9600);
  u8x8.begin();
  u8x8.setFont(u8x8_font_profont29_2x3_f);
  TCCR1A = 0;
  TCCR1B = 0;
  TCCR1B |= (1 << CS12);   //Prescaler 256
  TIMSK1 |= (1 << TOIE1);  //enable timer overflow
  pinMode(2, INPUT);
  attachInterrupt(0, RPM, FALLING);
}

ISR(TIMER1_OVF_vect) {
  tooslow = 1;
}

void loop() {
  unsigned long currentMillis = millis();

  //******data update******
  uduino.update();
  if (currentMillis - lastUnityRefreshMillis >= UNITY_INTERVAL ) {
    lastUnityRefreshMillis = currentMillis;
    if(!tooslow) {
      rpmfloat = 120 / (rpmtime / 31250.00);
      rpm = round(rpmfloat);
    }

      //******report data to unity******
    if (uduino.isConnected()) {
      unityIsConnected = 1;
      if(tooslow) {
        uduino.println("0");
      }
      else {
        uduino.println(rpm);
      }
  }
  }

  else {
    unityIsConnected = 0;
  }

  //******print data to led******
  if (currentMillis - lastLedRefreshMillis >= LED_INTERVAL ) {
    lastLedRefreshMillis = currentMillis;
      if (tooslow == 1) {
        u8x8.clear();
        u8x8.drawString(1, 0, "SLOW!");
        printUnityConnections(unityIsConnected);
      }   
    else {
      u8x8.clear();
      u8x8.setCursor(1, 0);
      u8x8.print(rpm);
      printUnityConnections(unityIsConnected);
    }
  }

}

void RPM() {
  rpmtime = TCNT1;
  TCNT1 = 0;
  tooslow = 0;
}

void printUnityConnections(bool isConnected) {
  u8x8.setCursor(1, 3);
  if (isConnected) {
    u8x8.print("Uduino:)");
  } else {
    u8x8.print("No Con");
  }
}