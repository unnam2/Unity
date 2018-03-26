using UnityEngine;
using System.Collections;

public class Effect : Pooling
{
    public bool m_loop = false;
    public float m_playTimeForLoop = 0f; //loop일 경우에 이 시간 이후에 꺼짐, <= 0f 이면 영구 켜짐

    public AudioClip m_audioClip;
    public bool m_3dSound = false;
    [Range(0f, 1f)]
    public float m_volume = 1f;
    public float m_offset = 0f;

    private float m_hideTime;
    private float m_soundTime;
    private float m_particleDuration;
    private float m_particleLifeTime;
    private AudioSource m_audioSource;
    private ParticleSystem[] m_particles;

    protected override void OnAwake()
    {
        const float EFFECT3D = 0.9f;
        const float MAX_DISTANCE = 100f;

        #region Set Sound
        m_soundTime = 0f;
        if (m_audioClip != null)
        {
            m_audioSource = gameObject.AddComponent<AudioSource>();
            m_audioSource.clip = m_audioClip;

            if (m_3dSound == true)
                m_audioSource.spatialBlend = EFFECT3D;

            m_audioSource.volume = m_volume;
            m_audioSource.rolloffMode = AudioRolloffMode.Linear;
            m_audioSource.maxDistance = MAX_DISTANCE;

            m_soundTime = m_audioClip.length;
        }
        #endregion

        #region Set Particle
        m_particles = GetComponentsInChildren<ParticleSystem>();

        m_particleDuration = 0f;
        for (int i = 0; i < m_particles.Length; ++i)
        {
            ParticleSystem.MainModule main = m_particles[i].main;
            if (!main.loop)
                main.loop = m_loop;

            if (m_particleDuration < main.duration)
                m_particleDuration = main.duration;
        }
        m_particleLifeTime = 0f;
        for (int i = 0; i < m_particles.Length; ++i)
        {
            float instance = m_particles[i].main.startLifetime.constant;

            if (m_particleLifeTime < instance)
                m_particleLifeTime = instance;
        }
        #endregion

        m_hideTime = 0f;
        if (m_loop && m_playTimeForLoop > 0f)
            m_hideTime = m_playTimeForLoop;

        if (!m_loop)
            m_hideTime = Mathf.Max(m_soundTime, m_particleDuration);
    }

    protected override void OnStart()
    {
        if (m_audioSource != null)
        {
            m_audioSource.time = m_offset;
            m_audioSource.Play();
        }

        if (m_hideTime > 0f)
            StartCoroutine(Run());
    }

    protected override void OnUpdate()
    {
        if (m_audioSource != null && m_loop && !m_audioSource.isPlaying)
        {
            m_audioSource.time = m_offset;
            m_audioSource.Play();
        }
    }

    protected override void OnRemove()
    {
        StartCoroutine(IRemove());
    }

    private IEnumerator Run()
    {
        if (m_hideTime > 0f)
            yield return new WaitForSeconds(m_hideTime);

        Remove();
    }

    private IEnumerator IRemove()
    {
        if (m_particles.Length > 0)
            m_particles[0].Stop();

        yield return new WaitForSeconds(m_particleLifeTime);
        gameObject.SetActive(false);
    }
}
