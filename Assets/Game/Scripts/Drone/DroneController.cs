using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    [Header("Drone Components")]
    GameObject droneVisual;
    Animator anim;

    [Header("Animation Parameters Strings")]
    [SerializeField]
    string takeOffBool = "TakeOff";

    [Header("Drone Configs")]
    [SerializeField]
    float moveSpeed;

    private void Awake()
    {
        droneVisual = transform.GetChild(0).gameObject;
        anim = droneVisual.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
              anim.SetBool(takeOffBool, true);
        }
        MoveDrone();
    }

    private void MoveDrone()
    {
        float speedX = Input.GetAxis("Horizontal") * moveSpeed;
        float speedZ = Input.GetAxis("Vertical") * moveSpeed;

        droneVisual.transform.localPosition += new Vector3(speedX, 0, speedZ) * Time.deltaTime;
    }
}
