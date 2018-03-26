using UnityEngine;
using UnityEngine.EventSystems;

public class PoolingManage : MonoSingleTon<PoolingManage>
{
    private const int MAX_BASE_NUM = 5;

    public static PoolingList<UIHud> UIHud;
    public static PoolingList<UIWorld> UIWorld;
    public static PoolingList<Effect> Effect;
    public static PoolingList<Character> Character;
    public static PoolingList<Projectile> Projectile;
    public static PoolingList<Pooling> Pooling;

    public static bool IsLoad;

    private void Awake()
    {
        gameObject.AddComponent<EventSystem>();
        gameObject.AddComponent<StandaloneInputModule>();

        UIHud = new PoolingList<UIHud>();
        UIHud.SetTransform(transform);

        UIWorld = new PoolingList<UIWorld>();
        UIWorld.SetTransform(transform);

        Character = new PoolingList<Character>();
        Character.SetTransform(transform);

        Effect = new PoolingList<Effect>();
        Effect.SetTransform(transform);

        Projectile = new PoolingList<Projectile>();
        Projectile.SetTransform(transform);

        Pooling = new PoolingList<Pooling>();
        Pooling.SetTransform(transform);

        PreLoad();
    }

    public void Init() { }

    public static void PreLoad()
    {
        IsLoad = true;

        {
            Pooling[] list = Resources.LoadAll<UIHud>(typeof(UIHud).Name);
            for (int i = 0; i < list.Length; ++i)
            {
                list[i] = UIHud.Create(list[i].name);
                list[i].gameObject.SetActive(false);
            }
        }
        {
            Pooling[] list = Resources.LoadAll<UIWorld>(typeof(UIWorld).Name);
            for (int i = 0; i < list.Length; ++i)
            {
                list[i] = UIWorld.Create(list[i].name);
                list[i].gameObject.SetActive(false);
            }
        }
        {
            Pooling[] list = Resources.LoadAll<Effect>(typeof(Effect).Name);
            for (int i = 0; i < list.Length; ++i)
            {
                list[i] = Effect.Create(list[i].name);
                list[i].gameObject.SetActive(false);
            }
        }
        {
            Pooling[] list = Resources.LoadAll<Character>(typeof(Character).Name);
            for (int i = 0; i < list.Length; ++i)
            {
                list[i] = Character.Create(list[i].name);
                list[i].gameObject.SetActive(false);
            }
        }
        {
            Pooling[] list = Resources.LoadAll<Projectile>(typeof(Projectile).Name);
            for (int i = 0; i < list.Length; ++i)
            {
                list[i] = Projectile.Create(list[i].name);
                list[i].gameObject.SetActive(false);
            }
        }

        IsLoad = false;
    }

    public static void ClearAll()
    {
        UIHud.Clear();
        UIWorld.Clear();
        Effect.Clear();
        Character.Clear();
        Projectile.Clear();
        Pooling.Clear();
    }
}
