using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    private Transform camtransform;
    [SerializeField] float maxSpeed = 3f;
    [SerializeField] float rotateSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        camtransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontal,0, vertical);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            inputMagnitude = inputMagnitude / 2;
        }
        animator.SetFloat("movement", inputMagnitude, 0.1f,Time.deltaTime);
        float speed = maxSpeed * inputMagnitude;

        //以主相机控制向左向右移动
        movementDirection = Quaternion.AngleAxis(camtransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();
        Vector3 velocity = movementDirection * speed;
        characterController.Move(velocity * Time.deltaTime);

        //控制转向
        if (movementDirection !=Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
    }
}
