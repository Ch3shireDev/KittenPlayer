using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace KittenPlayer
{
    internal class ActionsControl
    {
        public static ActionsControl Instance = null;

        private class ActionsPair
        {
            public Action Undo;
            public Action Redo;

            public ActionsPair(Action Undo, Action Redo)
            {
                this.Undo = Undo;
                this.Redo = Redo;
            }
        }

        private List<ActionsPair> ActionsList = new List<ActionsPair>();
        private int ActionsListIndex = -1;

        private ActionsControl()
        {
        }

        public static ActionsControl GetInstance()
        {
            if (Instance is null)
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

        /// <summary>
        /// Adds list of actions for Redo and Undo operations. Do not reverse before passing.
        /// </summary>

        public void AddActionsList(List<Action> redoActions, List<Action> undoActions)
        {
            Action RedoActions = () =>
            {
                foreach (Action action in redoActions)
                {
                    action();
                }
            };

            undoActions.Reverse();

            Action UndoActions = () =>
            {
                foreach (Action action in undoActions)
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
            if (Enumerable.Range(0, ActionsList.Count).Contains(ActionsListIndex + 1))
            {
                ActionsList[ActionsListIndex + 1].Redo();
                ActionsListIndex++;
            }
        }
    }
}