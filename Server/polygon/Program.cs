using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace PipeApplication1
{
    class ProgramPipeTest
    {
        public static void ThreadStartServer()
        {
            using (NamedPipeServerStream pipeStream = new NamedPipeServerStream("mytestpipe"))
            {
                Console.WriteLine("[Server] Pipe created {0}", pipeStream.GetHashCode());
                pipeStream.WaitForConnection();
                Console.WriteLine("[Server] Pipe connection established");
                FileStream file = new FileStream("/home/r3pl1c4nt/Projects/1.txt", FileMode.Append);
                
                using (StreamReader sr = new StreamReader(pipeStream))
                {
                    StreamWriter sw = new StreamWriter(file);
                    sw.WriteLine(sr.ReadLine());
                    sw.Dispose();
                }
            }
        }
        
        static void Main(string[] args)
        {
            ProgramPipeTest Server = new ProgramPipeTest();

            Thread ServerThread = new Thread(Server.ThreadStartServer);

            ServerThread.Start();

        }
    }
}
