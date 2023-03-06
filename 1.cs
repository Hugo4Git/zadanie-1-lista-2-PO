//Hubert Dyczkowski
//lista 2 zadanie 1
//Mono C# compiler version 6.8.0.105

using System;

class IntStream //metody jak w opisie zadania
{
    public int number = -1;
    public int next()
    {
        number++;
        return number;
    }
    public bool eos()
    {
        if (number == Int32.MaxValue)
        {
            return true;
        }
        return false;
    }
    public void reset()
    {
        number = -1;
    }
}

class PrimeStream : IntStream
{
    bool prime() //prosty test pierwszości
    {
        if(number < 2)
        {
            return false;
        }
        for(int i=2; i*i<=number; i++)
        {
            if(number%i == 0)
            {
                return false;
            }
        }
        return true;
    }
    public new int next()
    {
        number++;
        while(!prime())
        {
            number++;
        }
        return number;
    }
    public new bool eos()
    {
        if(number == 2147483629) //największa liczba pierwsza w int32
        {
            return true;
        }
        return false;
    }
}

class RandomStream : IntStream
{
    Random rand = new Random();
    public new int next()
    {
        number = rand.Next();
        return number;
    }
    public new bool eos()
    {
        return false;
    }
}

class RandomWordStream
{
    PrimeStream primes = new PrimeStream();
    RandomStream rands = new RandomStream();
    public string next()
    {
        int len = primes.next();
        string res = "";
        for(int i=0; i<len; i++)
        {
            res += (char)('a' + rands.next()%26); //losowy znak
        }
        return res;     
    }
    public bool eos()
    {
        if(primes.eos())
        {
            return true;
        }
        return false;
    }
    public void reset()
    {
        primes.reset();
    }
}

class Program
{
    static void Main()
    {
        RandomWordStream x = new RandomWordStream();
        Console.WriteLine("Proszę wpisać jedno z poleceń:");
        Console.WriteLine("next - losowe słowo o długości kolejnej liczby pierwszej");
        Console.WriteLine("reset - zresetowanie strumienia\n");
        while(true) //testowanie programu
        {
            var a = Console.ReadLine();
            if(a == "next")
            {
                if(x.eos())
                {
                    Console.WriteLine("end of stream!\n");
                }
                else
                {
                    Console.WriteLine(x.next());
                    Console.WriteLine();
                }
            }
            else if(a == "reset")
            {
                x.reset();
                Console.WriteLine("reset successful!\n");
            }
            else
            {
                Console.WriteLine("wrong input!\n");
            }
        }
    }
}
