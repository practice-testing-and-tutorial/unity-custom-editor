using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewDatabase", menuName = "Database", order = 0)]
public class DatabaseSO : ScriptableObject 
{
	[Serializable]
	public struct Ingredient
	{
		public uint id;
		public string name;
	}

	[Serializable]
	public struct Tool
	{
		public uint id;
		public string name;
	}

	public struct IngredientPack
	{
		public Ingredient ingredient;
		public int amount;
	}

	public struct ToolPack
	{
		public Tool tool;
		public int amount;
	}

	[Serializable]
	public struct Recipe
	{
		public string name;
		public IngredientPack[] ingredientPacks;
		public ToolPack[] toolPacks;
	}

	public Ingredient[] ingredients;
	public Tool[] tools;
	public Recipe[] recipes;
}