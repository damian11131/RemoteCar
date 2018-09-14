#include <Wire.h>
#include <LiquidCrystal_PCF8574.h>

// Pin definitions for controlling two motors using L9110
// All PWM pins
#define LEFT_MOTOR_A 6
#define LEFT_MOTOR_B 9
#define RIGHT_MOTOR_A 3
#define RIGHT_MOTOR_B 5

// Commands
#define MOVE_FORWARD "FD"
#define STOP "ST"
#define MOVE_BACKWARDS  "BD"
#define ROTATE_LEFT "RL"
#define ROTATE_RIGHT "RR"
#define STOP_ROTATING "SR"

// Serial configuration
#define BAUD_RATE 115200
#define TIMEOUT 10

#define FULL_SPEED 255
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
}

void loop() {
  if (Serial.available()) {
    String data = Serial.readString();
#ifdef LCD_ENABLED
    lcd.clear();
    lcd.print(data);
#endif
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
  }
}

void rotateLeft() {
  if (!isRotatingRight) {
    // Disabling left motor will cause robot rotating left
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
      digitalWrite(LEFT_MOTOR_A, FULL_SPEED);
      digitalWrite(LEFT_MOTOR_B, LOW);
    }
    else if (isRotatingRight) {
      digitalWrite(RIGHT_MOTOR_A, FULL_SPEED);
      digitalWrite(RIGHT_MOTOR_B, LOW);
    }
  }
  else if (isMovingBackwards) {
    if (isRotatingLeft) {
      digitalWrite(LEFT_MOTOR_A, LOW);
      digitalWrite(LEFT_MOTOR_B, FULL_SPEED);
    }
    else if (isRotatingRight) {
      digitalWrite(RIGHT_MOTOR_A, LOW);
      digitalWrite(RIGHT_MOTOR_B, FULL_SPEED);
    }
  }
  isRotatingLeft = false;
  isRotatingRight = false;
}

void moveForward() {
  stop();
  digitalWrite(LEFT_MOTOR_A, FULL_SPEED);
  digitalWrite(LEFT_MOTOR_B, LOW);
  digitalWrite(RIGHT_MOTOR_A, FULL_SPEED);
  digitalWrite(RIGHT_MOTOR_B, LOW);

  isMovingForward = true;
}

void moveBackwards() {
  stop();
  digitalWrite(LEFT_MOTOR_A, LOW);
  digitalWrite(LEFT_MOTOR_B, FULL_SPEED);
  digitalWrite(RIGHT_MOTOR_A, LOW);
  digitalWrite(RIGHT_MOTOR_B, FULL_SPEED);

  isMovingBackwards = true;
}

void stop() {
  digitalWrite(LEFT_MOTOR_A, LOW);
  digitalWrite(LEFT_MOTOR_B, LOW);
  digitalWrite(RIGHT_MOTOR_A, LOW);
  digitalWrite(RIGHT_MOTOR_B, LOW);
  
  isMovingForward = false;
  isMovingBackwards = false;
}
