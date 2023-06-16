using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(Gameover());
    }

    private IEnumerator Gameover()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("SampleScene");
    }
}
