using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    public float MaxHp = 100;
    public float hp = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp != 0)
        {
            this.transform.localPosition = new Vector3((-200 + 200 * (hp / MaxHp)), 0.0f, 0.0f);
        }
        else
        {
            this.transform.localPosition = new Vector3(-200, 0.0f, 0.0f);
        }
    }
}
