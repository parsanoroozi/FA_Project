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
        public bool isFinal;
        public bool isInit;
        public static List<string> alphabets;
        public string name;
        Dictionary<string, List<State>> DTransitions;
        public State(string name, bool isInit = false)
        {
            this.name = name;
            this.isInit = isInit;
            isFinal = false;
        }
        /// <summary>
        /// for adding a transition from this state to others.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="destination"></param>
        public void AddTransition(string key, State destination)
        {
            List<State> temp;

            bool keyExists = DTransitions.TryGetValue(key, out temp);
            if (keyExists)
            {
                if (!temp.Exists(x => x.name == destination.name)) temp.Add(destination);
            }
            else
            {
                temp = new List<State>() { destination };
                DTransitions.Add(key, temp);
            }
        }
    }
    class NFA
    {
        public State initialState { get; set; }
        public List<State> States;
        public NFA(State initialState, List<State> states)
        {
            this.initialState = initialState;
            this.States = states;
        }
        virtual public bool IsAcceptByFA(string input)
        {

        }
        public DFA CreateEquivalentDFA()
        {

        }
        public string findRegExp()
        {

        }
    }
    class DFA : NFA
    {

        public DFA(State initialState, List<State> states) : base(initialState, states)
        {
        }
        override public bool IsAcceptByFA(string input)
        {

        }
        public static DFA MakeSimpleDFA(DFA FA)
        {

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<State> states = new List<State>();
            Console.WriteLine("Please, Enter the states like ( q0,q1,q2, ...):");
            string[] Statenames = Console.ReadLine().Split(',');
            for (int i = 0; i < Statenames.Length; i++)
            {
                if (i != 0)
                {
                    State temp = new State(Statenames[i]);
                    states.Add(temp);
                }
                else
                {
                    State temp = new State(Statenames[i], true);
                    states.Add(temp);
                }
            }
            Console.WriteLine("Please, Enter the alphabet like (a,b,c, ...):");
            State.alphabets = Console.ReadLine().Split(',').ToList();            
            Console.WriteLine("Please, Enter the Final states like (q0,q1,q2, ...):");
            string[] FinalStates = Console.ReadLine().Split(',');
            for (int i = 0; i < FinalStates.Length; ++i)
            {
                for (int j = 0; j < states.Count; ++j)
                {
                    if (states[j].name == FinalStates[i])
                    {
                        states[j].isFinal = true;
                        break;
                    }
                }
            }
            Console.WriteLine("Please, Enter how many rules you want to enter: ");
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] token = Console.ReadLine().Split(' ');
                for (int j = 0; j < states.Count; j++)
                {
                    // fill sources
                    if (token[0] == states[j].name)
                    {
                        for (int t = 0; t < states.Count; t++)
                        {
                            if (token[1] == states[t].name)
                            {
                                states[j].destinations.Add(states[t]);
                                states[j].D_alphabet.Add(token[2]);

                            }
                        }
                    }
                    //fill destinations
                    if (token[1] == states[j].name)
                    {
                        for (int t = 0; t < states.Count; t++)
                        {
                            if (token[0] == states[t].name)
                            {
                                states[j].sources.Add(states[t]);
                                states[j].S_alphabet.Add(token[2]);
                            }
                        }
                    }
                }



            }
        }
    }
}
