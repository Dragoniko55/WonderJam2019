using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public float f_waitBeforeLoad;
    public AsyncOperation operation;
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);
            yield return StartCoroutine("AllowOp"); 
        }
    }
    IEnumerator AllowOp()
    {
        yield return new WaitForSeconds(f_waitBeforeLoad); 
        operation.allowSceneActivation = true;
    }

}
