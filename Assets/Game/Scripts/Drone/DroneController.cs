using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneController : MonoBehaviour
{
    // components
    GameObject droneVisual;
    Animator animator;
    FixedJoystick joystick;

    // values
    [SerializeField]
    private float moveSpeed = 5f;

    // UI Buttons
    Button _takeOffButton;
    Button _landButton;

    // enum

    enum DroneState
    {
        DRONE_STATE_IDLE,
        DRONE_STATE_START_TAKINGOFF,
        DRONE_STATE_TAKINGOFF,
        DRONE_STATE_MOVING_UP,
        DRONE_STATE_FLYING,
        DRONE_STATE_START_LANDING,
        DRONE_STATE_LANDING,
        DRONE_STATE_LANDED,
        DRONE_STATE_WAIT_ENGINE_STOP,
    }

    DroneState _state;

    private void Start()
    {
        var marker = FindObjectOfType<PlacementMarker>().gameObject;
        marker.SetActive(false);
        // assigning the gameobjects

        droneVisual = transform.GetChild(0).gameObject;
        animator = droneVisual.GetComponent<Animator>();
        joystick = GameManager.instance.Joystick;

        _takeOffButton = GameManager.instance.TakeOffButton;
        _landButton = GameManager.instance.LandButton;

        // assiging values into game objects
        joystick.gameObject.SetActive(false);
        _takeOffButton.gameObject.SetActive(true);
        _state = DroneState.DRONE_STATE_IDLE;
        _takeOffButton.onClick.AddListener(TakeOffDrone);
        _landButton.onClick.AddListener(LandDrone);
    }

    private void Update()
    {
        ControlDroneStates();
    }

    private void FixedUpdate()
    {
        RotateDrone();
    }

    // update methods

    private void ControlDroneStates()
    {
        switch (_state)
        {
            case DroneState.DRONE_STATE_IDLE:
                break;

            case DroneState.DRONE_STATE_START_TAKINGOFF:
                animator.SetBool("TakeOff", true);
                _takeOffButton.gameObject.SetActive(false);
                _state = DroneState.DRONE_STATE_TAKINGOFF;
                break;

            case DroneState.DRONE_STATE_TAKINGOFF:
                if (animator.GetBool("TakeOff") == false)
                {
                    _state = DroneState.DRONE_STATE_MOVING_UP;
                }
                break;

            case DroneState.DRONE_STATE_MOVING_UP:
                if (animator.GetBool("MoveUp") == false)
                {
                    _state = DroneState.DRONE_STATE_FLYING;
                }
                break;

            case DroneState.DRONE_STATE_FLYING:

                MoveDrone();
                _landButton.gameObject.SetActive(true);
                joystick.gameObject.SetActive(true);
                break;

            case DroneState.DRONE_STATE_START_LANDING:
                animator.SetBool("MoveDown", true);
                _landButton.gameObject.SetActive(false);
                joystick.gameObject.SetActive(false);
                _state = DroneState.DRONE_STATE_LANDING;
                break;

            case DroneState.DRONE_STATE_LANDING:
                if (animator.GetBool("MoveDown") == false)
                    ;
                _state = DroneState.DRONE_STATE_LANDED;
                break;

            case DroneState.DRONE_STATE_LANDED:
                animator.SetBool("Land", true);
                _state = DroneState.DRONE_STATE_WAIT_ENGINE_STOP;
                break;

            case DroneState.DRONE_STATE_WAIT_ENGINE_STOP:
                if (animator.GetBool("Land") == false)
                {
                    _state = DroneState.DRONE_STATE_IDLE;
                    _takeOffButton.gameObject.SetActive(true);
                }
                break;
        }
    }

    private void MoveDrone()
    {
        var horizontal = joystick.Horizontal;
        var vertical = joystick.Vertical;

        var moveVector = droneVisual.transform.localPosition;
        moveVector += new Vector3(horizontal, 0f, vertical) * moveSpeed * Time.deltaTime;
        droneVisual.transform.localPosition = moveVector;
    }

    // fixed update methods
    private void RotateDrone()
    {
        if (_state == DroneState.DRONE_STATE_FLYING)
        {
            var zRotation = -30f * joystick.Horizontal * 60f * Time.deltaTime;
            var xRotation = 30f * joystick.Vertical * 60f * Time.deltaTime;

            var droneRotationvector = droneVisual.transform.localRotation;
            droneRotationvector = Quaternion.Euler(xRotation, droneRotationvector.y, zRotation);
            droneVisual.transform.localRotation = droneRotationvector;
        }
    }

    // rest of the methods



    private void TakeOffDrone()
    {
        if (IsIdle())
        {
            TakeOff();
        }
    }

    private void LandDrone()
    {
        if (IsFlying())
        {
            StartLanding();
        }
    }

    bool IsIdle()
    {
        return (_state == DroneState.DRONE_STATE_IDLE);
    }

    void TakeOff()
    {
        _state = DroneState.DRONE_STATE_START_TAKINGOFF;
    }

    bool IsFlying()
    {
        return (_state == DroneState.DRONE_STATE_FLYING);
    }

    void StartLanding()
    {
        _state = DroneState.DRONE_STATE_START_LANDING;
    }
}
