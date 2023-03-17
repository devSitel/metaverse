using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneController : MonoBehaviour
{
    public string _SceneName;
    public GameObject Camara;
    public void ChangeScene(string newScene)
    {
        StartCoroutine(startCinematic(newScene));
    }
    IEnumerator startCinematic(string newScene)
    {
        Camara.SetActive(true);
        yield return new WaitForSeconds(10);
        LoadBetweenScenes.LoadScene(newScene);
    }
}
