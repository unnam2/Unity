using UnityEngine;
using UnityEngine.UI;

public class UIStart : UIHud
{
    public Dropdown m_difficultyDropdown;
    public InputField m_greaterRifitLevel;
    public InputField m_monsterNum;

    private Image m_greaterRifitLevelImage;

    protected override void OnAwake()
    {
        if (m_difficultyDropdown != null)
        {
            m_difficultyDropdown.options.Clear();

            for (int i = 0; i < Util.GetEnumNum<GameDifficult>(); ++i)
            {
                GameDifficult item = (GameDifficult)i;
                m_difficultyDropdown.options.Add(new Dropdown.OptionData(item.ToString()));
            }
            m_difficultyDropdown.captionText.text = m_difficultyDropdown.options[0].text;
        }
        m_monsterNum.contentType = InputField.ContentType.IntegerNumber;
        m_monsterNum.text = "1";
        m_greaterRifitLevelImage = m_greaterRifitLevel.GetComponent<Image>();
        m_greaterRifitLevel.contentType = InputField.ContentType.IntegerNumber;
        m_greaterRifitLevel.text = "1";
        m_greaterRifitLevelImage.color = Color.gray;
        m_greaterRifitLevel.enabled = false;
    }
    protected override void OnEnableAfterFrame()
    {
        base.OnEnableAfterFrame();
        PoolingManage.Character.GetList<Player>()[0].m_keyInput = false;
    }
    protected override void OnRemove()
    {
        base.OnRemove();
        PoolingManage.Character.GetList<Player>()[0].m_keyInput = false;
    }

    public void OnEventTextMonsterNumChange()
    {
        if (m_monsterNum.text.Length == 0)
        {
            m_monsterNum.text = "0";
            return;
        }
        if (m_monsterNum.text != null && int.Parse(m_monsterNum.text) > 26)
        {
            m_monsterNum.text = "25";
        }
    }

    public void OnEventDropDownChange()
    {
        if (m_difficultyDropdown.captionText.text == GameDifficult.GreaterRift.ToString())
        {
            m_greaterRifitLevel.enabled = true;
            m_greaterRifitLevelImage.color = Color.white;
        }
        else
        {
            m_greaterRifitLevelImage.color = Color.gray;
            m_greaterRifitLevel.enabled = false;
        }
    }

    public void OnEventButtonStart()
    {
        Monster[] list = PoolingManage.Character.GetList<Monster>();
        for (int i = 0; i < list.Length; ++i)
        {
            list[i].Remove();
        }

        GameDifficult difficult = (GameDifficult)System.Enum.Parse(typeof(GameDifficult), m_difficultyDropdown.captionText.text);
        int greaterRiftLevel = int.Parse(m_greaterRifitLevel.text);
        int monsterNum = int.Parse(m_monsterNum.text);

        for (int i = 0; i < monsterNum; ++i)
        {
            Monster monster = (Monster)PoolingManage.Character.Create("Butcher");
            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(-16, 15);
            pos.z = Random.Range(0, 15);
            monster.transform.position = pos;
            GameLevelData.ApplyMonster(monster, difficult, greaterRiftLevel);
        }

        Remove();
    }
}
