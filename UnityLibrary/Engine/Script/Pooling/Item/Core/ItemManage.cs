public enum ItemType { Head, Shoulders, Torso, Wrists, Hands, Waist, Legs, Feet, Amulets, Rings, Off_Hand, One_Handed, Two_Handed }

//enum ItemType       { Head, Shoulders, Torso, Wrists, Hands, Waist, Legs, Feet, Jewelry, Off_Hand, One_Handed, Two_Handed, Ranged }
//enum HeadType       { Helms, SpiritStones, VoodooMasks, WizrdHats }
//enum ShouldersType  { Pauldrons }
//enum TorsoType      { ChestArmor, Cloaks }
//enum WristsType     { Bracers }
//enum HandsType      { Gloves }
//enum WaistType      { Belts, MightyBelts }
//enum LegsType       { Pants }
//enum FeetType       { Boots }
//enum JewelryType    { Amulets, Rings }
//enum Off_HandType   { Shields, CrusaderShields, Mojos, Orbs, Quivers }
//enum One_HandedType { Axes, Daggers, Maces, Spears, Swords, CeremonialKnives, FistWeapons, Flails, MigityWeapons }
//enum Two_HandedType { Axes, Maces, Polearms, Staves, Swords, Daibo, Flails, MightyWeapons }
//enum Ranged         { Bows, Crossbowsm, HandCrossbows, Wands }

//public class ItemManage
//{
//    private Character.Data m_data;
//    private Item[] m_items;

//    public Player.Data Data { get { return m_data; } }

//    public ItemManage()
//    {
//        m_data = new Player.Data();
//        m_items = new Item[Util.GetEnumNum<ItemType>()];
//    }

//    public void Init()
//    {
//        for (int i = 0; i < m_items.Length; ++i)
//        {
//            m_items[i] = null;
//        }
//    }

//    /// <summary>item을 장착하고 해제한 아이템 반환</summary>
//    public Item EquipItem(Item item)
//    {
//        Item temp = null;
//        if (null != m_items[(int)item.m_itemType])
//        {
//            temp = m_items[(int)item.m_itemType];
//        }
//        m_items[(int)item.m_itemType] = item;
//        ItemApply();
//        return temp;
//    }

//    /// <summary>type에 맞는 아이템을 해제하고 해제한 아이템 반환</summary>
//    public Item UnEquipItem(ItemType type)
//    {
//        Item temp = m_items[(int)type];
//        m_items[(int)type] = null;
//        ItemApply();
//        return temp;
//    }

//    private void ItemApply()
//    {
//        m_data = new Player.Data();
//        for (int i = 0; i < m_items.Length; ++i)
//        {
//            if (m_items[i] != null)
//            {

//            }
//        }
//    }
//}
