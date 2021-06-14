module Shared
{
    exception CaseError 
    {
        string reason;
    }

    struct Case1Request
    {   
        int clientId;
        string name;
        bool hasVat;
    }

    struct Case2Request
    {   
        int clientId;
        int number;
    }

    struct Case3SubModel
    {   
        int amount;
        string name;
    }

    sequence<Case3SubModel> List;

    struct Case3Request
    {   
        int clientId;
        List subModelList;
    }

    enum Response { Fail, Git, Sth }
    enum Case { Case1, Case2, Case3 }

    class CaseResponse
    {   
        Case case;
        Response result;
    }

    class Case1Response extends CaseResponse
    {   
        string price;
    }

    sequence<CaseResponse> PrevResponses;

    interface ClientProxy
    {
        void caseResponseMessage(CaseResponse caseResponse);
    }

    interface CaseSolver
    {
        ["amd"] PrevResponses logIn(ClientProxy* client, int clientId);
        int case1(Case1Request case) throws CaseError;
        int case2(Case2Request case) throws CaseError;
        int case3(Case3Request case) throws CaseError;
    }
}