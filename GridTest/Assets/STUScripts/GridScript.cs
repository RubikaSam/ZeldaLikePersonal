﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridScript : MonoBehaviour
{
    public int width = 6;
    public int height = 6;

    public Color defaultColor = Color.white;
    public Color touchedColor = Color.blue;

    public SquareCellScript cellPrefab;
    public Text cellLabelPrefab;

    SquareCellScript[] cells;

    Canvas gridCanvas;
    CellMesh cellMesh;

    public PlayerController controller;

    void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        cellMesh = GetComponentInChildren<CellMesh>();

        cells = new SquareCellScript[height * width];

        for (int y = 0, i = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, y, i++);
            }
        }
    }

    public void Start()
    {
        cellMesh.Triangulate(cells);
    }

    public void CreateCell(int x, int y, int i)
    {
        Vector3 position;
        position.x = x;
        position.z = 0f;
        position.y = y;

        SquareCellScript cell = cells[i] = Instantiate<SquareCellScript>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = CellCoords.FromCoordinates (x, y);
        cell.color = defaultColor;

        if (x > 0)
        {
            cell.SetNeighbor(CellDirection.W, cells[i - 1]);
        }

        if (y > 0)
        {
            cell.SetNeighbor(CellDirection.S, cells[i - width]);
        }

            Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition =
            new Vector2(position.x, position.y);
        label.text = x.ToString() + "\n" + y.ToString();

        
    }

    #region GetPositionThroughRaycast
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        int layerMaskCells = LayerMask.GetMask("Cells");

        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit, layerMaskCells))
        {
            TouchCell(hit.point);
        }
    }

    //Converts touch position to cell coordinates via the CellCoords script
    void TouchCell(Vector3 position)
    {
        position = transform.InverseTransformPoint(position);
        CellCoords coordinates = CellCoords.FromPosition(position);
        Debug.Log("contact at " + coordinates.ToString());
        int index = coordinates.X + coordinates.Y * width;
        SquareCellScript cell = cells[index];
        cell.color = touchedColor;
    }
    #endregion

}
