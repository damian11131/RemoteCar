using System;
using System.Collections;
using UnityEngine;

public class IntervalExecuter : MonoBehaviour
{
    Action [] methodsToExecute;
    uint intervalInSeconds;
    bool keepExecuting;
    #region Singleton
    public static IntervalExecuter instance;
    #endregion

    public void Awake()
    {
        instance = this;
    }

    public void StartExecuting(Action[] methodsToExecute, uint intervalInSeconds)
    {
        this.methodsToExecute = methodsToExecute;
        this.intervalInSeconds = intervalInSeconds;
        keepExecuting = true;
        StartCoroutine(Execute());
    }

    IEnumerator Execute()
    {
        if (keepExecuting)
        {
            foreach (Action method in methodsToExecute)
            {
                method();
            }
            yield return new WaitForSeconds(intervalInSeconds);
            StartCoroutine(Execute());
        }
        else
        {
            yield return null;
        }
        
       
    }

    public void StopExecuting()
    {
        keepExecuting = false;
    }
}