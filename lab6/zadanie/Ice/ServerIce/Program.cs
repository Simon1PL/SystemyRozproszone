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
                using Ice.Communicator communicator = Ice.Util.initialize(ref args);
                var adapter = communicator.createObjectAdapterWithEndpoints("CasesSolverAdapter", "default -h localhost -p 10000");
                adapter.add(new CasesService(), Ice.Util.stringToIdentity("CasesSolver"));
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
