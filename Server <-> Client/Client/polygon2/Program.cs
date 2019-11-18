using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace PipeApplication1
{
    class ProgramPipeTest
    {

        public void ThreadStartClient(object obj)
        {

            using (NamedPipeClientStream pipeStream = new NamedPipeClientStream("mytestpipe"))
            {
                pipeStream.Connect();

                Console.WriteLine("[Client] Pipe connection established");

                StreamReader sr = new StreamReader(pipeStream);
                StreamWriter sw = new StreamWriter(pipeStream)
                {
                    AutoFlush = true
                };

                Console.WriteLine("Waiting 4 messages from server...");

                while (true)
                {
                    string temp = sr.ReadLine();
                    Console.WriteLine(temp);
                    if (temp == "start")
                        break;
                }

                Console.WriteLine("Sending messages to server...");

                while (true)
                {
                    string temp = Console.ReadLine();
                    sw.WriteLine(temp);
                    if (temp == "end")
                        break;
                }

                Console.WriteLine("DONE");
            }
        }

        static void Main(string[] args)
        {
        

            ProgramPipeTest Client = new ProgramPipeTest();

            Thread ClientThread = new Thread(Client.ThreadStartClient);
            ClientThread.Start();
        }
    }
}