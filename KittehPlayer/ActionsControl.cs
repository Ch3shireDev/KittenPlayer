using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace KittehPlayer
{
    class ActionsControl
    {
        public static ActionsControl Instance = null;
        
        class ActionsPair
        {
            public Action Undo;
            public Action Redo;

            public ActionsPair(Action Undo, Action Redo)
            {
                this.Undo = Undo;
                this.Redo = Redo;
            }
        }

        List<ActionsPair> ActionsList = new List<ActionsPair>();
        int ActionsListIndex = -1;

        private ActionsControl() {

        }

        public static ActionsControl NewActionsControl()
        {
            if(Instance is null)
            {
                Instance = new ActionsControl();
            }
            return Instance;
        }

        public void AddAction(Action action, Action reversed)
        {
            ActionsList.Add(new ActionsPair(action, reversed));
            ActionsListIndex = ActionsList.Count - 1;
        }

        public void AddActionsList(List<Action> Actions, List<Action> Reversed)
        {
            Action RedoActions = () =>
            {
                foreach (Action action in Actions)
                {
                    action();
                }
            };
            
            Reversed.Reverse();

            Action UndoActions = () =>
            {
                foreach (Action action in Reversed)
                {
                    action();
                }
            };

            this.ActionsList.Add(new ActionsPair(UndoActions, RedoActions));
            ActionsListIndex = ActionsList.Count - 1;

        }

        public void Undo()
        {
            Debug.WriteLine(ActionsListIndex);
            Debug.WriteLine(ActionsList.Count);
            if (Enumerable.Range(0, ActionsList.Count).Contains(ActionsListIndex))
            {
                ActionsList[ActionsListIndex].Undo();
                ActionsListIndex--;
            }
            
        }

        public void Redo()
        {
            if (Enumerable.Range(0, ActionsList.Count).Contains(ActionsListIndex+1))
            {
                ActionsList[ActionsListIndex+1].Redo();
                ActionsListIndex++;
            }
        }
    }
}
