using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputBuffer : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputAction left;
    [SerializeField] private InputAction right;
    [SerializeField] private InputAction down;
    [SerializeField] private InputAction up;
    
    [SerializeField] private InputAction lp; // Light punch
    [SerializeField] private InputAction sp; // Strong punch
    [SerializeField] private InputAction lk; // Light kick
    [SerializeField] private InputAction sk; // Strong kick

    [Header("Buffer Settings")]
    public int bufferSize = 10;
    public Queue<FrameInputData> inputBuffer = new Queue<FrameInputData>();

    private int _currentFrame = 0;
    public HashSet<string> usedButtons = new HashSet<string>();


    void OnEnable()
    {
        EnableAction(left, "Left");
        EnableAction(right, "Right");
        EnableAction(down, "Down");
        EnableAction(up, "Up");
        EnableAction(lp, "LP");
        EnableAction(sp, "SP");
        EnableAction(lk, "LK");
        EnableAction(sk, "SK");
    }

    void OnDisable()
    {
        DisableAction(left);
        DisableAction(right);
        DisableAction(down);
        DisableAction(up);
        DisableAction(lp);
        DisableAction(sp);
        DisableAction(lk);
        DisableAction(sk);
    }

    private void EnableAction(InputAction action, string name)
    {
        action.Enable();
        action.canceled += ctx => usedButtons.Remove(name);
    }

    private void DisableAction(InputAction action)
    {
        action.Disable();
    }

    void Update()
    {
        UpdateBuffer();
    }

    private void UpdateBuffer()
    {
        _currentFrame++;
        List<string> pressed = new List<string>();
    
        GetInput(pressed);
        
        FrameInputData frameInput = new FrameInputData
        {
            frame = _currentFrame,
            pressedButtons = pressed,
        };
        

        inputBuffer.Enqueue(frameInput);
        
        if (inputBuffer.Count > bufferSize)
        {
            //TODO a function that removes and element and increase the hold attribute
            inputBuffer.Dequeue();
        }
    }

    private void GetInput(List<string> pressed)
    {
        if (left.IsPressed()) pressed.Add("Left");
        if (right.IsPressed()) pressed.Add("Right");
        if (down.IsPressed()) pressed.Add("Down");
        if (up.IsPressed()) pressed.Add("Up");

        if (lp.IsPressed()) pressed.Add("LP");
        if (sp.IsPressed()) pressed.Add("SP");
        if (lk.IsPressed()) pressed.Add("LK");
        if (sk.IsPressed()) pressed.Add("SK");
        
        if (pressed.Count == 0)
        {
            pressed.Add("Neutral");
        }
    }
}


[System.Serializable]
public class FrameInputData
{
    public int frame;
    public List<string> pressedButtons = new List<string>();
    public int hold;
}
