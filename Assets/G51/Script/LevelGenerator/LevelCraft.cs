using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelCraft : MonoBehaviour
{
    public List<SpawnZone> Zones;

    [System.Serializable]
    public class SpawnZone
    {
        public List<Shape> shapes;
        
        [System.Serializable]
        public class Shape
        {
            public enum ShapeType {Dot, Box};
            public ShapeType type;
            public Vector2 position;
            [ShowIf("ShowIfBox")]
            public Vector2 size;
            public virtual Vector2 GeneratePos()
            {
                Vector2 returned = Vector2.zero;
                switch (type)
                {
                    case ShapeType.Dot:
                        returned = position;
                        break;
                    case ShapeType.Box:
                        float x = Random.Range(position.x, size.x);
                        float y = Random.Range(position.y, size.y);
                        returned = new Vector2(x, y);         
                        break;
                }
                return returned;
            }
            bool ShowIfBox()
            {
                return type == ShapeType.Box;
            }
        }
    }
}
