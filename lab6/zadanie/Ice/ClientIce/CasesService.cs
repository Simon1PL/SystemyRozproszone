using Newtonsoft.Json;
using Shared;
using System;
using System.Threading.Tasks;

namespace ClientIce
{
    class CasesService
    {
        public async Task CommandParser(CaseSolverPrx CaseSolver, string command, int clientId)
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
                        Case1(CaseSolver, command.Split(" ")[1]);
                        break;
                    case "case2":
                        if (!int.TryParse(command.Split(" ")[1], out number))
                        {
                            Console.WriteLine("Bad command");
                            break;
                        }
                        Case2(CaseSolver, number);
                        break;
                    case "case3":
                        if (!int.TryParse(command.Split(" ")[1], out number))
                        {
                            Console.WriteLine("Bad command");
                            break;
                        }
                        Case3(CaseSolver, number);
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

        private void Case1(CaseSolverPrx CaseSolver, string text)
        {
            Case1Response case1Response = CaseSolver.case1(new Case1Request
            {
                name = text,
                hasVat = true
            });
            Console.WriteLine("case1 result: " + JsonConvert.SerializeObject(case1Response));
        }

        private void Case2(CaseSolverPrx CaseSolver, int number)
        {
            CaseResponse caseResponse = CaseSolver.case2(new Case2Request
            {
                number = number
            });
            Console.WriteLine("case2 result: " + JsonConvert.SerializeObject(caseResponse));
        }

        private void Case3(CaseSolverPrx CaseSolver, int number)
        {
            CaseResponse caseResponse = CaseSolver.case3(new Case3Request
            {
                subModelList = new Case3SubModel[number]
            });
            Console.WriteLine("case3 result: " + JsonConvert.SerializeObject(caseResponse));
        }
    }
}
