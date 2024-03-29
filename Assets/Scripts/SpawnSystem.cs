using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject template;
    [SerializeField] private List<GameObject> spawnList;

    public const string NUM_SPAWNS_KEY = "NUM_SPAWNS_KEY";

    // Start is called before the first frame update
    void Start()
    {
        this.template.SetActive(false);
        EventBroadcaster.Instance.AddObserver(EventNames.ON_SPAWN_BUTTON_CLICKED, this.OnSpawnEvent);
        EventBroadcaster.Instance.AddObserver(EventNames.ON_CLEAR_BUTTON_CLICKED, this.OnClearEvent);
    }


    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_SPAWN_BUTTON_CLICKED);
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_CLEAR_BUTTON_CLICKED);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnSpawnEvent(Parameters parameters)
    {
        int nSpawns = parameters.GetIntExtra(NUM_SPAWNS_KEY, 1);

        for (int i=0; i<nSpawns; i++)
        {
            GameObject copy = GameObject.Instantiate(template, this.transform);
            copy.SetActive(true);
            spawnList.Add(copy);
        }
    }

    public void OnClearEvent()
    {
        if (spawnList.Count > 0)
        {
            for (int i = 0; i < spawnList.Count; i++)
            {
                Destroy(spawnList[i]);
            }
            this.spawnList.Clear();
        }
        else
        {
            Debug.Log("Empty Spawn List");
        }
    }
}
