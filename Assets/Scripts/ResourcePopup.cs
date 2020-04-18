using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePopup : MonoBehaviour
{
    private float t;
    private float timer = 0;
    private Vector3 startingPos;
    private Vector3 endPos;
    public TextMeshProUGUI text;
    public RawImage imageObj;
    private GameObject ui;
    private float offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = Random.Range(-10, 10);
        ui = GameObject.FindGameObjectWithTag("UI");
        Transform tmp = transform;
        tmp.SetParent(ui.transform);
        tmp.localPosition = Vector3.zero;
        tmp.rotation = ui.transform.rotation;
        startingPos = tmp.position;
        endPos = new Vector3(startingPos.x + offset, startingPos.y + 50f, startingPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        t += Time.deltaTime/0.2f;
        transform.position = Vector3.Lerp(startingPos, endPos, t);
        if (timer > 0.3f)
        {
            Destroy(gameObject);
        }
    }

    public void SetText(int amount)
    {
        text.text = $"+{amount}";
    }

    public void SetIcon(Texture icon)
    {
        imageObj.texture = icon;
    }
}
