using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace KittenPlayer
{
    internal class ActionsControl
    {
        public static ActionsControl Instance { get; set; }

        private  List<ActionsPair> ActionsList { get; } = new List<ActionsPair>();
        private int ActionsListIndex { get; set; }= -1;

        private ActionsControl()
        {
        }

        public static ActionsControl GetInstance()
        {
            if (Instance is null)
                Instance = new ActionsControl();
            return Instance;
        }

        public void AddAction(Action action, Action reversed)
        {
            ActionsList.Add(new ActionsPair(action, reversed));
            ActionsListIndex = ActionsList.Count - 1;
        }

        /// <summary>
        ///     Adds list of actions for Redo and Undo operations. Do not reverse before passing.
        /// </summary>
        public void AddActionsList(List<Action> redoActions, List<Action> undoActions)
        {
            void RedoActions()
            {
                foreach (var action in redoActions) action();
            }

            undoActions.Reverse();

            void UndoActions()
            {
                foreach (var action in undoActions) action();
            }

            ActionsList.Add(new ActionsPair(UndoActions, RedoActions));
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

        private class ActionsPair
        {
            public readonly Action Redo;
            public readonly Action Undo;

            public ActionsPair(Action Undo, Action Redo)
            {
                this.Undo = Undo;
                this.Redo = Redo;
            }
        }
    }
}