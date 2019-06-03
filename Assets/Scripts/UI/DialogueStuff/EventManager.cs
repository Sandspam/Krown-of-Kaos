using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject> { }

public class EventManager : MonoBehaviour
{
    //Sets a dictionary that you can add different events to
    private Dictionary<string, UnityEvent<GameObject>> eventDictionary;

    //References the event manager (The script itself)
    private static EventManager eventManager;

    /// <summary>
    /// Singleton pattern implementation, prevents multiple triggers and allows static reference to non-static class.
    /// </summary>

    //Handles the events
    public static EventManager instance
    {
        get
        {
            //If there is no event manager script set...
            if (!eventManager)
            {
                //Then the event manager sets itself to the gameobject it's attached to
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                //If there is no event manager script...
                if (!eventManager)
                {
                    //Tells the console there isn't any event manager scripts attached to any game objects
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    //If the event manager script IS set, run Initialize
                    eventManager.Init();
                }
            }

            //Returns eventManager
            return eventManager;
        }
    }

    /// <summary>
    /// Initialize the event dictionary to an empty dictionary.
    /// </summary>
    void Init()
    {
        //If the event dictionary is not existing...
        if (eventDictionary == null)
        {
            //Set eventDictionary to a new event dictionary
            eventDictionary = new Dictionary<string, UnityEvent<GameObject>>();
        }
    }

    /// <summary>
    /// Subscribe a class to an Event of name contained in eventName.
    /// </summary>
    /// <param name="eventName">Name of the event to listen for.</param>
    /// <param name="listener">Action variable from the subscribing class (must not be null).</param>
    public static void StartListening(string eventName, UnityAction<GameObject> listener)
    {
        //Sets the current event to null?
        UnityEvent<GameObject> thisEvent = null;

        //If the instance is trying to get an event...?
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            //Then it adds a listener to that event?
            thisEvent.AddListener(listener);
        }
        else
        {
            //If the event does not exist then set "thisEvent" to the new GameObject event?
            thisEvent = new GameObjectEvent();
            //
            thisEvent.AddListener(listener);
            //Adds the event to the event manager
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// Unsubscribe a specific listener from an Event.
    /// </summary>
    /// <param name="eventName">Name of the event to stop listening for.</param>
    /// <param name="listener">Action variable from the subscribing class (must not be null).</param>
    public static void StopListening(string eventName, UnityAction<GameObject> listener)
    {
        //If the event manager exists...
        if (eventManager == null) { return; }
        //The listeners event it's listening to is set to nothing
        UnityEvent<GameObject> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            //Removes the listener from that event
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// Execute an event by name.
    /// </summary>
    /// <param name="eventName">Name of the event to trigger.</param>
    /// <param name="eventObject">GameObject to be passed to listeners' handler function (must not be null).</param>

    //Executes the event 
    public static void TriggerEvent(string eventName, GameObject eventObject)
    {
        UnityEvent<GameObject> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventObject);
        }
    }
}
