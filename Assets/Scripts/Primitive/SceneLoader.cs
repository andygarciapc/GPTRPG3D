using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public string targetSceneName = "Scene2";
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            Debug.Log("loading scene");
            LoadTargetScene();
        }
    }

    public void LoadTargetScene()
    {
        //Debug.Log("loading scene");
        StartCoroutine(LoadLevel());
        //SceneManager.LoadScene(targetSceneName);
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(targetSceneName);
    }
}
