//using UnityEngine;
//using System.Collections;

//class NodeGrenade : Node
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

//    Projectile m_projectile;

//    public override void Execute()
//    {
//        m_projectile = PoolingManage.Instance.Create<Projectile>("Grenade");

//        Vector3 targetPos;
//        Vector3 firePos = m_parent.m_owner.transform.position + m_parent.m_owner.transform.forward;

//        if (m_parent.m_owner.m_data.target != null)
//        {
//            targetPos = m_parent.m_owner.m_data.target.transform.position;
//        }
//        else
//        {
//            targetPos = m_parent.m_owner.m_data.targetPos; ;
//        }
//        m_projectile.transform.position = firePos;
//        m_projectile.transform.LookAt(targetPos);

//        Vector3 force = m_projectile.transform.forward;
//        float distance = Vector3.Distance(m_parent.m_owner.transform.position, targetPos) - 1f;
//        force *= distance * 60f;
//        force.y = 200f;
//        m_projectile.m_rigidbody.AddForce(force);
//        m_projectile.m_rigidbody.useGravity = true;

//        firePos.y = 0.5f;
//        m_projectile.transform.position = firePos;

//        m_projectile.m_collisionCallback += M_projectile_m_collisionCallback;

//        StartCoroutine(Run());
//    }

//    private void M_projectile_m_collisionCallback(Character character)
//    {
//        const float DAMAGE_RATIO = 1.60f;

//        float damage = DAMAGE_RATIO * m_parent.m_owner.m_data.damage;

//        if (character is Monster)
//        {
//            character.Attacked(damage);
//            m_projectile.Remove();
//            GoNextNode();
//        }
//    }

//    private IEnumerator Run()
//    {
//        yield return new WaitForSeconds(2f);
//        m_projectile.Remove();
//        GoNextNode();
//    }
//}
