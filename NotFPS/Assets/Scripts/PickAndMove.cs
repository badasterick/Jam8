using UnityEngine;
using System.Collections;

public class PickAndMove : MonoBehaviour
{
    public GameObject fPSController;

    // Range of movement on X axis this will set from x to -x.
    public float clampForX = 10;
    //Value for minimum/lowest movement value on Y axis.
    public float clampForYmin = 1;
    //Value for maximum/highest movement value on Y axis.
    public float clampForYmax = 20;
    //Value for where the Game object starts off on the Z axis.
    private float startPositionOnZAxis;

    public float distanceToHold = 3.0f;

   // bool isColloiding = false;
    float distanceFromPlayer = 0.0f;



    void Start()
    {
        startPositionOnZAxis = this.transform.position.z;
       
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, distanceToHold))
        {
            distanceFromPlayer = Vector3.Distance(hit.transform.position, Camera.main.gameObject.transform.position);
            if (hit.transform.tag.Equals("Metal"))
            {

                float massDifference = hit.transform.GetComponent<Rigidbody>().mass - fPSController.GetComponent<Rigidbody>().mass;
                if (massDifference < 0)
                {
                    if (distanceFromPlayer < distanceToHold)
                        StartCoroutine(mouseDown(hit.transform));
                }

            }
        }


        
    }

    IEnumerator mouseDown(Transform hit)
    {
        while (Input.GetMouseButton(1))
        {
            //if(!isColloiding)
            MoveUpdate(hit);
            yield return null;  // Give up control 
        }

    }


    void MoveUpdate(Transform hit)
    {

            Vector3 tempPos = Camera.main.ScreenToWorldPoint(new Vector3(hit.position.x,
                                                        hit.position.y, hit.position.z));
            float tempClampX = Mathf.Clamp(0.01f, -0.01f, 0.01f);

            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Rotation(tempClampX) + 3));

            float clampX = Mathf.Clamp(pos.x, -clampForX, clampForX);
            float clampY = Mathf.Clamp(pos.y, clampForYmin, clampForYmax);
            hit.position = new Vector3(pos.x, pos.y, pos.z);


    }

    float Rotation(float clamp)
    {

        // change from 0 to 1;
        float change;
        float rotation=0;

        float absVal = Mathf.Abs(clamp);
        change = absVal / clampForX;
        rotation = change - 1;
        return rotation;
    }


}