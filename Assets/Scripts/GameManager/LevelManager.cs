using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    IEnumerator LoadSceneAsync(string sceneName)
    {
        if (SceneManager.GetActiveScene().name == "ChooseWeapon")
        {
            yield break; 
        }

        if (animator != null)
        {
            animator.SetTrigger("StartTransition");
            Debug.Log("transisi jalan");
        }
        
        yield return new WaitForSeconds(1f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Main");

        if (animator != null)
        {
            animator.SetTrigger("EndTransition");
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync("Main"));
    }
}


