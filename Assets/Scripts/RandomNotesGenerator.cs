using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNotesGenerator : MonoBehaviour
{

    public GameObject notesPrefab;
    public float reloadTime = 0.3f;
    public float reloadProgress = 0f;
    private float randomX;
    public System.Random r = new System.Random();
    private float[] xPosition;
    private Color[] notesColor;
    private GameObject newObject;
    private int rand;

    // Start is called before the first frame update
    void Start()
    {
        xPosition = new float[6] {-2.5f, -1.5f, -0.5f, 0.5f, 1.5f, 2.5f};
        notesColor = new Color[6] {Color.yellow, Color.magenta, Color.blue, Color.green, Color.red, Color.cyan};
    }

    // Update is called once per frame
    void Update()
    {
        reloadProgress += Time.deltaTime;
        if(reloadProgress >= reloadTime){
            rand = r.Next(6);
            randomX = xPosition[rand];
            newObject = Instantiate<GameObject>(notesPrefab, new Vector2(randomX, 6), Quaternion.identity);
            newObject.GetComponent<SpriteRenderer>().color = notesColor[rand];
            reloadProgress = 0f;
        }
    }
}
