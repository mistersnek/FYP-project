//Code adapted from:
//Brackeys - https://www.youtube.com/watch?v=FkLJ45Pt-mY

using UnityEngine;

public class MovingWall : MonoBehaviour
{

    public float min = 2f;
    public float max = 3f;

    // Use this for initialization
    void Start()
    {

        min = transform.position.x;
        max = transform.position.x + 3;

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.y, transform.position.z);

    }
}

