using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public int ScaleCursor = 64;
    public Texture2D cursorMano, cursorNormal;
    Texture2D cursorActivo;
    //public Image mira; // Referencia al objeto de la mira
    void Start()
    {
        Cursor.visible = false;
        CambiarCursor("normal");
    }

    public void CambiarCursor(string tipoCursor)
    {
        if (tipoCursor == "normal")
        {
            cursorActivo = cursorNormal;
        }
        if (tipoCursor == "mano")
        {
            cursorActivo = cursorMano;
        }

    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Input.mousePosition.x + -15.0f, Screen.height - Input.mousePosition.y + 10.0f, ScaleCursor, ScaleCursor), cursorActivo);
    }
}
