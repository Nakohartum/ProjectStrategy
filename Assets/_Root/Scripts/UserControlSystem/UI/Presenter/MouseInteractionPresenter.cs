using System;
using System.Linq;
using Abstractions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Root.Scripts.UserControlSystem
{
    public class MouseInteractionPresenter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectableObject _selectedObject;
        [SerializeField] private EventSystem _eventSystem;
        
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            if (_eventSystem.IsPointerOverGameObject())
            {
                return;
            }
            var hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
            if (hits.Length == 0)
            {
                return;
            }
            
            var selectable = hits.Select(hit => hit.collider.GetComponent<ISelectable>())
                .FirstOrDefault();
            _selectedObject.SetValue(selectable);
            
            
            
        }
    }
}