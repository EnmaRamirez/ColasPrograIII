using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colas
{
    public class QueueManager
    {
        private readonly IQueueService _queueService;

        public QueueManager(IQueueService queueService)
        {
            _queueService = queueService;
        }

        public void SendMessage(string message)
        {
            _queueService.Enqueue(message);
        }

        public string ReceiveMessage()
        {
            return _queueService.Dequeue();
        }

        public bool IsQueueEmpty()
        {
            return _queueService.IsEmpty();
        }

        public int GetQueueCount()
        {
            return _queueService.Count();
        }

        public void PrintQueueContents()
        {
            _queueService.PrintQueue();
        }
    }
}
