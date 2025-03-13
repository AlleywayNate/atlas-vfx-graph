using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    void Update()
    {
        Vector2 cursorPos= Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(cursorPos.x, cursorPos.y);
    }
}