using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ServerIce
{
    public class CasesService : CaseSolverDisp_
    {
        public override void printString(string s, Ice.Current current)
        {
            Console.WriteLine(s);
        }

        public override Case1Response case1(Case1Request case1, Ice.Current current) 
        {
            if (case1.name.ToLower().Contains("jebac pis")) throw new CaseError
            {
                reason = "incorrectly completed form"
            };
            int seconds = new Random().Next(1, 10);
            Thread.Sleep(seconds * 1000);
            decimal price = 100.12M;
            return new Case1Response
            {
                result = Response.Git,
                price = price.ToString()
            };
        }

    public override CaseResponse case2(Case2Request case2, Ice.Current current)
    {
        if (case2.number > 10) throw new CaseError
        {
            reason = "incorrectly completed form"
        };
        int seconds = new Random().Next(1, 10);
        Thread.Sleep(seconds * 1000);
        return new CaseResponse
        {
            result = Response.Git
        };
    }

    public override CaseResponse case3(Case3Request case3, Ice.Current current)
        {
            if (case3.subModelList.Length < 1) throw new CaseError
            {
                reason = "incorrectly completed form"
            };
            int seconds = new Random().Next(1, 10);
            Thread.Sleep(seconds * 1000);
            return new CaseResponse
            {
                result = Response.Git
            };
        }
    }
}
