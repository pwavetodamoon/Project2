using UnityEngine;

public class HealthBarStatic : HealthBarBase
{

    public float xPos = -.8f;
    public float yPos = .3f;
    public void SetPosition(Vector2 position)
    {
        position = new Vector3(position.x + xPos, position.y + yPos);
        transform.position = new Vector3(position.x, position.y);
    }

}
