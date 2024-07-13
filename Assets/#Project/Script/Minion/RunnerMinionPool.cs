using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMinionPool : MonoBehaviour
{
    private Stack<RunnerMinionPoolMember> pool = new();
    [Range(1, 100)][SerializeField] private int initialBatch = 50;
    [Range(1, 100)][SerializeField] private int batch = 10;

    [SerializeField] GameObject prefab;


    public void Awake()
    {
        Create(initialBatch);
    }

    private void Create(int number)
    {
        for (int _ = 0; _ < number; _++)
        {
            GameObject newOne = Instantiate(prefab);
            newOne.GetComponent<RunnerMinionPoolMember>().pool = this;
            Kill(newOne.GetComponent<RunnerMinionPoolMember>());
        }
    }

    public RunnerMinionPoolMember Spawn(Vector3 position, Quaternion rotation, bool isEnemy)
    {
        if (pool.Count == 0)
        {
            Create(batch);
        }
        RunnerMinionPoolMember member = pool.Pop();
        member.isEnemy = isEnemy;
        Revive(member,position, rotation);
        return member;
    }

    public void Kill(RunnerMinionPoolMember member)
    {
        member.gameObject.SetActive(false);
        member.hasBeenInitialized = false;
        pool.Push(member);
    }

    public void Revive(RunnerMinionPoolMember member, Vector3 position, Quaternion rotation){
        member.gameObject.SetActive(true);
        member.transform.position = position;
        member.transform.rotation = rotation;

    }
}
