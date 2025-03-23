using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colas
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=====================================");
                Console.WriteLine("WELCOME");
                Console.WriteLine("=====================================");
                Console.WriteLine("1. Gestor de Colas");
                Console.WriteLine("0. Salir");
                Console.Write("\nSelecciona una opción: ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        RunQueueManager();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void RunQueueManager()
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine("SELECCIONAR PROVEEDOR DE COLA:");
            Console.WriteLine("=====================================");
            Console.WriteLine("1. RabbitMQ");
            Console.WriteLine("2. Amazon SQS");
            Console.WriteLine("3. Azure Queue Storage");
            Console.Write("\nSeleccione una opción: ");

            IQueueService queueService = null;
            bool validSelection = false;

            while (!validSelection)
            {
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        queueService = new RabbitMQService();
                        validSelection = true;
                        break;
                    case "2":
                        queueService = new AmazonSQSService();
                        validSelection = true;
                        break;
                    case "3":
                        queueService = new AzureQueueService();
                        validSelection = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }

            QueueManager queueManager = new QueueManager(queueService);

            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=====================================");
                Console.WriteLine("OPERACION DE COLA");
                Console.WriteLine("=====================================");
                Console.WriteLine($"Proveedor actual: {queueService.GetType().Name}");
                Console.WriteLine($"Elementos en cola: {queueManager.GetQueueCount()}");
                Console.WriteLine("\n1. Agregar mensaje a la cola");
                Console.WriteLine("2. Obtener mensaje de la cola");
                Console.WriteLine("3. Verificar si la cola está vacía");
                Console.WriteLine("4. Mostrar contenido de la cola");
                Console.WriteLine("0. Volver al menú principal");
                Console.Write("\nSeleccione una opción: ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Write("\nIngrese el mensaje: ");
                        string? message = Console.ReadLine();
                        queueManager.SendMessage(message);
                        break;
                    case "2":
                        string receivedMessage = queueManager.ReceiveMessage();
                        Console.WriteLine($"\nMensaje recibido: {receivedMessage}");
                        break;
                    case "3":
                        bool isEmpty = queueManager.IsQueueEmpty();
                        Console.WriteLine($"\nLa cola está vacía: {isEmpty}");
                        break;
                    case "4":
                        queueManager.PrintQueueContents();
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                if (!back)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar");
                    Console.ReadKey();
                }
            }
        }
    }
}
