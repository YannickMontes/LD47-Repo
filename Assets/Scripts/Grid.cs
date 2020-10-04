using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
	public int X { get { return m_Xsize; } }
	public int Y { get { return m_Ysize; } }

	public Grid(int x, int y)
	{
		m_Xsize = x;
		m_Ysize = y;
		for (int i = 0; i < m_Ysize; i++)
		{
			List<Cell> cells = new List<Cell>();
			for (int j = 0; j < m_Xsize; j++)
			{
				cells.Add(new Cell());
			}
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

	private int m_Xsize = 10;
	private int m_Ysize = 10;
	private List<List<Cell>> m_grid = new List<List<Cell>>();
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

	private List<GameEntity> m_entityOnCell = new List<GameEntity>();
}