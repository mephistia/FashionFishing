
#include <Wire.h>
 
//Endereco I2C do MPU6050
const int MPU=0x68;  
 
// valores dos eixos
int AcX,AcY,AcZ;

// variável para armazenar valor anterior
int AcX2, AcY2, AcZ2;


 
void setup()
{
  // inicializar o acelerômetro
  Serial.begin(9600);
  Wire.begin();
  Wire.beginTransmission(MPU);
  Wire.write(0x6B); 
  Wire.write(0); 
  Wire.endTransmission(true);

  AcX2 = 0;
  AcY2 = 0;
  AcZ2 = 0;
    
}
 
void loop()
{
  
  Wire.beginTransmission(MPU);
  Wire.write(0x3B);  
  Wire.endTransmission(false);
  
  // faz requisição dos dados
  Wire.requestFrom(MPU,14,true);  
  
  // armazena os valores "crus" atuais
  AcX=Wire.read()<<8|Wire.read(); 
  AcY=Wire.read()<<8|Wire.read(); 
  AcZ=Wire.read()<<8|Wire.read(); 

  // offset (deixar mais próximo de 0 em repouso)
  AcX = AcX / -963;
  AcY = AcY / -2050;
  AcZ = AcZ / 4771;
  
  // se a diferença do valor antigo for maior que 10, lançou muito perto
  if ((AcX - AcX2) > 10 || (AcY - AcY2) > 10 || (AcZ - AcZ2) > 10){
    Serial.write(1); // envia o valor 1 para o serial (será lido na Unity)
    Serial.flush();
  }
  // (lançamento perfeito)
  else if ((AcX - AcX2) > 15 || (AcY - AcY2) > 15 || (AcZ - AcZ2) > 15){
    Serial.write(2);
    Serial.flush();
  }

  // (muito longe)
  else if ((AcX - AcX2) > 20 || (AcY - AcY2) > 20 || (AcZ - AcZ2) > 20){
    Serial.write(3);
    Serial.flush();
  }
  else {
    Serial.write(-1);
    Serial.flush();
  }
  //Atribui o valor de agora como valor antigo
  AcX2 = AcX;
  AcY2 = AcY;
  AcZ2 = AcZ;

 
  //Aguarda
  delay(100);
}
