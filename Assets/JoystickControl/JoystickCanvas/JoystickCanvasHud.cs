using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickCanvasHud : MonoBehaviour
{
    public RectTransform joystickPos;
    public RectTransform joystickBackgroundPos;
    Vector3 defPos;

    private void Start()
    {
        this.defPos = joystickPos.position;
    }

    public void SetJoystickPos(Vector2 currentPos, Vector2 startPos)
    {
        this.joystickPos.position = new Vector3(currentPos.x, currentPos.y, 0f);
        this.joystickBackgroundPos.position = new Vector3(startPos.x, startPos.y, 0f);
    }

    public void SetDefaultJoystickPos()
    {
        this.joystickPos.position = defPos;
        this.joystickBackgroundPos.position = defPos;
    }

    public Vector2 getDefaultPos()
    {
        return new Vector2(joystickPos.position.x, joystickPos.position.y);
    }
}
