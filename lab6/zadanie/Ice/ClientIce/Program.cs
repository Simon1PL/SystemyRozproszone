using Ice;
using Shared;
using System;
using System.Threading.Tasks;

namespace ClientIce
{
    class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                using Communicator communicator = Util.initialize(ref args);
                ObjectPrx obj = communicator.stringToProxy("CaseSolver:default -h localhost -p 10000"); // CaseSolver - proxy from server
                CaseSolverPrx caseSolver = CaseSolverPrxHelper.checkedCast(obj);
                if (caseSolver == null)
                {
                    throw new ApplicationException("Invalid proxy");
                }

                // "log in" client by id
                CasesService casesService = new CasesService();
                int clientId = 0;
                Console.WriteLine("Podaj id klienta");
                string stringId = Console.ReadLine();
                while (!int.TryParse(stringId, out clientId))
                {
                    Console.WriteLine("Id klienta musi byc liczba");
                    stringId = Console.ReadLine();
                }

                // creating bidirectional connection:
                var adapter = communicator.createObjectAdapter("");
                var cbPrx = ClientProxyPrxHelper.uncheckedCast(adapter.addWithUUID(new ClientProxyI()));
                caseSolver.ice_getCachedConnection().setAdapter(adapter);
                caseSolver.ice_getCachedConnection().setACM(30, ACMClose.CloseOff, ACMHeartbeat.HeartbeatAlways); // utrzymywanie połączenia dwukierunkowego przez klienta

                CaseResponse[] missedResponses = caseSolver.logIn(cbPrx, clientId);

                foreach (CaseResponse response in missedResponses)
                {
                    Console.Write("case end: " + response.@case + " " + response.result);
                    if (response.@case == Case.Case1)
                    {
                        Console.Write(" price: " + ((Case1Response)response).price);
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("Command list:\ncase1 {text}\ncase2 {number}\ncase3 {number}");
                while (true)
                {
                    string command = Console.ReadLine();
                    Task.Run(() =>
                    {
                        casesService.CommandParser(caseSolver, command, clientId);
                    });
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
