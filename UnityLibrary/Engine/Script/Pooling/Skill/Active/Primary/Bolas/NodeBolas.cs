//using UnityEngine;
//using System.Collections;

//class NodeBolas : Node
//{
//    public string input0 = NodeString.INPUT;

//    public string output0 = NodeString.OUTPUT;

//    public override void InitEditor()
//    {
//        m_rect.width = 100f;

//        InitIO(1, 1);

//        m_inputs[0].Set(NodeIO.EditorType.Label, true);

//        m_outputs[0].Set(NodeIO.EditorType.Label, true);
//    }

//    public override void OnUpdateEditorData()
//    {
//        m_inputs[0].IOCheck(ref input0);

//        m_outputs[0].IOCheck(ref output0);
//    }

//    public override void Execute()
//    {
//        StartCoroutine(Run());
//    }

//    const float TIME = 2f;

//    float m_currentTime;
//    Vector3 m_direction;
//    Character m_target;
//    Projectile m_projectile;

//    private IEnumerator Run()
//    {
//        const float SPEED = 20f;

//        m_currentTime = 0f;

//        m_projectile = PoolingManage.Instance.Create<Projectile>("Bolas");
//        Vector3 pos = m_parent.m_owner.transform.position;
//        pos.y = 0.5f;
//        m_projectile.transform.position = pos;
//        m_projectile.m_collisionCallback += CollisionCallback;
//        m_target = null;
//        SetTarget();

//        while (null == m_target && true == m_parent.m_use)
//        {
//            m_projectile.transform.position += m_direction * Time.deltaTime * SPEED;

//            m_currentTime += Time.deltaTime;
//            if (m_currentTime >= TIME)
//            {
//                break;
//            }
//            yield return null;
//        }
//        if (m_target == false)
//        {
//            m_projectile.Remove();
//            GoNextNode();
//        }
//    }

//    private void CollisionCallback(Character character)
//    {
//        if (character is Monster)
//        {
//            m_target = character;
//            m_currentTime = TIME;
//            StartCoroutine(WaitExplosion(1f));
//        }
//    }

//    private void SetTarget()
//    {
//        m_currentTime = 0f;

//        if (null != m_parent.m_owner.m_data.target && null == m_target)
//        {
//            m_direction = m_parent.m_owner.m_data.target.transform.position;
//        }
//        else if (null == m_target)
//        {
//            m_direction = m_parent.m_owner.m_data.targetPos;
//        }
//        else if (null != m_target)
//        {
//            m_direction = m_target.transform.position;
//        }
//        m_projectile.transform.LookAt(m_direction);
//        m_direction = m_direction - m_projectile.transform.position;
//        m_direction.y = 0f;
//        m_direction.Normalize();
//    }

//    IEnumerator WaitExplosion(float time)
//    {
//        m_currentTime = 0f;
//        while (true)
//        {
//            Vector3 temp = m_target.transform.position;
//            temp.y = m_projectile.transform.position.y;
//            m_projectile.transform.position = temp;

//            m_projectile.transform.Rotate(Vector3.up * 200f * Time.deltaTime);
//            if (m_currentTime >= time)
//            {
//                break;
//            }
//            m_currentTime += Time.deltaTime;
//            yield return null;
//        }

//        const float DAMAGE_RATIO = 1.6f;
//        float damage = DAMAGE_RATIO * m_parent.m_owner.m_data.damage;
//        m_target.Attacked(damage);
//        m_projectile.Remove();
//        GoNextNode();
//    }
//}
