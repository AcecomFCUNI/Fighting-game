using UnityEngine;
using System;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player playerOne;
    [SerializeField] private Player playerTwo;

    [SerializeField] private float deadZoneThreshold = 5;
    [SerializeField] private float cameraThreshold = 7.5f;
    private Vector3 auxPos;
    
    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }
    private void UpdatePosition()
    {
        float xDifferenceOne = Mathf.Abs(playerOne.transform.position.x - transform.position.x);
        float xDifferenceTwo = Mathf.Abs(playerTwo.transform.position.x - transform.position.x);

        if((xDifferenceTwo >= 6.8f && playerOne.MovementInput == playerTwo.transform.right.x) || 
           (xDifferenceOne >= 6.8f && playerTwo.MovementInput == playerOne.transform.right.x ))
        {
            return;
        }
        
        if(xDifferenceOne >= deadZoneThreshold)
        {
            auxPos = transform.position + new Vector3(Time.deltaTime * Player.speed * playerOne.MovementInput, 0, 0);
            if(!IsOutOfLimits(ref auxPos))
            {
                transform.position = 
                    Vector3.MoveTowards(transform.position, auxPos, Player.speed * 0.75f * Time.deltaTime);
            }
        }

        if(xDifferenceTwo > deadZoneThreshold)
        {
            auxPos = transform.position + new Vector3(Time.deltaTime * Player.speed * playerTwo.MovementInput, 0, 0);
            if(!IsOutOfLimits(ref auxPos))
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, auxPos, Player.speed * 0.75f * Time.deltaTime);
            }
            
        } 
    }

    private bool IsOutOfLimits(ref Vector3 auxPos)
    {
        float posX = Mathf.Abs(auxPos.x);
        if(posX >= cameraThreshold)
        {
            auxPos.x = cameraThreshold;
            return true;
        }
        return false;
    }
}