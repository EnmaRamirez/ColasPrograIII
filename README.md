Este es un Programa que implementa un sistema de colas en C# Basado en Programacion Orientado a Objetos (POO), utilizando el principio de inversion de dependencias los cuales se pueden cambiar de manera sencilla los proveedores de colas mediante la interfaz llamada IQueueService.

Interfaz IQueueService: define las operaciones basicas de la cola.
RabbitMQService: es proveedor de cola.
QueueManajer: Logica de enclonado y desenclonado.

Requisitos a utilizar antes de crear este programa;
1) NET SDK en la version más reciente.
2) Visual studio en su actualizacion más reciente

Uso de ejecución.
Compilar y ejecutar
Aparecera un interfaz con 3 proveedores de cola, RabbitMQ, Amazon SQS, Azure
Seleccionamos el proveedor de cola
Enviar mensaje

Cambiar entre proveedores.
El archivo Program.cs solo se selecciona que proveedor de cola utilizar al crear el objeto queueService


