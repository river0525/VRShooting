using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavSwitch : MonoBehaviour
{
    [SerializeField] NavMeshAgent nav;
    // Start is called before the first frame update
    public void NavStart()
    {
        nav.isStopped = false;
    }

    // Update is called once per frame
    public void NavStop()
    {
        nav.isStopped = true;
    }
}
