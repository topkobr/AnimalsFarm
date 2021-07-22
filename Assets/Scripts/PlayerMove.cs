using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    JoystickControl joystickInput;
    [SerializeField] Animator _rigAnimator;

    float turnSmoothVelocity;
    float turnSmoothTime = .09f;
    float moveSmoothVelocity;
    float moveSmoothTime = .2f;
    Vector3 moveDir;
    float moveSmooth;
    [SerializeField] float speed = 2f;
    bool groundedPlayer;
    Vector3 gravityVelocity = new Vector3();

    LayerMask checkGroundMask;
    public Transform checkGround;

    Transform _wheatDirection;

    private bool _isJoystickNow;

    void Start()
    {
        this.joystickInput = JoystickControl.instance;
        checkGroundMask = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        // Set utils variables
        groundedPlayer = Physics.CheckSphere(checkGround.position, 1f, checkGroundMask);
        float isRunning = this.joystickInput.getInputs().joystickValue;
        Vector2 joystickInpit = this.joystickInput.getInputs().joystickDirection;
        Vector3 direction = new Vector3(joystickInpit.x, 0f, joystickInpit.y).normalized;

        // Check joystick Input
        if (joystickInpit != new Vector2())
            _isJoystickNow = true;
        else
            _isJoystickNow = false;

        // Check rotate to wheat
        if (_wheatDirection != null && !_isJoystickNow)
        {
            direction = (_wheatDirection.position - transform.position).normalized;
            direction.y = 0f;
        }

        // Save direction if joystick not tapped
        if (direction != new Vector3())
        {
            moveDir = direction;
        }

        // Check minimal speed and prevent falling move
        if (!groundedPlayer || (transform.forward - direction).magnitude > 1.3f)
            isRunning = 0f;

        // Player rotate and move
        float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
        moveDir = Quaternion.Euler(0f, smoothAngle, 0f) * Vector3.forward;
        moveSmooth = Mathf.SmoothDamp(moveSmooth, isRunning, ref moveSmoothVelocity, moveSmoothTime);
        transform.position += transform.forward * speed * moveSmooth * Time.deltaTime;

        // Set grounded gravity
        if (groundedPlayer)
            gravityVelocity.y = 0f;
        else
            gravityVelocity.y += -0.1981f * Time.deltaTime;

        // Reach maximum gravity
        if (gravityVelocity.y < -.4f)
            gravityVelocity.y = -.4f;

        // Gravity move;
        transform.position += gravityVelocity;

        // Animations
        if (_rigAnimator != null)
            _rigAnimator.SetFloat("Speed", moveSmooth);
    }

    public float GetMoveSmooth()
    {
        return this.moveSmooth;
    }
    public void SetWheatDirection(Transform wheatTransform)
    {
        _wheatDirection = wheatTransform;
    }
    public bool IsJoystickClick()
    {
        return _isJoystickNow;
    }
    public void WheatAnimation(bool isTrue)
    {
        if (_rigAnimator != null)
        {
            if (isTrue)
            {
                _rigAnimator.SetBool("WheatNow", true);
            } 
            else
            {
                _rigAnimator.SetBool("WheatNow", false);
            }
        }
    }
}
