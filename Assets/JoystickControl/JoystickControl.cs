using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{
    #region Singletion

    public static JoystickControl instance;

    public bool isDisabled = false;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of JoystickControl found!");
            return;
        }

        instance = this;
    }

    #endregion

    public bool moveToTouch;
    public JoystickCanvasHud joystickCanvas;
    private float MaxMagnitude = 100f;

    private bool isDragging;
    private Vector2 startTouch, swipeDelta;
    private JoystickInputs Inputs;

    private Vector2 defPos;

    private void Start()
    {
        this.Inputs = new JoystickInputs();
        this.defPos = joystickCanvas.getDefaultPos();
    }

    private void Update()
    {
        this.HandleMobileControl();
    }

    void HandleMobileControl()
    {
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            if (moveToTouch)
                startTouch = Input.mousePosition;
            else
                startTouch = defPos;
        }
        else if (Input.GetMouseButtonUp(0) || isDisabled)
        {
            isDragging = false;
            Reset();
        }
        #endregion

        #region Mobile Inputs
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDragging = true;
                if (moveToTouch)
                    startTouch = Input.touches[0].position;
                else
                    startTouch = defPos;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled || isDisabled)
            {
                isDragging = false;
                Reset();
            }
        }
        #endregion

        swipeDelta = Vector2.zero;
        if (isDragging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        float joystickValue = swipeDelta.magnitude / MaxMagnitude;
        if (joystickValue > 1f) joystickValue = 1f;

        this.Inputs.joystickDirection = swipeDelta;
        this.Inputs.joystickValue = joystickValue;

        if (joystickValue != 0)
            joystickCanvas.SetJoystickPos(startTouch + Vector2.ClampMagnitude(swipeDelta, MaxMagnitude), startTouch);
        else
            joystickCanvas.SetDefaultJoystickPos();
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }

    public struct JoystickInputs
    {
        public Vector2 joystickDirection;
        public float joystickValue;
    }

    public JoystickInputs getInputs()
    {
        return this.Inputs;
    }

    public bool isDrug()
    {
        return this.isDragging;
    }
}
