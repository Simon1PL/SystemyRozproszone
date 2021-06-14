using Shared;
using System;

namespace ServerIce
{
    class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                // https://doc.zeroc.com/ice/3.7/hello-world-application/writing-an-ice-application-with-c-sharp
                using Ice.Communicator communicator = Ice.Util.initialize(ref args);
                var adapter = communicator.createObjectAdapterWithEndpoints("CaseSolverAdapter", "default -h localhost -p 10000"); // listen for incoming requests using the default transport protocol (TCP/IP) at port number 10000
                adapter.add(new CaseSolverI(), Ice.Util.stringToIdentity("CaseSolver"));
                adapter.activate();
                communicator.waitForShutdown();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return 1;
            }
            return 0;
        }
    }
}
