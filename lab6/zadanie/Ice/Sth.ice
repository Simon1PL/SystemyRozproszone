module Shared
{
    exception CaseError 
    {
        string reason;
    }

    struct Case1Request
    {   
        string name;
        bool hasVat;
    }

    struct Case2Request
    {   
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
        List subModelList;
    }

    enum Response { Fail, Git, Sth }

    struct CaseResponse
    {   
        Response result;
    }

    struct Case1Response
    {   
        string price;
        Response result;
    }

    interface CaseSolver
    {
        void printString(string s);
        Case1Response case1(Case1Request case) throws CaseError;
        CaseResponse case2(Case2Request case) throws CaseError;
        CaseResponse case3(Case3Request case) throws CaseError;
    }
}