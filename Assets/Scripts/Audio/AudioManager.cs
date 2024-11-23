using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;

    private EventInstance musicEventInstance;
    private bool isInitialized;
    public static AudioManager instance { get; private set; }

    private int currentSceneIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();
    }

    private void Start()
    {
        InitializeMusic(FMODEvents.instance.Music);
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && currentSceneIndex != 0)
        {
            currentSceneIndex = 0;
            AudioManager.instance.SetMusicParameter("SceneName", 0);
            Debug.Log("AudioManager set music parameter to 0");
        }

        if (SceneManager.GetActiveScene().buildIndex == 2 && currentSceneIndex != 2)
        {
            currentSceneIndex = 2;
            AudioManager.instance.SetMusicParameter("SceneName", 1);
            Debug.Log("AudioManager set music parameter to 1");
        }
    }

    public void InitializeMusic(EventReference musicEventReference)
    {
        if (isInitialized == false)
        {
            musicEventInstance = CreateEventInstance(musicEventReference);
            musicEventInstance.start();
            isInitialized = true;
        }
    }

    public void SetMusicParameter(string parameterName, float parameterValue)
    {
        musicEventInstance.setParameterByName(parameterName, parameterValue);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;
    }

    private void CleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }

        foreach (StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }
}
