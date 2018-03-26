using UnityEngine;
using System.Collections;

public class Player : Character
{
    [System.Serializable]
    public struct Status
    {
        public float hp;
        public float damage;
        public float moveSpeed;
        public float resource;
    }

    public Status m_status;

    private readonly Vector3 CAMERAPOS = new Vector3(0f, 3f, -2f) * 5f;
    private const string TEMP_TARGET = "Target";

    [HideInInspector]
    public float m_attackSpeedForSecond;
    [HideInInspector]
    public bool m_keyInput;
    [HideInInspector]
    public float m_resource;

    private bool m_isAction;
    private Transform m_temporaryTarget;

    protected override void OnStart()
    {
        base.OnStart();
        m_isAction = false;
        m_hp = m_status.hp;
        m_damage = m_status.damage;
        m_moveSpeed = m_status.moveSpeed;
        m_attackSpeedForSecond = 2f;

        m_resource = m_status.resource;

        GameObject temp = new GameObject();
        temp.name = TEMP_TARGET;
        m_temporaryTarget = temp.transform;

        //PoolingManage.Instance.Create<UIHud>("Status");
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (Camera.main != null)
        {
            Camera.main.transform.position = CAMERAPOS + transform.position;
        }

        if (Input.anyKey && m_keyInput == true)
        {
            InputRay();
        }
        InputProcessMenu();

        if (m_resource < m_status.resource)
        {
            m_resource += Time.deltaTime * 5f;
        }
        if (m_resource > m_status.resource)
        {
            m_resource = m_status.resource;
        }
        TestCode();
    }

    private void TestCode()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.LoadScene(GameManager.SceneString.Title);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Projectile pro = PoolingManage.Projectile.Create("Pro", transform.position);
            pro.Set(5f, this, ProjectileAction.Hit);
            pro.Move.Straight(10f, transform.forward);
        }
    }

    protected override void OnRemove()
    {
        Destroy(m_temporaryTarget);
    }

    private void InputRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, Camera.main.farClipPlane))
        {
            Collider collider = hit.collider;
            if (collider != null)
            {
                if (Input.anyKeyDown && Input.GetKeyDown(KeyCode.BackQuote) == false && Input.GetKeyDown(KeyCode.LeftShift) == false)
                {
                    Monster monster = collider.GetComponent<Monster>();
                    if (monster != null)
                    {
                        m_target = monster.transform;
                    }
                    if (monster == null)
                    {
                        m_target = m_temporaryTarget;
                    }
                }
                if (m_target != m_temporaryTarget)
                {
                    InputMonster();
                }
                if (m_target == m_temporaryTarget)
                {
                    InputGround(hit.point);
                }
            }
        }
    }

    private void InputMonster()
    {
        if (Input.GetKey(KeyCode.Mouse0) && m_isAction == false)
        {
            Fire(0);
        }
    }
    private void InputGround(Vector3 point)
    {
        bool fix = false;
        if (Input.GetKey(KeyCode.BackQuote) || Input.GetKey(KeyCode.LeftShift))
        {
            fix = true;
        }

        if (fix == false && Input.GetKey(KeyCode.Mouse0) && m_isAction == false)
        {
            MoveStart(point);
        }

        if (fix == true && Input.GetKey(KeyCode.Mouse0) && m_isAction == false)
        {
            m_temporaryTarget.position = point;
            Fire(0);
        }
    }

    private void InputProcessMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIHud[] list = PoolingManage.UIHud.GetList<UIStart>();
            if (list.Length == 0)
            {
                PoolingManage.UIHud.Create("Start");
            }
            else
            {
                list[0].Remove();
            }
        }
    }

    private void Fire(int num)
    {
        if (true == m_isAction || m_skillManage.m_skills[num].m_data.resource > m_resource)
        {
            return;
        }
        if (null != m_target)
        {
            transform.LookAt(m_target.position);
        }
        m_resource -= m_skillManage.m_skills[num].m_data.resource;
        m_skillManage.Fire(num);
        Arrive();
        m_isAction = true;
        StartCoroutine(WaitTime(1 / m_attackSpeedForSecond));
    }

    private IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        m_isAction = false;
    }
}
