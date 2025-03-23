using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colas
{
    public class RabbitMQService : IQueueService
    {
        private List<string> _messages = new List<string>();
        private int _front = 0;

        public int Count()
        {

            int count = _messages.Count - _front;
            Console.WriteLine($"RabbitMQ: Contando elementos - Lista.Count: {_messages.Count}, Índice frontal: {_front}, Elementos actuales: {count}");
            return count;
        }

        public string Dequeue()
        {
            Console.WriteLine("\n=== INICIO OPERACIÓN DEQUEUE ===");
            Console.WriteLine($"RabbitMQ: Estado inicial - Lista.Count: {_messages.Count}, Índice frontal: {_front}");


            if (_front >= _messages.Count)
            {
                Console.WriteLine("RabbitMQ: No hay mensajes en la cola (_front >= _messages.Count)");
                Console.WriteLine("=== FIN OPERACIÓN DEQUEUE ===\n");
                return "La cola de RabbitMQ está vacía";
            }
            string message = _messages[_front];
            Console.WriteLine($"RabbitMQ: Mensaje encontrado en la posición {_front}: \"{message}\"");

            _front++;
            Console.WriteLine($"RabbitMQ: Índice frontal incrementado a {_front}");

            if (_front > 100 && _messages.Count > 200)
            {
                Console.WriteLine($"RabbitMQ: Optimización activada (_front > 100 && _messages.Count > 200)");
                Console.WriteLine($"RabbitMQ: Eliminando los primeros {_front} elementos");
                _messages.RemoveRange(0, _front);
                _front = 0;
                Console.WriteLine($"RabbitMQ: Lista reconstruida - Nuevo tamaño: {_messages.Count}, Nuevo índice frontal: {_front}");
            }

            DisplayQueueState();
            Console.WriteLine($"RabbitMQ: Mensaje desencolado: \"{message}\"");
            Console.WriteLine("=== FIN OPERACIÓN DEQUEUE ===\n");
            return message;
        }

        public void Enqueue(string message)
        {
            Console.WriteLine("\n=== INICIO OPERACIÓN ENQUEUE ===");
            Console.WriteLine($"RabbitMQ: Estado inicial - Lista.Count: {_messages.Count}, Índice frontal: {_front}");

            _messages.Add(message);
            Console.WriteLine($"RabbitMQ: Mensaje agregado al final de la lista: \"{message}\"");
            Console.WriteLine($"RabbitMQ: Nuevo tamaño de la lista: {_messages.Count}");

            DisplayQueueState();
            Console.WriteLine($"RabbitMQ: Total de mensajes en cola: {Count()}");
            Console.WriteLine("=== FIN OPERACIÓN ENQUEUE ===\n");
        }

        public bool IsEmpty()
        {
            bool isEmpty = _front >= _messages.Count;
            Console.WriteLine($"RabbitMQ: Verificando si está vacía - Lista.Count: {_messages.Count}, Índice frontal: {_front}, ¿Está vacía?: {isEmpty}");
            return isEmpty;
        }

        private void DisplayQueueState()
        {
            Console.WriteLine("RabbitMQ: Estado actual de la cola:");

            StringBuilder queueVisual = new StringBuilder();
            queueVisual.Append("FRONT [");

            int displayCount = Math.Min(10, Count());

            for (int i = 0; i < displayCount; i++)
            {
                if (i > 0) queueVisual.Append(" | ");
                queueVisual.Append(_messages[_front + i]);
            }

            if (Count() > 10)
                queueVisual.Append(" | ...");

            queueVisual.Append("] BACK");
            Console.WriteLine(queueVisual.ToString());
            Console.WriteLine($"Elementos totales: {Count()}, Elementos ocultos: {_messages.Count}");
        }

        public void PrintQueue()
        {
            Console.WriteLine("\n=== CONTENIDO DE COLA RABBITMQ ===");
            if (_front >= _messages.Count)
            {
                Console.WriteLine("RabbitMQ: La cola está vacía");
            }
            else
            {
                DisplayQueueState();
            }
            Console.WriteLine("=== FIN CONTENIDO COLA RABBITMQ ===\n");
        }

    }
}
