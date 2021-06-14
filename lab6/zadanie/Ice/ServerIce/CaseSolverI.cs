using Ice;
using Shared;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerIce
{
    public class CaseSolverI : CaseSolverDisp_
    {
        private readonly ConcurrentDictionary<int, List<CaseResponse>> clientsMissedCases = new ConcurrentDictionary<int, List<CaseResponse>>();
        private readonly ConcurrentDictionary<int, List<ClientProxyPrx>> clientsProxy = new ConcurrentDictionary<int, List<ClientProxyPrx>>();

        public override Task<CaseResponse[]> logInAsync(ClientProxyPrx clientProxy, int clientId, Current current)
        {
            var proxy = (ClientProxyPrx)clientProxy.ice_fixed(current.con);
            clientsProxy[clientId] = clientsProxy.AddOrUpdate(clientId, new List<ClientProxyPrx> { proxy }, (key, oldValue) =>
            {
                oldValue.Add(proxy);
                return oldValue;
            }); ; // Returns a new fixed proxy bound to the given(current) connection
            Console.WriteLine("Login: " + clientId);
            current.con.setCloseCallback(con =>
            {
                Console.WriteLine("Logout: " + clientId);
                clientsProxy[clientId].Remove(proxy);
            });
            return Task.Run(() =>
            {
                if (clientsMissedCases.TryRemove(clientId, out List<CaseResponse> missedResponsses))
                {
                    return missedResponsses.ToArray();
                }
                return new List<CaseResponse>().ToArray();
            });
        }

        public override int case1(Case1Request case1, Current current)
        {
            if (case1.name.ToLower().Contains("jebac pis")) throw new CaseError
            {
                reason = "incorrectly completed form"
            };
            int seconds = new Random().Next(1, 10);
            Task.Run(() =>
            {
                Console.WriteLine("case1 start");
                Thread.Sleep(seconds * 1000);
                CaseResponse response = new Case1Response
                {
                    @case = Case.Case1,
                    price = 100.12M.ToString(),
                    result = Response.Git
                };
                Console.WriteLine("case1 end");
                if (clientsProxy.TryGetValue(case1.clientId, out List<ClientProxyPrx> clientProxies) && clientProxies.Count != 0)
                {
                    Console.WriteLine(clientProxies.Count);
                    try
                    {
                        foreach (ClientProxyPrx clientProxy in clientProxies)
                        {
                            clientProxy.caseResponseMessage(response);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Sth is not ok with client proxies. Program should never be here.");
                        clientsMissedCases.AddOrUpdate(case1.clientId, new List<CaseResponse> { response }, (key, oldValue) =>
                        {
                            oldValue.Add(response);
                            return oldValue;
                        });
                    }
                }
                else
                {
                    clientsMissedCases.AddOrUpdate(case1.clientId, new List<CaseResponse> { response }, (key, oldValue) =>
                    {
                        oldValue.Add(response);
                        return oldValue;
                    });
                }
            });
            return seconds;
        }

        public override int case2(Case2Request case2, Current current)
        {
            if (case2.number > 10) throw new CaseError
            {
                reason = "incorrectly completed form"
            };
            int seconds = new Random().Next(1, 10);
            Task.Run(() =>
            {
                Console.WriteLine("case2 start");
                Thread.Sleep(seconds * 1000);
                CaseResponse response = new CaseResponse
                {
                    @case = Case.Case2,
                    result = Response.Git
                };
                Console.WriteLine("case2 end");
                if (clientsProxy.TryGetValue(case2.clientId, out List<ClientProxyPrx> clientProxies) && clientProxies.Count != 0)
                {
                    try
                    {
                        foreach (ClientProxyPrx clientProxy in clientProxies)
                        {
                            clientProxy.caseResponseMessage(response);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Sth is not ok with client proxies. Program should never be here.");
                        clientsMissedCases.AddOrUpdate(case2.clientId, new List<CaseResponse> { response }, (key, oldValue) =>
                        {
                            oldValue.Add(response);
                            return oldValue;
                        });
                    }
                }
                else
                {
                    clientsMissedCases.AddOrUpdate(case2.clientId, new List<CaseResponse> { response }, (key, oldValue) =>
                    {
                        oldValue.Add(response);
                        return oldValue;
                    });
                }
            });
            return seconds;
        }

        public override int case3(Case3Request case3, Current current)
        {
            if (case3.subModelList.Length < 1) throw new CaseError
            {
                reason = "incorrectly completed form"
            };
            int seconds = new Random().Next(1, 10);
            Task.Run(() =>
            {
                Console.WriteLine("case3 start");
                Thread.Sleep(seconds * 1000);
                CaseResponse response = new CaseResponse
                {
                    @case = Case.Case3,
                    result = Response.Git,
                };
                Console.WriteLine("case3 end");
                if (clientsProxy.TryGetValue(case3.clientId, out List<ClientProxyPrx> clientProxies) && clientProxies.Count != 0)
                {
                    try
                    {
                        foreach (ClientProxyPrx clientProxy in clientProxies)
                        {
                            clientProxy.caseResponseMessage(response);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Sth is not ok with client proxies. Program should never be here.");
                        clientsMissedCases.AddOrUpdate(case3.clientId, new List<CaseResponse> { response }, (key, oldValue) =>
                        {
                            oldValue.Add(response);
                            return oldValue;
                        });
                    }
                }
                else
                {
                    clientsMissedCases.AddOrUpdate(case3.clientId, new List<CaseResponse> { response }, (key, oldValue) =>
                    {
                        oldValue.Add(response);
                        return oldValue;
                    });
                }
            });
            return seconds;
        }
    }
}
