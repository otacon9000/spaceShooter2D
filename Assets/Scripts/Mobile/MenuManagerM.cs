using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManagerM : MonoBehaviour
{
    [Header("Menu UI")]
    [SerializeField]
    private Text _title;

    private void Start()
    {
        StartCoroutine(TitleFlickerRoutine());
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }


    IEnumerator TitleFlickerRoutine()
    {
        while (true)
        {
            _title.text = "SPACE\nDIVING";
            yield return new WaitForSeconds(0.5f);
            _title.text = "";
            yield return new WaitForSeconds(0.4f);
        }
    }
}
