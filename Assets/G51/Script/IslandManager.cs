using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IslandManager : MonoBehaviour
{
    public List<IslandInfo> islands;
    public float step;

    private void LateUpdate()
    {
        Vector2 pos = transform.position;
        foreach (var island in islands)
        {
            Vector2 p = island.position;
            float lt = Mathf.Min(island.position.y - pos.y, 0f);

            Vector2 a = island.position;
            Vector2 b = new Vector2(0, a.y);
            island.transform.position = Vector2.LerpUnclamped(a, b, lt);
        }

        float speed = 10f;
        Vector3 moveDirection = transform.forward;
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = moveDirection * speed * Time.deltaTime;

    }
    
    [System.Serializable]
    public class IslandInfo
    {
        public Vector3 position;
        public Transform transform;
    }
}
