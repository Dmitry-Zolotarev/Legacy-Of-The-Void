using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/EnemyDatabase")]
public class EnemyDatabase : ScriptableObject
{
    public List<EnemyData> enemies;
}