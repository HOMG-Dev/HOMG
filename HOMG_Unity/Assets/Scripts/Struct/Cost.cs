using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Cost
{
    public readonly int civilianFactory;
    public readonly int militaryFactory;

    public Cost(int civilianFactory, int militaryFactory)
    {
        this.civilianFactory = civilianFactory;
        this.militaryFactory = militaryFactory;
    }

    public static Cost operator + (Cost value1,Cost value2)
    {
        return new Cost(value1.civilianFactory + value2.civilianFactory,value1.militaryFactory + value2.militaryFactory);
    }
}
