
#ifndef CALC_ICE
#define CALC_ICE

module Demo
{
    enum operation { MIN, MAX, AVG };
  
    exception NoInput {};

    struct A
    {
        short a;
        long b;
        float c;
        string d;
    };

    sequence<long> LongSeq;
    interface Calc
    {
        long add(int a, int b);
        long subtract(int a, int b);
        void op(A a1, short b1); //załóżmy, że to też jest operacja arytmetyczna ;)
        idempotent double avg(LongSeq seq) throws NoInput;
    };

};

#endif
