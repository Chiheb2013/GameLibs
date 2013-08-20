using System;
using System.Linq;
using System.Collections.Generic;

namespace Mappy.States
{
    public class StateSystem
    {
        static SFML.Graphics.RenderWindow renderWindow;
        
        IState currentState;
        Dictionary<string, IState> states;

        public IState CurrentState { get { return currentState; } set { currentState = value; } }
        public IState this[string name] { get { return states[name]; } }
        public Dictionary<string, IState> States { get { return states; } }

        public static SFML.Graphics.RenderWindow RenderWindow { get { return renderWindow; } set { renderWindow = value; } }

        public StateSystem()
        {
            this.states = new Dictionary<string, IState>();
        }
       

        public void AddState(string name, IState state)
        {
            if (!states.Keys.Contains(name))
                states.Add(name, state);
            else throw new ArgumentException("Symbol '" + name + "' is already used. source:StateSystem.AddState()");
        }

        public void SetCurrentState(string name)
        {
            if (states.Keys.Contains(name))
                currentState = states[name];
            else throw new KeyNotFoundException("Symbol '" + name + "' was not found. source:StateSystem.SetCurrentState()");
        }
    }
}
