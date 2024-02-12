using UnityEngine;

[CreateAssetMenu(fileName = "CameraData", menuName = "ScriptableObjects/CameraData", order = 2)]
public class CameraData : ScriptableObject
{
    [SerializeField] public float deadZoneThreshold;
    [SerializeField] public float cameraThreshold;
}
