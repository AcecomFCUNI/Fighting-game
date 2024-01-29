using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Entity entity;
    [SerializeField] float deadZoneThreshold;
    private Vector3 auxPos;
    
    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }
    private void UpdatePosition()
    {
        float xDifference = Mathf.Abs(entity.transform.position.x - transform.position.x);

        if(xDifference >= deadZoneThreshold)
        {
            auxPos = new Vector3(entity.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, auxPos, entity.speed * Time.deltaTime);
        }
    }
}
