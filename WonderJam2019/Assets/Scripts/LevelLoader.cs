using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public float f_waitBeforeLoad;
    public AsyncOperation operation;
    public GameObject GO_skip;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));

        Debug.Log("load");
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);
            if(progress == 1.0f)
            {
                Debug.Log("Active");
                GO_skip.SetActive(true);
            }
            yield return null;
        }
        
    }

    


    void Update()
    {
        if (GO_skip.activeSelf)
        {
            if (Input.GetKeyDown("space"))
            {
                operation.allowSceneActivation = true;
            }
        }
    }


}
