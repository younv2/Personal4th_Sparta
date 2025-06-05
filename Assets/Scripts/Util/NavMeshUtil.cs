
using UnityEngine;
using UnityEngine.AI;

public static class NavMeshUtil
{
    public static Vector3 GetRandomPointOnNavMesh()
    {
        var navMesh = NavMesh.CalculateTriangulation();
        int triIndex = Random.Range(0, navMesh.indices.Length / 3) * 3;

        Vector3 a = navMesh.vertices[navMesh.indices[triIndex]];
        Vector3 b = navMesh.vertices[navMesh.indices[triIndex + 1]];
        Vector3 c = navMesh.vertices[navMesh.indices[triIndex + 2]];

        // 삼각형 내 임의의 위치 뽑기
        float r1 = Mathf.Sqrt(Random.value);
        float r2 = Random.value;
        Vector3 point = (1 - r1) * a + r1 * (1 - r2) * b + r1 * r2 * c;

        return point;
    }
    public static bool IsExistNavMesh()
    {
        var triangulation = NavMesh.CalculateTriangulation();

        bool navMeshExists = triangulation.vertices != null &&
                             triangulation.vertices.Length > 0 &&
                             triangulation.indices != null &&
                             triangulation.indices.Length > 0;
        if (!navMeshExists)
        {
            Debug.LogError("NavMesh가 존재하지 않습니다");
            return false;
        }
        return true;
    }

}

