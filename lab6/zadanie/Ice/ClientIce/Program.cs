using Ice;
using Shared;
using System;

namespace ClientIce
{
    class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                using Communicator communicator = Util.initialize(ref args);
                ObjectPrx obj = communicator.stringToProxy("CasesSolver:default -h localhost -p 10000");
                CaseSolverPrx CaseSolver = CaseSolverPrxHelper.checkedCast(obj);
                if (CaseSolver == null)
                {
                    throw new ApplicationException("Invalid proxy");
                }
                CasesService casesService = new CasesService();
                int clientId = 0;
                Console.WriteLine("Podaj id klienta"); 
                string stringId = Console.ReadLine();
                while (!int.TryParse(stringId, out clientId))
                {
                    Console.WriteLine("Id klienta musi byc liczba");
                    stringId = Console.ReadLine();
                }
                CaseSolver.printString("Hello World!");
                Console.WriteLine("Command list:\ncase1 {number}\ncase2 {text}\ncase3 {number}");
                while (true)
                {
                    string command = Console.ReadLine();
                    casesService.CommandParser(CaseSolver, command, clientId);
                }
            }
            catch (System.Exception e)
            {
                Console.Error.WriteLine(e);
                return 1;
            }
        }
    }
}
