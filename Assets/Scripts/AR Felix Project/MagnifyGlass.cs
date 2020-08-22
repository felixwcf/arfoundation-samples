﻿using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class MagnifyGlass : MonoBehaviour
{
    private Camera magnifyCamera;
    private GameObject magnifyBorders;
    private LineRenderer LeftBorder, RightBorder, TopBorder, BottomBorder; // Reference for lines of magnify glass borders
    private float MGOX, MG0Y; // Magnify Glass Origin X and Y position
    private float MGWidth = Screen.width / 2.5f, MGHeight = Screen.width / 2.5f; // Magnify glass width and height
    private Vector3 mousePos;

    private bool isForARDemo;   // Custom made
    private bool isMagnifyCamReady;

    [SerializeField] Text testText;

    void Start()
    {
        isForARDemo = true; // For this AR demo
        createMagnifyGlass();
    }
    void Update()
    {
        if (isMagnifyCamReady)
        {
            // Following lines set the camera's pixelRect and camera position at mouse position
            magnifyCamera.pixelRect = new Rect(Input.mousePosition.x - MGWidth / 2.0f, Input.mousePosition.y - MGHeight / 2.0f, MGWidth, MGHeight);
            mousePos = getWorldPosition(Input.mousePosition);

            if (isForARDemo)
            {
                magnifyCamera.transform.rotation = Camera.main.transform.rotation;
                magnifyCamera.transform.position = Camera.main.transform.position;
                mousePos.y = Camera.main.transform.position.y + 2.7f;
                mousePos.x += 0.2f;
            }
            else
            {
                mousePos.y += 5.7f;
            }

            magnifyCamera.transform.position = mousePos;

            mousePos.z = 0;
            if (magnifyBorders) magnifyBorders.transform.position = mousePos;

            // FOR DEBUG PURPOSE
            //if (magnifyCamera && Camera.main)
            //{
            //    Debug.Log("Main: " + Camera.main.transform + " | mousePos:" + magnifyCamera.transform);
            //    testText.text = "Main:" + Camera.main.transform.position + "|mousePos:" + magnifyCamera.transform.position;
            //}
        }
    }

    // Following method creates MagnifyGlass
    private void createMagnifyGlass()
    {
        GameObject camera = new GameObject("MagnifyCamera");
        MGOX = Screen.width / 2f - MGWidth / 2f;
        MG0Y = Screen.height / 2f - MGHeight / 2f;
        magnifyCamera = camera.AddComponent<Camera>();
        magnifyCamera.nearClipPlane = 0.3f;
        magnifyCamera.farClipPlane = 100f;
        magnifyCamera.pixelRect = new Rect(MGOX, MG0Y, MGWidth, MGHeight);
        magnifyCamera.transform.position = new Vector3(0, 0, 0);
        
        if (Camera.main.orthographic)
        {
            magnifyCamera.orthographic = true;
            magnifyCamera.orthographicSize = Camera.main.orthographicSize / 5.0f;//+ 1.0f;
            createBordersForMagniyGlass();
        }
        else
        {
            if (isForARDemo)
            {
                magnifyCamera.transform.position = Camera.main.transform.position;
                magnifyCamera.transform.rotation = Camera.main.transform.rotation;
                //magnifyCamera.transform.parent = Camera.main.transform;
            }
            magnifyCamera.orthographic = false;
            magnifyCamera.fieldOfView = Camera.main.fieldOfView / 10.0f;//3.0f;
        }

        isMagnifyCamReady = true;
    }

    // Following method sets border of MagnifyGlass
    private void createBordersForMagniyGlass()
    {
        magnifyBorders = new GameObject();
        LeftBorder = getLine();
        LeftBorder.positionCount = 2;
        LeftBorder.SetPosition(0, new Vector3(getWorldPosition(new Vector3(MGOX, MG0Y, 0)).x, getWorldPosition(new Vector3(MGOX, MG0Y, 0)).y - 0.1f, -1));
        LeftBorder.SetPosition(1, new Vector3(getWorldPosition(new Vector3(MGOX, MG0Y + MGHeight, 0)).x, getWorldPosition(new Vector3(MGOX, MG0Y + MGHeight, 0)).y + 0.1f, -1));
        LeftBorder.transform.parent = magnifyBorders.transform;
        TopBorder = getLine();
        TopBorder.positionCount = 2;
        TopBorder.SetPosition(0, new Vector3(getWorldPosition(new Vector3(MGOX, MG0Y + MGHeight, 0)).x, getWorldPosition(new Vector3(MGOX, MG0Y + MGHeight, 0)).y, -1));
        TopBorder.SetPosition(1, new Vector3(getWorldPosition(new Vector3(MGOX + MGWidth, MG0Y + MGHeight, 0)).x, getWorldPosition(new Vector3(MGOX + MGWidth, MG0Y + MGHeight, 0)).y, -1));
        TopBorder.transform.parent = magnifyBorders.transform;
        RightBorder = getLine();
        RightBorder.positionCount = 2;
        RightBorder.SetPosition(0, new Vector3(getWorldPosition(new Vector3(MGOX + MGWidth, MG0Y + MGWidth, 0)).x, getWorldPosition(new Vector3(MGOX + MGWidth, MG0Y + MGWidth, 0)).y + 0.1f, -1));
        RightBorder.SetPosition(1, new Vector3(getWorldPosition(new Vector3(MGOX + MGWidth, MG0Y, 0)).x, getWorldPosition(new Vector3(MGOX + MGWidth, MG0Y, 0)).y - 0.1f, -1));
        RightBorder.transform.parent = magnifyBorders.transform;
        BottomBorder = getLine();
        BottomBorder.positionCount = 2;
        BottomBorder.SetPosition(0, new Vector3(getWorldPosition(new Vector3(MGOX + MGWidth, MG0Y, 0)).x, getWorldPosition(new Vector3(MGOX + MGWidth, MG0Y, 0)).y, -1));
        BottomBorder.SetPosition(1, new Vector3(getWorldPosition(new Vector3(MGOX, MG0Y, 0)).x, getWorldPosition(new Vector3(MGOX, MG0Y, 0)).y, -1));
        BottomBorder.transform.parent = magnifyBorders.transform;
    }

    // Following method creates new line for MagnifyGlass's border
    private LineRenderer getLine()
    {
        LineRenderer line = new GameObject("Line").AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Diffuse"));
        line.positionCount = 2;
        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
        line.startColor = Color.black;
        line.endColor = Color.black;
        line.useWorldSpace = false;
        return line;
    }
    private void setLine(LineRenderer line)
    {
        line.material = new Material(Shader.Find("Diffuse"));
        line.positionCount = 2;
        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
        line.startColor = Color.black;
        line.endColor = Color.black;
        line.useWorldSpace = false;
    }

    // Following method calculates world's point from screen point as per camera's projection type
    public Vector3 getWorldPosition(Vector3 screenPos)
    {
        Vector3 worldPos;
        if (Camera.main.orthographic)
        {
            worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            worldPos.z = Camera.main.transform.position.z;
        }
        else
        {
            worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Camera.main.transform.position.z));
            worldPos.x *= -1;
            worldPos.y *= -1;
        }
        return worldPos;
    }
}