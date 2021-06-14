using Ice;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientIce
{
    class ClientProxyI : ClientProxyDisp_ // Implements interface "ClientProxy" from ice
    {
        // Message when case is done
        public override void caseResponseMessage(CaseResponse response, Current current = null)
        {
            Console.Write("case end: " + response.@case + " " + response.result);
            if (response.@case == Case.Case1)
            {
                Console.Write(" price: " + ((Case1Response)response).price);
            }
            Console.WriteLine();
        }
    }
}
