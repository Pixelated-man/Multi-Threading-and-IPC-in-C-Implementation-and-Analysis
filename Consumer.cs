using System;
using System.IO.Pipes;
using System.Text;

namespace PipeCommunication
{
    public class Consumer
    {
        private string pipeName;

        public Consumer(string pipeName)
        {
            this.pipeName = pipeName;
        }

        public void Start()
        {
            using (var pipeClient = new NamedPipeClientStream(pipeName))
            {
                Console.WriteLine("Consumer: Connecting to producer...");

                pipeClient.Connect();
                Console.WriteLine("Consumer: Connected to producer.");

                using (var reader = new StreamReader(pipeClient, Encoding.UTF8))
                {
                    string message;
                    while ((message = reader.ReadLine()) != null)
                    {
                        Console.WriteLine($"Consumer: Received -> {message}");

                        if (message == "STOP")
                        {
                            break; // Stop consuming when "STOP" message is received
                        }
                    }
                }

                Console.WriteLine("Consumer: Finished receiving messages.");
            }
        }
    }
}
