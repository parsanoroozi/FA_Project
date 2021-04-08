using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FA
{
    class State
    {
        public string name;
        public List<State> sources = new List<State>();
        public List<string> S_alphabet = new List<string>();
        public List<State> destinations = new List<State>();
        public List<string> D_alphabet = new List<string>();

        public State(string name)
        {
            this.name = name;
        }
    }
   class DFA
    {
        public List<State> dfa = new List<State>();

        public DFA(List <State> dfa)
        {
            this.dfa = dfa;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            List<State> FA = new List<State>();
            Console.WriteLine("Please enter the states like: q0,q1,q2 ....");
            string[] Statenames = Console.ReadLine().Split(',');
            for(int i =0; i< Statenames.Length; i++)
            {
                State temp = new State(Statenames[i]);
                FA.Add(temp);
            }
            Console.WriteLine("Please enter the alphabet like: a,b,c ....");
            string[] Alphabet = Console.ReadLine().Split(',');
            Console.WriteLine("Please enter how many rules you want to enter: ");
            int n = int.Parse(Console.ReadLine());
            for(int i=0; i< n; i++)
            {
                string[] token = Console.ReadLine().Split(' ');
                for(int j =0; j < FA.Count; j++)
                {
                    // fill sources
                    if(token[0] == FA[j].name)
                    {
                        for(int t =0; t < FA.Count; t++)
                        {
                            if(token[1] == FA[t].name)
                            {
                                FA[j].destinations.Add(FA[t]);
                                FA[j].D_alphabet.Add(token[2]);
                            }
                        }
                    }
                    //fill destinations
                    if (token[1] == FA[j].name)
                    {
                        for (int t = 0; t < FA.Count; t++)
                        {
                            if (token[0] == FA[t].name)
                            {
                                FA[j].sources.Add(FA[t]);
                                FA[j].S_alphabet.Add(token[2]);
                            }
                        }
                    }
                }



            }
        }
    }
}
