using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yube;

public class Grid
{
	public int X { get { return m_Xsize; } }
	public int Y { get { return m_Ysize; } }

	public Grid(int x, int y, GameObject backgroundBox, GameObject pairBox, GameObject oddBox)
	{
		m_Xsize = x;
		m_Ysize = y;
		int gridCount = 0;
		for (int i = 0; i < m_Ysize; i++)
		{
			List<Cell> cells = new List<Cell>();
			for (int j = 0; j < m_Xsize; j++)
			{
				cells.Add(new Cell());

				GameObject go;
				if (gridCount % 2 == 0)
					go = ResourceManager.Instance.AcquireInstance(pairBox, null);
				else
					go = ResourceManager.Instance.AcquireInstance(oddBox, null);
				m_visuals.Add(go);
				go.transform.position = new Vector3(j, i, 0);
				//bg
				go = ResourceManager.Instance.AcquireInstance(backgroundBox, null);
				go.transform.position = new Vector3(j, i, 0);
				m_visuals.Add(go);
				gridCount++;
			}
			if (m_Xsize % 2 == 0)
				gridCount++;

			m_grid.Add(cells);
		}
	}

	public Cell GetCell(int x, int y)
	{
		if (x >= 0 && x < m_Xsize && y >= 0 && y < m_Ysize)
		{
			return m_grid[y][x];
		}
		return null;
	}

	public Cell GetCell(Vector2 coord)
	{
		int xCoord = Mathf.RoundToInt(coord.x);
		int yCoord = Mathf.RoundToInt(coord.y);
		return GetCell(xCoord, yCoord);
	}

	public void Destroy()
	{
		foreach (List<Cell> cells in m_grid)
		{
			foreach (Cell cell in cells)
			{
				cell.Destroy();
			}
			cells.Clear();
		}
		m_grid.Clear();
		foreach (GameObject visual in m_visuals)
		{
			ResourceManager.Instance.ReleaseInstance(visual);
		}
		m_visuals.Clear();
	}

	private int m_Xsize = 10;
	private int m_Ysize = 10;
	private List<List<Cell>> m_grid = new List<List<Cell>>();
	private List<GameObject> m_visuals = new List<GameObject>();
}

public class Cell
{
	public IReadOnlyList<GameEntity> Entities { get { return m_entityOnCell; } }

	public bool Contains<T>() where T : GameEntity
	{
		foreach (GameEntity entity in m_entityOnCell)
		{
			if (entity is T)
				return true;
		}
		return false;
	}

	public void AddEntity(GameEntity entity)
	{
		m_entityOnCell.Add(entity);
	}

	public void RemoveEntity(GameEntity entity)
	{
		m_entityOnCell.Remove(entity);
	}

	public void Destroy()
	{
		for (int i = m_entityOnCell.Count - 1; i >= 0; i--)
		{
			ResourceManager.Instance.ReleaseInstance(m_entityOnCell[i]);
		}
	}

	private List<GameEntity> m_entityOnCell = new List<GameEntity>();
}