using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Books : MonoBehaviour
{
    public Image image; 
    bool open = false;
    // Start is called before the first frame update
    void Start()
    {
       this.image = GetComponent<Image>();
       this.image.enabled = false;
    }

    // Update is called once per frame
    
    public void Book() {
        if (open == false) {
            this.image.enabled = true;
            open = true;
        }
        else{
            this.image.enabled = false;
            open = false;
        }
    }
}
