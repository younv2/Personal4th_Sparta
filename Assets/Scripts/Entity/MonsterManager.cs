using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoSingleton<MonsterManager>
{
    private List<Monster> monsters = new();

    public void Register(Monster monster)
    {
        monsters.Add(monster);
    }

    public void Unregister(Monster monster)
    {
        monsters.Remove(monster);
    }

    public Monster GetNearestMonster(Vector3 pos)
    {
        Monster nearest = null;
        float closest = float.MaxValue;

        foreach (var monster in monsters)
        {
            if (monster.IsDead) continue;
            float dist = (monster.transform.position - pos).sqrMagnitude;
            if (dist < closest)
            {
                closest = dist;
                nearest = monster;
            }
        }

        return nearest;
    }
    public int GetMonsterCount()
    {
        return monsters.Count;
    }
}