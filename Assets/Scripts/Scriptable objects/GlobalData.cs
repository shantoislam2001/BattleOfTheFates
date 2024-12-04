using UnityEngine;

[CreateAssetMenu(fileName = "GlobalData", menuName = "Scriptable Objects/GlobalData")]
public class GlobalData : ScriptableObject
{
    public float walkSpeedW;
    public float runSpeedW;
    public float jumpHightW;
    public float gravityW;
    public float rotationSpeedW;
    public float lookSencitivity;
    public float cameraSmothTime;
    public Vector3 cameraOffset;
}    
