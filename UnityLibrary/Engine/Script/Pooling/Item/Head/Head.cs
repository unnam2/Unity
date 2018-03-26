public class Head : Item
{
    public int ability;
    public int armor;

    protected override void OnStart()
    {
        base.OnStart();
        m_itemType = ItemType.Head;
    }
}
