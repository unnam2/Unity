using UnityEngine;

[System.Serializable]
public class SkillManage
{
    class Data
    {
        public int fireNum = 1;
        public bool ready = true;
        public float cooldown = 0f;
    }

    public SkillActive[] m_skills;

    private Data[] m_skillDatas;
    private Character m_owner;

    public void Init(Character owner)
    {
        m_owner = owner;

        m_skillDatas = new Data[m_skills.Length];
        for (int i = 0; i < m_skills.Length; ++i)
        {
            if (null == m_skills[i])
            {
                Debug.LogError("skill is empty");
                return;
            }
            m_skillDatas[i] = new Data();
            m_skillDatas[i].fireNum = m_skills[i].m_data.fireNum;
        }
    }

    public bool GetReady(int num)
    {
        if (m_skills.Length <= num)
        {
            return false;
        }
        return m_skillDatas[num].ready;
    }

    public float MinRange()
    {
        float min = float.MaxValue;

        for (int i = 0; i < m_skills.Length; ++i)
        {
            if (null == m_skills[i])
            {
                continue;
            }

            if (min > m_skills[i].m_data.range)
            {
                min = m_skills[i].m_data.range;
            }
        }
        return min;
    }

    public void Fire(int num)
    {
        if (m_skills.Length <= num || null == m_skills[num])
        {
            return;
        }

        if (true == m_skillDatas[num].ready)
        {
            //SkillActive skill = PoolingManage.Instance.Create(m_skills[num]);
            //skill.m_owner = m_owner;
            --m_skillDatas[num].fireNum;
            if (0 == m_skillDatas[num].fireNum)
            {
                m_skillDatas[num].ready = false;
            }
        }
        else
        {
            //스킬 대기중
        }
    }

    public void OnUpdate()
    {
        if (m_skills.Length == 0)
        {
            return;
        }
        for (int i = 0; i < m_skills.Length; ++i)
        {
            if (false == m_skills[i].m_data.charge)
            {
                if (false == m_skillDatas[i].ready)
                {
                    m_skillDatas[i].cooldown += Time.deltaTime;

                    if (m_skillDatas[i].cooldown >= m_skills[i].m_data.cooldown)
                    {
                        m_skillDatas[i].cooldown = 0f;
                        m_skillDatas[i].ready = true;
                        m_skillDatas[i].fireNum = m_skills[i].m_data.fireNum;
                    }
                }
            }
            else
            {
                if (m_skillDatas[i].fireNum < m_skills[i].m_data.fireNum)
                {
                    m_skillDatas[i].cooldown += Time.deltaTime;

                    if (m_skillDatas[i].cooldown >= m_skills[i].m_data.cooldown)
                    {
                        ++m_skillDatas[i].fireNum;
                        m_skillDatas[i].cooldown = 0f;
                    }
                    if (m_skillDatas[i].fireNum > 0)
                    {
                        m_skillDatas[i].ready = true;
                    }
                }
            }
        }
    }
}
