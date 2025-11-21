using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    //Inspector 창에서 이펙트 설정을 쉽게 관리하기 위한 클래스
    [System.Serializable]
    public class EffectConfig
    {
        public string id; //이펙트 이름 : key 값
        public GameObject prefab; //이펙트
        public int preloadCount; //미리 만들어둘 개수
    }

    [SerializeField]
    List<EffectConfig> configs = new List<EffectConfig>();

    //예> pools["Fire"] = 파이어 이펙트가 들어있는 풀
    private Dictionary<string, EffectPool> pools = new Dictionary<string, EffectPool>();
    //EffectManager.Instance.Play(firePrefab, position);

    //인스펙터에서 이렇게 정리한다고 생각하면?
    //"Fire" -> fireVFX.prefab
    //"Explosion" -> Explosion.prefab

    //EffectManager.Instance.Play("fire", position);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializePools();
    }

    private void InitializePools()
    {
        pools.Clear();

        foreach (var cfg in configs)
        {
            if (cfg == null || cfg.prefab == null || string.IsNullOrEmpty(cfg.id))
            {
                continue;
            }

            pools[cfg.id] = new EffectPool(cfg.prefab, cfg.preloadCount, transform);
        }
    }

    public GameObject Play(string id, Vector3 position)
    {
        return Play(id, position, Quaternion.identity);
    }

    public GameObject Play(string id, Vector3 position, Quaternion rotation)
    {
        EffectPool pool;
        if (!pools.TryGetValue(id, out pool))
        {
            return null;
        }

        GameObject instance = pool.Get(position, rotation);

        var systems = instance.GetComponentsInChildren<ParticleSystem>(true);

        ResetAndPlay(systems);

        StartCoroutine(ReturnEffectCo(id, instance, systems));

        return instance;
    }

    //파티클 초기화 재생
    private void ResetAndPlay(ParticleSystem[] systems)
    {
        foreach (var ps in systems)
        {
            if (ps != null) continue;
            ps.Clear(true);
            ps.Play(true);
        }
    }

    IEnumerator ReturnEffectCo(string id, GameObject go, ParticleSystem[] systems)
    {
        var wfs = new WaitForSeconds(2.0f);
        if (systems.Length == 0)
        {
            yield return wfs;
        }
        else
        {
            //모든 파티클이 꺼질때 까지 기다리는
            bool anyAlive = false;
            while (true)
            {
                foreach (var ps in systems)
                {
                    if (ps != null && ps.IsAlive(true))
                    {
                        anyAlive = true;
                        break;
                    }
                }

                if (!anyAlive)
                {
                    break;
                }
                yield return null;
            }
            pools[id].ReturnPool(go);
        }
    }
}
