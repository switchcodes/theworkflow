using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveChanger : MonoBehaviour
{
    [SerializeField] Vector2 []direction;
    List<Vector2> list;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetNewMoveDirection()
    {
        return direction[Random.Range(0, direction.Length)];
    }
}
