﻿using UnityEngine;

using System.Collections;



public class DestroyBlocks : MonoBehaviour

{

    void OnCollisionEnter(Collision col)

    {

        if (col.gameObject.name == "destroyBlock")    //destroy block if collision

        {

            Destroy(col.gameObject);   //destroy activated

        }

    }

}

