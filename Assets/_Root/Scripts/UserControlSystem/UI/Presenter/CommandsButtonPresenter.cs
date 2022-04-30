using System;
using System.Collections.Generic;
using _Root.Scripts.Core.Unit;
using Abstractions;
using Injector;
using UI.View;
using UnityEngine;
using UserControl;
using Zenject;


namespace _Root.Scripts.UserControlSystem.UI.Presenter
{
    public class CommandsButtonPresenter : MonoBehaviour
    {
        [SerializeField] private SelectableObject _selectable;
        [SerializeField] private CommandButtonsView _view;
        [Inject] private CommandsButtonModel _model;
        private ISelectable _currentSelectable;

        private void Start()
        {
            _view.OnClick += _model.OnCommandButtonClicked;
            _model.OnCommandSent += _view.UnblockAllInteractions;
            _model.OnCommandCancel += _view.UnblockAllInteractions;
            _model.OnCommandAccepted += _view.BlockInteractions;

            _selectable.OnNewValue += OnSelected;
            OnSelected(_selectable.CurrentValue);
        }

        private void OnSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable)
            {
                return;
            }

            if (_currentSelectable != null)
            {
                _model.OnSelectionChanged();
            }

            _currentSelectable = selectable;
            _view.Clear();
            if (selectable != null)
            {
                var commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange((selectable as Component).GetComponentsInParent<ICommandExecutor>());
                _view.MakeLayout(commandExecutors);
            }
        }
    }
}