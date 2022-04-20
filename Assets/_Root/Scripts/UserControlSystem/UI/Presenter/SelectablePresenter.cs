using System;
using Abstractions;
using Outlines;
using UnityEngine;

namespace _Root.Scripts.UserControlSystem.UI.Presenter
{
    public class SelectablePresenter : MonoBehaviour
    {
        [SerializeField] private SelectableObject _selectedObject;
        private ISelectable _currentSelectedObject;

        private void Start()
        {
            _selectedObject.OnSelected += Select;
        }

        private void Select(ISelectable selectable)
        {
            if (_currentSelectedObject == selectable)
            {
                return;
            }

            if (_currentSelectedObject != null)
            {
                SetSelected(_currentSelectedObject,false);
            }
            
            _currentSelectedObject = selectable;

            if (_currentSelectedObject != null)
            {
                SetSelected(_currentSelectedObject, true);
            }
        }

        private void SetSelected(ISelectable selectable, bool set)
        {
            selectable.Outline.enabled = set;
        }
    }
}