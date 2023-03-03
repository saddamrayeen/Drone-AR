using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(instance);
            instance = this;
        }
    }

    [SerializeField]
    private Button _takeOffButton;

    [SerializeField]
    Button _landButton;

    [SerializeField]
    FixedJoystick _joystick;

    // parameters for assiging it into a different script
    public Button TakeOffButton
    {
        get { return _takeOffButton; }
    }
    public Button LandButton
    {
        get { return _landButton; }
    }
    public FixedJoystick Joystick
    {
        get { return _joystick; }
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
