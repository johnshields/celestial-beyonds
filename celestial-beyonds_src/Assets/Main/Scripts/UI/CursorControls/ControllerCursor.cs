using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// refs - https://youtu.be/cWAdUrw2bXQ & https://youtu.be/7h1cnGggY2M
// Required with new Input System to control Cursor with Mouse and Gamepad
namespace Main.Scripts.UI.CursorControls
{
    public class ControllerCursor : MonoBehaviour
    {
        public float speed;
        public GameObject canvasUI;
        public string clickedElement;
        private GraphicRaycaster _raycaster;
        private PointerEventData _pointerEventData;
        private List<RaycastResult> _raycastResults;
        private Rigidbody2D _rb;
        private Vector2 moveInputValue;
        private bool _leftStickMoved;

        private void Awake()
        {
            foreach (var t in Gamepad.all)
                print(t == null ? $"Connected Gamepad: {false}" : $"Connected Gamepad: {t.name}");
        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _raycaster = canvasUI.GetComponent<GraphicRaycaster>();
            _pointerEventData = new PointerEventData(EventSystem.current);
            _raycastResults = new List<RaycastResult>();
        }

        private void OnMove(InputValue val)
        {
            if (Bools.cursorRequired)
            {
                moveInputValue = val.Get<Vector2>();
                //print($"inputValue moved: {val}");   
            }
        }

        private void MoveLogicMethod()
        {
            if (Bools.cursorRequired)
            {
                var result = moveInputValue * (speed * Time.fixedDeltaTime);
                _rb.velocity = result;   
            }
        }

        private void FixedUpdate()
        {
            if (Bools.cursorRequired)
            {
                MoveLogicMethod();
            
                if (Mouse.current.leftButton.wasReleasedThisFrame)
                    GetClickedUI();

                if (Gamepad.current != null)
                {
                    if (Gamepad.current.buttonSouth.IsPressed())
                    {
                        _leftStickMoved = true; 
                        GetClickedUI();
                    }
                }   
            }
        }

        private void GetClickedUI()
        {
            if (!_leftStickMoved)
                _pointerEventData.position = Mouse.current.position.ReadValue();
            else
            {
                if (Gamepad.current != null)
                    _pointerEventData.position = transform.position;
                _leftStickMoved = false;
            }

            _raycastResults.Clear();
            _raycaster.Raycast(_pointerEventData, _raycastResults);

            foreach (RaycastResult result in _raycastResults)
            {
                clickedElement = result.gameObject.name;
                print($"Cursor clicked: {clickedElement}");
            }
        }
    }
}