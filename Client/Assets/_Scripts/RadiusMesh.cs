using UnityEngine;
using System.Collections;

/**
 * @author mwan
 * Version: 1.0
 * Date: 2016/8/15
 * Description: 绘制扇形网格.
 * Copyright (c) 2016 OEPT inc. All rights reserved.
 */
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class RadiusMesh : MonoBehaviour {

    public float radius = 2;
    public float angleDegree = 100;
    public int segments = 10;
    public int angleDegreePrecision = 1000;
    public int radiusPrecision = 1000;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private SectorMeshCreator creator = new SectorMeshCreator();

    [ExecuteInEditMode]
    private void Awake()
    {

        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        meshFilter.mesh = creator.CreateMesh(radius, angleDegree, segments, angleDegreePrecision, radiusPrecision);
        meshRenderer.enabled = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;
        DrawMesh();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        DrawMesh();
    }

    private void DrawMesh()
    {
        Mesh mesh = creator.CreateMesh(radius, angleDegree, segments, angleDegreePrecision, radiusPrecision);
        int[] tris = mesh.triangles;
        for (int i = 0; i < tris.Length; i += 3)
        {
            Gizmos.DrawLine(convert2World(mesh.vertices[tris[i]]), convert2World(mesh.vertices[tris[i + 1]]));
            Gizmos.DrawLine(convert2World(mesh.vertices[tris[i]]), convert2World(mesh.vertices[tris[i + 2]]));
            Gizmos.DrawLine(convert2World(mesh.vertices[tris[i + 1]]), convert2World(mesh.vertices[tris[i + 2]]));
        }
    }

    private Vector3 convert2World(Vector3 src)
    {
        return transform.TransformPoint(src);
    }

    private class SectorMeshCreator
    {
        private float radius;
        private float angleDegree;
        private int segments;

        private Mesh cacheMesh;

        /// <summary>  
        /// 创建一个扇形Mesh  
        /// </summary>  
        /// <param name="radius">扇形半价</param>  
        /// <param name="angleDegree">扇形角度</param>  
        /// <param name="segments">扇形弧线分段数</param>  
        /// <param name="angleDegreePrecision">扇形角度精度（在满足精度范围内，认为是同个角度）</param>  
        /// <param name="radiusPrecision"> 
        /// </param>  
        /// <returns></returns>  
        public Mesh CreateMesh(float radius, float angleDegree, int segments, int angleDegreePrecision, int radiusPrecision)
        {
            if (checkDiff(radius, angleDegree, segments, angleDegreePrecision, radiusPrecision))
            {
                Mesh newMesh = Create(radius, angleDegree, segments);
                if (newMesh != null)
                {
                    cacheMesh = newMesh;
                    this.radius = radius;
                    this.angleDegree = angleDegree;
                    this.segments = segments;
                }
            }
            return cacheMesh;
        }

        private Mesh Create(float radius, float angleDegree, int segments)
        {

            if (segments == 0)
            {
                segments = 1;
#if UNITY_EDITOR
                Debug.Log("segments must be larger than zero.");
#endif
            }

            Mesh mesh = new Mesh();
            Vector3[] vertices = new Vector3[3 + segments - 1];
            vertices[0] = new Vector3(0, 0, 0);

            float angle = Mathf.Deg2Rad * angleDegree;
            float currAngle = angle / 2;
            float deltaAngle = angle / segments;
            for (int i = 1; i < vertices.Length; i++)
            {
                vertices[i] = new Vector3(Mathf.Cos(currAngle) * radius, 0, Mathf.Sin(currAngle) * radius);
                currAngle -= deltaAngle;
            }

            int[] triangles = new int[segments * 3];
            for (int i = 0, vi = 1; i < triangles.Length; i += 3, vi++)
            {
                triangles[i] = 0;
                triangles[i + 1] = vi;
                triangles[i + 2] = vi + 1;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangles;

            Vector2[] uvs = new Vector2[vertices.Length];
            for (int i = 0; i < uvs.Length; i++)
            {
                uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
            }
            mesh.uv = uvs;

            return mesh;
        }

        private bool checkDiff(float radius, float angleDegree, int segments, int angleDegreePrecision, int radiusPrecision)
        {
            return segments != this.segments || (int)((angleDegree - this.angleDegree) * angleDegreePrecision) != 0 ||
                   (int)((radius - this.radius) * radiusPrecision) != 0;
        }
    }
}
