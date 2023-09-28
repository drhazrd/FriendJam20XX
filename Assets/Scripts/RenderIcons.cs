using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderIcons : MonoBehaviour
{
    public Camera renderCam;
    private GameObject[] objectsToRender;

    private void Update()
    {
        //set objectstorender to children of transform
        objectsToRender = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            objectsToRender[i] = transform.GetChild(i).gameObject;
            objectsToRender[i].SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RenderObject());
        }
    }

    IEnumerator RenderObject()
    {
        foreach (GameObject prefab in objectsToRender)
        {
            prefab.SetActive(true);

            // // set camera fov to fill bounds of go
            // Bounds bounds = new Bounds(prefab.transform.position, Vector3.zero);
            // foreach (Renderer renderer in prefab.GetComponentsInChildren<Renderer>())
            // {
            //     bounds.Encapsulate(renderer.bounds);
            // }

            // float distance = Vector3.Distance(bounds.min, bounds.max);
            // float fov = 2 * Mathf.Atan(distance / 2) * Mathf.Rad2Deg;
            // renderCam.fieldOfView = fov;

            Debug.Log("Rendering " + prefab.name);
            //write renderCam to file
            RenderTexture rt = new RenderTexture(256, 256, 24);
            renderCam.targetTexture = rt;
            Texture2D screenShot = new Texture2D(256, 256, TextureFormat.ARGB32, false);
            renderCam.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            renderCam.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);
            prefab.SetActive(false);
            byte[] bytes = screenShot.EncodeToPNG();
            string filename = Application.dataPath + "/Resources/Icons/" + prefab.name + ".png";
            System.IO.File.WriteAllBytes(filename, bytes);
            yield return new WaitForEndOfFrame();
        }
    }
}
