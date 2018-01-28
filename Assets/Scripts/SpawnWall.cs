using UnityEngine;

public class SpawnWall : MonoBehaviour {

    public Camera main;
    public bool isLeft;

	void Start () {
        var dist = (transform.position - main.transform.position).z;
        var leftBorder = main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        if (isLeft)
            transform.position = new Vector2(leftBorder, transform.position.y);
        else
            transform.position = new Vector2(rightBorder, transform.position.y);
    }
}
