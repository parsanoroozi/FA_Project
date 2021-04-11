using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Msagl;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.Splines;
using System.Windows;
using System.Diagnostics;
using System.Windows.Forms.Integration;
using System.Windows.Forms;

namespace FA
{
    class State
    {
        public bool isFinal;
        public bool isInit;
        public static List<string> alphabets;
        public string name;
        public Dictionary<string, List<State>> DTransitions = new Dictionary<string, List<State>>();
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
            bool answer = false;



            return answer;
        }
        public DFA CreateEquivalentDFA()
        {
            int t = FindStateNumber(initialState.name);
            List<State> DFAStates = new List<State>() { new State(""), new State(Math.Pow(2, t).ToString(), true) };


        }

        /// <summary>
        /// Takes a state name and return the index of it in the states list.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        int FindStateNumber(string Name)
        {
            int temp;
            temp = States.FindIndex(x => x.name == Name);
            return temp;
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
            bool answer = true;
            State temp = States[0];

            for (int i = 0; i < input.Length; i++)
            {
                List<State> tempList;
                if (temp.DTransitions.TryGetValue(input[i].ToString(), out tempList))
                    temp = tempList[0];
                else
                    return false;
            }
            if (!temp.isFinal)
                return false;

            return answer;

        }
        public static DFA MakeSimpleDFA(DFA FA)
        {

        }
        public void ShowSchematicDFA()
        {
            WindowsFormsHost GraphView = new WindowsFormsHost();
            //create a form
            Form form = new Form();
            //create a viewer object
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();

            //Margin = "0,34,0,0"
            Graph Dfa = new Graph("DFA");
            foreach (State s in this.States)
            {
                if (s.isInit)
                {
                    AddInitNode(Dfa, s.name, s.isFinal, s);
                }
                else
                {
                    AddCNode(Dfa, s.name, s.isFinal, s);
                }
            }
            foreach (State s in this.States)
            {
                foreach (string L in s.DTransitions.Keys)
                {
                    s.DTransitions[L].ForEach((x) => { Dfa.AddEdge(s.name, L, x.name); });
                }
            }
            viewer.Graph = Dfa;
            form.SuspendLayout();
            viewer.Dock = DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            ///show the form
            form.ShowDialog();
        }

        private void AddInitNode(Graph graph, string nodeName, bool IsFinal = false, State s = null)
        {
            Microsoft.Msagl.Drawing.Node init = new Microsoft.Msagl.Drawing.Node(nodeName);
            init.Attr.FillColor = Color.LavenderBlush;
            init.Attr.Shape = Shape.Triangle;
            if (IsFinal)
            {
                init.Attr.AddStyle(Style.Bold);
                init.Attr.FillColor = Color.PowderBlue;
            }

            init.Attr.XRadius = 4;
            init.Attr.YRadius = 4;
            init.Attr.LineWidth = 10;
            if (s != null)
                init.UserData = s;
            else
            {
                init.UserData = nodeName;
            }

            graph.AddNode(init);
        }
        private void AddCNode(Graph graph, string nodeName, bool IsFinal = false, State s = null)
        {
            Microsoft.Msagl.Drawing.Node nod = new Microsoft.Msagl.Drawing.Node(nodeName);
            nod.Attr.FillColor = Color.Honeydew;
            nod.Attr.Shape = Shape.Circle;
            if (IsFinal)
            {
                nod.Attr.Shape = Shape.DoubleCircle;
                nod.Attr.AddStyle(Style.Bold);
                nod.Attr.FillColor = Color.PowderBlue;
            }
            nod.Attr.XRadius = 5;
            nod.Attr.YRadius = 5;
            nod.Attr.LineWidth = 10;
            if (s != null)
                nod.UserData = s;
            else
            {
                nod.UserData = nodeName;
            }
            graph.AddNode(nod);
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
            State.alphabets.Add("");
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
                string[] token = Console.ReadLine().Split(',');
                for (int j = 0; j < states.Count; j++)
                    if (states[j].name == token[0])
                        for (int t = 0; t < states.Count; t++)
                            if (states[t].name == token[1])
                                states[j].AddTransition(token[2], states[t]);
            }

            /*NFA nfa = new NFA(states[0], states);
            Console.WriteLine();
            Console.WriteLine("Alphabet : ");

            foreach (var i in State.alphabets)
                Console.WriteLine(i);

            Console.WriteLine("States: ");
            for (int i = 0; i < nfa.States.Count; i++)
            {
                Console.WriteLine($"name: {nfa.States[i].name}   isinitial: {nfa.States[i].isInit}    isfinal: {nfa.States[i].isFinal}");
                foreach (var j in nfa.States[i].DTransitions)
                    foreach (var t in j.Value)
                        Console.WriteLine($"source: {nfa.States[i].name}  alphabet: {j.Key}  destination: {t.name}");
                Console.WriteLine();
            }*/


        }
    }
}
