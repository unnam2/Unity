//using UnityEngine;
//using System.Collections;

//class NodeEvasiveFire : Node
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

//    private Projectile m_projectile;

//    public override void Execute()
//    {
//        m_projectile = PoolingManage.Instance.Create<Projectile>("EvasiveFire");
//        Vector3 temp = m_parent.m_owner.transform.position;
//        temp.y = 0.5f;
//        m_projectile.transform.position = temp + m_parent.m_owner.transform.forward;

//        Vector3 targetPos;
//        if (m_parent.m_owner.m_data.target != null)
//        {
//            targetPos = m_parent.m_owner.m_data.target.transform.position;
//        }
//        else
//        {
//            targetPos = m_parent.m_owner.m_data.targetPos;
//        }
//        m_projectile.transform.LookAt(targetPos);

//        Vector3 playerPos = m_parent.m_owner.transform.position;

//        const float ANGLE = 15f;
//        const float DISTANCE = 30f;
//        const float DAMAGE_RATIO = 2f;

//        Monster[] monsters = PoolingManage.Instance.GetList<Monster>();

//        for (int i = 0; i < monsters.Length; ++i)
//        {
//            Character enemy = monsters[i];

//            Vector3 dir = enemy.transform.position - playerPos;

//            if (ANGLE > Vector3.Angle(dir, m_parent.m_owner.transform.forward) &&
//                DISTANCE > Vector3.Distance(enemy.transform.position, m_parent.m_owner.transform.position))
//            {
//                float damage = m_parent.m_owner.m_data.damage * DAMAGE_RATIO;
//                enemy.Attacked(damage);
//            }
//        }

//        StartCoroutine(Run());
//    }

//    private IEnumerator Run()
//    {
//        yield return new WaitForSeconds(0.4f);
//        m_projectile.Remove();
//        GoNextNode();
//    }
//}
