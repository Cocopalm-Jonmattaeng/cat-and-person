using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IFOI : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        for (int i = 1; i < 90; i++) { 
            GetComponent<Image>().color = new Color(0, 0, 0, 3f / (i));
            yield return new WaitForSeconds(0.0333333f);
        }

        GetComponent<Image>().color = new Color(0, 0, 0, 0);

        for (int i = 119; i > 0; i--)
        {
            GetComponent<Image>().color = new Color(0, 0, 0, 3f / (i));
            yield return new WaitForSeconds(0.0333333f);
        }
        GetComponent<Image>().color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Overworld");
    }
}
