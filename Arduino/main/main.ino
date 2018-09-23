#include <Wire.h>
#include <LiquidCrystal_PCF8574.h>
#include <math.h>

// Pin definitions for controlling two motors using L9110
// All PWM pins
#define LEFT_MOTOR_A 6
#define LEFT_MOTOR_B 9
#define RIGHT_MOTOR_A 3
#define RIGHT_MOTOR_B 5
#define VOLTAGE_READ A3

// Commands
#define MOVE_FORWARD "FD"
#define STOP "ST"
#define MOVE_BACKWARDS  "BD"
#define ROTATE_LEFT "RL"
#define ROTATE_RIGHT "RR"
#define STOP_ROTATING "SR"
#define MEASURE_VOLTAGE "MV"

// Serial configuration
#define BAUD_RATE 115200
#define TIMEOUT 10

// Lcd can be used for debugging purposes
#define LCD_ENABLED

#ifdef LCD_ENABLED
LiquidCrystal_PCF8574 lcd(0x38);
#endif
bool isMovingForward = false;
bool isMovingBackwards = false;
bool isRotatingLeft = false;
bool isRotatingRight = false;

void setup() {
  Serial.setTimeout(TIMEOUT);
  Serial.begin(BAUD_RATE);

#ifdef LCD_ENABLED
  Wire.begin();
  Wire.beginTransmission(0x38);

  lcd.begin(16, 2);
  lcd.setBacklight(255);
  lcd.home();
#endif
  pinMode(LEFT_MOTOR_A, OUTPUT);
  pinMode(LEFT_MOTOR_B, OUTPUT);
  pinMode(RIGHT_MOTOR_A, OUTPUT);
  pinMode(RIGHT_MOTOR_B, OUTPUT);
  pinMode(LED_BUILTIN, OUTPUT);
  pinMode(VOLTAGE_READ, INPUT);
}

void loop() {
  if (Serial.available()) {
    String data = Serial.readString();
    
    if (data.equals(MOVE_FORWARD)) {
      moveForward();
    }
    else if (data.equals(STOP)) {
      stop();
    }
    else if (data.equals(MOVE_BACKWARDS)) {
      moveBackwards();
    }
    else if (data.equals(ROTATE_LEFT)) {
      rotateLeft();
    }
    else if (data.equals(ROTATE_RIGHT)) {
      rotateRight();
    }
    else if (data.equals(STOP_ROTATING)) {
      stopRotating();
    }
    else if(data.equals(MEASURE_VOLTAGE)) {
      int voltage = measureVoltage();
      byte percentage = expressIn1023Percentage(voltage);
      sendMeasuredVoltagePercentage(percentage);
      #ifdef LCD_ENABLED
      lcd.print(percentage);
      #endif 
    }
  }
}

void rotateLeft() {
  if (!isRotatingRight) {
    digitalWrite(LEFT_MOTOR_A, LOW);
    digitalWrite(LEFT_MOTOR_B, LOW);
    isRotatingLeft = true;
  }

}

void rotateRight() {
  if (!isRotatingLeft) {
    digitalWrite(RIGHT_MOTOR_A, LOW);
    digitalWrite(RIGHT_MOTOR_B, LOW);
    isRotatingRight = true;
  }

}

void stopRotating() {
  if (isMovingForward) {
    if (isRotatingLeft) {
      digitalWrite(LEFT_MOTOR_A, 255);
      digitalWrite(LEFT_MOTOR_B, LOW);
    }
    else if (isRotatingRight) {
      digitalWrite(RIGHT_MOTOR_A, 255);
      digitalWrite(RIGHT_MOTOR_B, LOW);
    }
  }
  else if (isMovingBackwards) {
    if (isRotatingLeft) {
      digitalWrite(LEFT_MOTOR_A, LOW);
      digitalWrite(LEFT_MOTOR_B, 255);
    }
    else if (isRotatingRight) {
      digitalWrite(RIGHT_MOTOR_A, LOW);
      digitalWrite(RIGHT_MOTOR_B, 255);
    }
  }
  isRotatingLeft = false;
  isRotatingRight = false;
}

void moveForward() {
  stop();
  digitalWrite(LEFT_MOTOR_A, 255);
  digitalWrite(LEFT_MOTOR_B, LOW);
  digitalWrite(RIGHT_MOTOR_A, 255);
  digitalWrite(RIGHT_MOTOR_B, LOW);

  isMovingForward = true;
}

void moveBackwards() {
  stop();
  digitalWrite(LEFT_MOTOR_A, LOW);
  digitalWrite(LEFT_MOTOR_B, 255);
  digitalWrite(RIGHT_MOTOR_A, LOW);
  digitalWrite(RIGHT_MOTOR_B, 255);

  isMovingBackwards = true;
}

void stop() {
  delay(10);
  digitalWrite(LEFT_MOTOR_A, LOW);
  digitalWrite(LEFT_MOTOR_B, LOW);
  digitalWrite(RIGHT_MOTOR_A, LOW);
  digitalWrite(RIGHT_MOTOR_B, LOW);
  
  isMovingForward = false;
  isMovingBackwards = false;
}

int measureVoltage() {
  int measuredVoltage = analogRead(VOLTAGE_READ);
  
  return measuredVoltage;
}

byte expressIn1023Percentage(int value) {
  return round(value / 1023.0f * 100);
}

void sendMeasuredVoltagePercentage(byte percentage) {
    Serial.write(percentage);
}
