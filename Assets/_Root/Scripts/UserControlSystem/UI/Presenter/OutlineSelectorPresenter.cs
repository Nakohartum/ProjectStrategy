using System;
using Abstractions;
using PlasticPipe.PlasticProtocol.Client;
using UI.View;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Root.Scripts.UserControlSystem.UI.Presenter
{
    public class OutlineSelectorPresenter : MonoBehaviour
    {
        [Inject] private IObservable<ISelectable> _selectableObject;
        private OutlineSelector[] _outlineSelectors;
        private ISelectable _currentSelectable;

        private void Start()
        {
            _selectableObject.Subscribe(OnSelected);
        }

        private void OnSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable)
            {
                return;
            }

            SetSelected(_outlineSelectors, false);
            _outlineSelectors = null;
            if (selectable != null)
            {
                _outlineSelectors = (selectable as Component).GetComponentsInParent<OutlineSelector>();
                SetSelected(_outlineSelectors, true);
            }
            else
            {
                if (_outlineSelectors != null)
                {
                    SetSelected(_outlineSelectors, false);
                }
            }

            _currentSelectable = selectable;

            static void SetSelected(OutlineSelector[] selectors, bool value)
            {
                if (selectors != null)
                {
                    for (int i = 0; i < selectors.Length; i++)
                    {
                        selectors[i].SetSelected(value);
                    }
                }
            }
        }
    }
}