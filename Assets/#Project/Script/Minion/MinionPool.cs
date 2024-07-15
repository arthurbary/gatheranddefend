using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPool : MonoBehaviour
{
    protected Stack<MinionPoolMember> Pool = new();
    [Range(1, 100)] protected int InitialBatch = 50;
    [Range(1, 100)] protected int Batch = 10;
    protected GameObject Prefab;


    public void Awake()
    {
        Create(InitialBatch);
    }

    private void Create(int number)
    {
        for (int _ = 0; _ < number; _++)
        {
            GameObject newOne = Instantiate(Prefab);
            newOne.GetComponent<MinionPoolMember>().Pool = this;
            Kill(newOne.GetComponent<MinionPoolMember>());
        }
    }

    public MinionPoolMember Spawn(Vector3 position, Quaternion rotation, bool isEnemy)
    {
        if (Pool.Count == 0)
        {
            Create(Batch);
        }
        MinionPoolMember member = Pool.Pop();
        member.isEnemy = isEnemy;
        Revive(member,position, rotation);
        return member;
    }

    public void Kill(MinionPoolMember member)
    {
        member.gameObject.SetActive(false);
        member.hasBeenInitialized = false;
        Pool.Push(member);
    }

    public void Revive(MinionPoolMember member, Vector3 position, Quaternion rotation){
        member.gameObject.SetActive(true);
        member.transform.position = position;
        member.transform.rotation = rotation;

    }
}
