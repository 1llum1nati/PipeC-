using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace PipeApplication1
{
    class ProgramPipeTest
    {
        public void ThreadStartServer()
        {
            using (NamedPipeServerStream pipeStream = new NamedPipeServerStream("mytestpipe"))
            {
                Console.WriteLine("[Server] Pipe created {0}", pipeStream.GetHashCode());

                pipeStream.WaitForConnection();
                Console.WriteLine("[Server] Pipe connection established");

                StreamReader sr = new StreamReader(pipeStream);
                StreamWriter sw = new StreamWriter(pipeStream)
                {
                    AutoFlush = true
                };

                Console.WriteLine("Sending messages to client...");

                while (true)
                {
                    string temp = Console.ReadLine();
                    sw.WriteLine(temp);
                    if (temp == "start")
                        break;
                }

                Console.WriteLine("Waiting 4 messages from client...");

                while (true)
                {
                    string temp = sr.ReadLine();
                    Console.WriteLine(temp);
                    if (temp == "end")
                        break;
                }

                Console.WriteLine("DONE");
            }

            //Console.WriteLine("Connection lost");
        }

        static void Main(string[] args)
        {


            ProgramPipeTest Server = new ProgramPipeTest();

            Thread ServerThread = new Thread(Server.ThreadStartServer);

            ServerThread.Start();
            /*Thread thread1 = new Thread(() => First());
            Thread thread2 = new Thread(() => Second());
            Thread thread3 = new Thread(() => Third());*/
            //thread1.Start();

            //thread2.Start();



        }
    }
}