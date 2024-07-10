using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularMinionPoolMember : Minion
{
    public RegularMinionPool pool;
    private void OnBecameInvisible(){
        //pool.Kill(this);
    }
}
