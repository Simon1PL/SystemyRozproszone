using Newtonsoft.Json;
using Shared;
using System;

namespace ClientIce
{
    class CasesService
    {
        public void CommandParser(CaseSolverPrx CaseSolver, string command, int clientId)
        {
            int number;
            if (command.Split(" ").Length < 2)
            {
                Console.WriteLine("Bad command");
                return;
            }
            try
            {
                switch (command.Split(" ")[0])
                {
                    case "case1":
                        Case1(CaseSolver, command.Split(" ", 2)[1], clientId);
                        break;
                    case "case2":
                        if (!int.TryParse(command.Split(" ")[1], out number))
                        {
                            Console.WriteLine("Bad command");
                            break;
                        }
                        Case2(CaseSolver, number, clientId);
                        break;
                    case "case3":
                        if (!int.TryParse(command.Split(" ")[1], out number) || number < 0)
                        {
                            Console.WriteLine("Bad command");
                            break;
                        }
                        Case3(CaseSolver, number, clientId);
                        break;
                    default:
                        Console.WriteLine("Bad command");
                        break;
                }
            }
            catch (Ice.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Case1(CaseSolverPrx CaseSolver, string text, int clientId)
        {
            try
            {
                int time = CaseSolver.case1(new Case1Request
                {
                    clientId = clientId,
                    name = text,
                    hasVat = true
                });
                Console.WriteLine("case1 sent, elapsed time: " + time);
            }
            catch (CaseError ex)
            {
                Console.WriteLine("case1 error: " + ex.reason);
            }
        }

        private void Case2(CaseSolverPrx CaseSolver, int number, int clientId)
        {
            try
            {
                int time = CaseSolver.case2(new Case2Request
                {
                    clientId = clientId,
                    number = number
                });
                Console.WriteLine("case2 sent, elapsed time: " + time);
            }
            catch (CaseError ex)
            {
                Console.WriteLine("case2 error: " + ex.reason);
            }
        }

        private void Case3(CaseSolverPrx CaseSolver, int number, int clientId)
        {
            try
            {
                int time = CaseSolver.case3(new Case3Request
                {
                    clientId = clientId,
                    subModelList = new Case3SubModel[number]
                });
                Console.WriteLine("case3 sent, elapsed time: " + time);
            }
            catch (CaseError ex)
            {
                Console.WriteLine("case3 error: " + ex.reason);
            }
        }
    }
}
