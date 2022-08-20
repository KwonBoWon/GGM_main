using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toNext : MonoBehaviour
{
    public GameObject thisImage;
    public GameObject nextImage;
    // Start is called before the first frame update
    public void ToNex()
    {
        gameObject.SetActive(false);
        thisImage.SetActive(false);
        nextImage.SetActive(true);
    }
}
