using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace PipeApplication1
{
    class ProgramPipeTest
    {
        public static void ThreadStartClient(object obj)
        {
            using (NamedPipeClientStream pipeStream = new NamedPipeClientStream("mytestpipe"))
            {
                pipeStream.Connect();
                FileStream file = new FileStream("/home/r3pl1c4nt/Projects/2.txt", FileMode.Open);
                Console.WriteLine("[Client] Pipe connection established");
                using (StreamWriter sw = new StreamWriter(pipeStream))
                {
                    sw.AutoFlush = true;
                    StreamReader sr = new StreamReader(file);
                    sw.WriteLine(sr.ReadLine());
                    sr.Dispose();
                }
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
