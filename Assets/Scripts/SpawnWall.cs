using UnityEngine;

public class SpawnWall : MonoBehaviour {

    public Camera main;
    public bool isLeft;
    float dist;
    public Transform player;
    
    void Start ()
    {
        dist = (transform.position - main.transform.position).z;
        var leftBorder = main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        if (isLeft)
            transform.position = new Vector2(leftBorder - transform.localScale.x, transform.position.y);
        else
            transform.position = new Vector2(rightBorder + transform.localScale.x, transform.position.y);
    }

    private void Update()
    {
            if (isLeft && player.position.x <= transform.position.x + transform.localScale.x)
                player.position = new Vector2(transform.position.x + transform.localScale.x * 2,player.position.y);
            else if (!isLeft && player.position.x >= transform.position.x - transform.localScale.x)
                player.position = new Vector2(transform.position.x - transform.localScale.x * 2, player.position.y);
    }
}
