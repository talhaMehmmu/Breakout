using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUI : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    void Start()
    {
        // levelGenerator = GetComponentInParent<LevelGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonClick()
    {
        levelGenerator.GenerateLevel();
    }
}
