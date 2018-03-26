using UnityEngine;

public abstract class Item : Pooling
{
    [HideInInspector]
    public ItemType m_itemType;
}
