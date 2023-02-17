using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private GameInput gameInput;

    /* stairs code */

    [SerializeField] GameObject stepRayUpperZ;
    [SerializeField] GameObject stepRayLowerZ;
    [SerializeField] float stepHeight = 0.7f;
    [SerializeField] float stepSmooth = 0.3f;
    [SerializeField] float lowerFloat = 0.1f;
    [SerializeField] float upperFloat = 0.2f;
    [SerializeField] float playerRadius = 0.7f;

    /* stairs code */

    private bool isWalking;

    private void Awake()
    {
        stepRayUpperZ.transform.position = new Vector3(stepRayUpperZ.transform.position.x, stepHeight, stepRayUpperZ.transform.position.z);
    }
     
    void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime; 
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove) //cannot move towards moveDir
        {
            // attempt to move to X
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) //can move on X
            {
                moveDir = moveDirX;
            }
            else  // cannot move on X
            {
                // attempt Z
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove) // can move only on the Z
                {
                    moveDir = moveDirZ;
                }
                else
                { // cannot move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);


        stepClimb();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    void stepClimb()
    {
        Debug.Log("stepRayLower.transform.position: " + stepRayLowerZ.transform.position);
        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLowerZ.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, lowerFloat))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpperZ.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, upperFloat))
            {
                transform.position -= new Vector3(0f, -stepSmooth, 0f);
            }
        }
    }
}
