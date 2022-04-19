using System;
using System.Linq;
using Abstractions;
using UnityEngine;

namespace _Root.Scripts.UserControlSystem
{
    public class MouseInteractionPresenter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SelectableObject _selectedObject;
        
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            var hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
            if (hits.Length == 0)
            {
                return;
            }

            var mainBuilding = hits.Select(hit => hit.collider.GetComponent<IUnitProduce>())
                .FirstOrDefault(c => c != null);

            var selectable = hits.Select(hit => hit.collider.GetComponent<ISelectable>())
                .FirstOrDefault();
            if (_selectedObject.CurrentValue != default)
            {
                _selectedObject.CurrentValue.Outline.enabled = false;
            }
            _selectedObject.SetValue(selectable);
            if (selectable!=default)
            {
                _selectedObject.CurrentValue.Outline.enabled = true;
            }

            if (mainBuilding == default)
            {
                return;
            }
            
            mainBuilding.ProduceUnit();
        }
    }
}