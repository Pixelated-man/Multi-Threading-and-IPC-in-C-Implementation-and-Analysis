using System;
using System.IO.Pipes;
using System.Text;
using System.Threading;

namespace PipeCommunication
{
    public class Producer
    {
        private string pipeName;

        public Producer(string pipeName)
        {
            this.pipeName = pipeName;
        }

        public void Start()
        {
            using (var pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.Out))
            {
                Console.WriteLine("Producer: Waiting for consumer to connect...");

                pipeServer.WaitForConnection();
                Console.WriteLine("Producer: Consumer connected.");

                string[] messages = { "Message 1", "Message 2", "Message 3", "STOP" };

                foreach (var message in messages)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(message + "\n"); // Ensure messages are newline-terminated
                    pipeServer.Write(buffer, 0, buffer.Length);
                    Console.WriteLine($"Producer: Sent -> {message}");

                    Thread.Sleep(1000); // Simulate work
                }

                Console.WriteLine("Producer: Finished sending messages.");
            }
        }
    }
}
