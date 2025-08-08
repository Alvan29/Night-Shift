using UnityEngine;

public interface ITask
{
    void OnTaskActivated(string taskName);
    void OnTaskCompleted(string taskName);
}
