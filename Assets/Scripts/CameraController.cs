using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Entity entityOne;
    [SerializeField] private Entity entityTwo;
    [SerializeField] private CameraData _data;
    private Vector3 auxPos;
    
    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }
    private void UpdatePosition()
    {
        float positionOne = Mathf.Abs(entityOne.transform.position.x);
        float positionTwo = Mathf.Abs(entityTwo.transform.position.x);
        float xDifferenceOne = Mathf.Abs(entityOne.transform.position.x - transform.position.x);
        float speedOne = entityOne.GetComponent<Rigidbody2D>().velocity.x;
        
        if(positionOne >= _data.cameraThreshold || positionTwo >= _data.cameraThreshold)
        {
            return;
        }
        if(xDifferenceOne > _data.deadZoneThreshold && (speedOne >= 0.1f || xDifferenceOne <= 7))
        {
            auxPos = new Vector3(entityOne.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, auxPos,entityOne.speed * Time.deltaTime);
            Debug.Log("One");
        }

        try
        {
            float xDifferenceTwo = Mathf.Abs(entityTwo.transform.position.x - transform.position.x);
            float speedTwo = entityTwo.GetComponent<Rigidbody2D>().velocity.x;
            if(xDifferenceTwo > _data.deadZoneThreshold && (speedTwo >= 0.1f || xDifferenceTwo <= 7))
            {
                auxPos = new Vector3(entityTwo.transform.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, auxPos, entityTwo.speed * Time.deltaTime);
                Debug.Log("Two");
            } 
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e.Message);
        }
    }
}
