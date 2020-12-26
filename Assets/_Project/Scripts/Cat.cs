using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{

    public delegate void CatEventHandler();
    public event CatEventHandler OnFlip;
    public event CatEventHandler OnWin;
    public event CatEventHandler OnGameOver;


    protected CharacterController controller;
    protected bool isGrounded = true;
    protected float rotation;

    [SerializeField]
    protected float rotationSpeed;
    [SerializeField]
    protected float fallingSpeed;
    [SerializeField]
    protected ParticleSystem starParticle;

    [SerializeField]
    protected Transform checkGround;
    [SerializeField]
    protected float checkGroundRadius;
    [SerializeField]
    protected LayerMask checkGroundLayer;



    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    protected void Update()
    {

    }

    public void CheckInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE

        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToViewportPoint(mousePosition);

            if(mousePosition.x > 0.5f)
            {
                RotateRight();
            } else
            {
                RotateLeft();
            }
        }
//#elif UNITY_IPHONE || UNITY_ANDROID
#endif
    }

    private void RotateLeft()
    {
        Debug.Log("left");
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        rotation += rotationSpeed * Time.deltaTime;
        if(Mathf.Abs(rotation) >= 360)
        {
            starParticle.Play();
            rotation -= 360;
            OnFlip.Invoke();
        }
    }

    public void Fall()
    {
        CheckGround();
        if (!isGrounded)
        {
            controller.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
        }
    }

    private void CheckGround()
    {
        Collider[] colliders = Physics.OverlapSphere(checkGround.position, checkGroundRadius, checkGroundLayer);
        if(colliders.Length == 0)
        {
            isGrounded = false;
        }
    }

    private void RotateRight()
    {
        Debug.Log("right");
        transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        rotation += -rotationSpeed * Time.deltaTime;
        if (Mathf.Abs(rotation) >= 360)
        {
            starParticle.Play();
            rotation += 360;
            OnFlip.Invoke();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Floor"))
        {
            if (Vector3.Angle(transform.up, Vector3.up) >= 30)
            {
                //Activation GameOver et ragdoll
                gameObject.SetActive(false);
                OnGameOver.Invoke();
            }
            else
            {
                //Win
                OnWin.Invoke();
            }
        }
        

        isGrounded = Vector3.Angle(Vector3.up,hit.normal) <= controller.slopeLimit;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(checkGround.position, checkGroundRadius);
    }
}
