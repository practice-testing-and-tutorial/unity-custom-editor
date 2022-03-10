using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

[CustomEditor(typeof(DatabaseSO))]
public class DatabaseEditor : Editor
{
	private List<DatabaseSO.Ingredient> ingredients;

	private AnimBool debugMode;

	private SerializedObject so;
	private SerializedProperty ingredientsProperty;
	private SerializedProperty toolsProperty;
	private SerializedProperty recipesProperty;

	private DatabaseSO db;

	private int ingredientIndex = 0;
	private int ingredientAmount = 0;

	private int ingredientCounter = 0;
	private int toolCounter = 0;

	private string addIngredientName;
	private string addToolName;
	private string addRecipeName;

	private void OnEnable()
	{
		ingredients = new List<DatabaseSO.Ingredient>();

		debugMode = new AnimBool(false);
		debugMode.valueChanged.AddListener(Repaint);

		so = serializedObject;
		ingredientsProperty = so.FindProperty("ingredients");
		toolsProperty = so.FindProperty("tools");
		recipesProperty = so.FindProperty("recipes");

		db = (DatabaseSO) target;

		ingredientCounter = 0;
		toolCounter = 0;
	}

	public override void OnInspectorGUI()
	{
		HandleDebugField();

		ShowCurrentIngredientsCount();
		ShowCurrentToolsCount();
		ShowCurrentRecipesCount();

		HandleAddIngredient();
		HandleAddTool();
		HandleAddRecipe();

		so.ApplyModifiedProperties();
	}

	private void HandleDebugField()
	{
		debugMode.target =
			EditorGUILayout.ToggleLeft("Show Debug Mode", debugMode.target);

		if (EditorGUILayout.BeginFadeGroup(debugMode.faded))
		{
			DrawDefaultInspector();
			GuiLine(Color.grey);
			GUILayout.Space(12);
		}

		EditorGUILayout.EndFadeGroup();

		GUILayout.Space(12);
	}

	private void ShowCurrentIngredientsCount()
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Current ingredients count: " + db.ingredients.Length);
		EditorGUILayout.EndHorizontal();
		GuiLine(Color.grey);
		GUILayout.Space(12);
	}

	private void ShowCurrentToolsCount()
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Current tools count: " + db.tools.Length);
		EditorGUILayout.EndHorizontal();
		GuiLine(Color.grey);
		GUILayout.Space(12);
	}

	private void ShowCurrentRecipesCount()
	{
		EditorGUILayout.BeginHorizontal();
		GUILayout.Label("Current recipes count: " + db.recipes.Length);
		EditorGUILayout.EndHorizontal();
		GuiLine(Color.grey);
		GUILayout.Space(12);
	}

	private void HandleAddIngredient()
	{
		GUILayout.Label("Add new ingredient", EditorStyles.boldLabel);
		GUILayout.Space(5);

		EditorGUILayout.BeginHorizontal();
		
		addIngredientName = EditorGUILayout.TextField(addIngredientName == string.Empty ? "Name" : addIngredientName);

		if (GUILayout.Button("Add"))
		{
			ingredientsProperty.InsertArrayElementAtIndex(ingredientsProperty.arraySize);
			
			var newIngredient = 
				ingredientsProperty.GetArrayElementAtIndex(ingredientsProperty.arraySize - 1);

			var newIngredientName = newIngredient.FindPropertyRelative("name");

			newIngredientName.stringValue = addIngredientName;

			addIngredientName = String.Empty;
		}

		EditorGUILayout.EndHorizontal();
		GUILayout.Space(5);
		GuiLine(Color.grey);
		GUILayout.Space(12);
	}

	private void HandleAddTool()
	{
		GUILayout.Label("Add new tool", EditorStyles.boldLabel);
		GUILayout.Space(5);

		EditorGUILayout.BeginHorizontal();
		
		addToolName = EditorGUILayout.TextField(addToolName == string.Empty ? "Name" : addToolName);

		if (GUILayout.Button("Add"))
		{
			toolsProperty.InsertArrayElementAtIndex(toolsProperty.arraySize);

			var newTool =
				toolsProperty.GetArrayElementAtIndex(toolsProperty.arraySize - 1);

			var newToolName = newTool.FindPropertyRelative("name");

			newToolName.stringValue = addToolName;

			addToolName = String.Empty;
		}

		EditorGUILayout.EndHorizontal();
		GUILayout.Space(5);
		GuiLine(Color.grey);
		GUILayout.Space(12);
	}

	private void HandleAddRecipe()
	{
		GUILayout.Label("Add new recipe", EditorStyles.boldLabel);
		GUILayout.Space(5);

		addRecipeName = EditorGUILayout.TextField(addRecipeName == string.Empty ? "Recipe name" : addRecipeName);
		GUILayout.Space(5);

		EditorGUILayout.BeginVertical();

		GUILayout.Label("Ingredients:", EditorStyles.boldLabel);

		for (int i = 0; i < ingredientCounter; i++)
		{
			ShowIngredients();
		}

		if (GUILayout.Button("+"))
		{
			ingredientCounter++;

			Debug.Log("new ingredient: " + ingredientCounter);

			// add more ingredient
			// save previous data to list

		}

		GUILayout.Space(5);

		GUILayout.Label("Tools:", EditorStyles.boldLabel);

		for (int i = 0; i < toolCounter; i++)
		{
			ShowIngredients();
		}

		if (GUILayout.Button("+"))
		{
			toolCounter++;

			Debug.Log("new tool: " + toolCounter);
			// add more tool
		}
		GUILayout.Space(5);

		EditorGUILayout.EndVertical();

		GuiLine(Color.grey);

		if (GUILayout.Button("Create recipe"))
		{
			// add recipe
			ingredientCounter = 0;
			toolCounter = 0;
			addRecipeName = String.Empty;
		}
		
		GuiLine(Color.grey);
		GUILayout.Space(12);
	}

	private void ShowIngredients()
	{
		string[] options = { "apple", "pineapple" };

		EditorGUILayout.BeginHorizontal("Box");
		
		if (GUILayout.Button("X"))
		{
			// remove from list
		}

		ingredientIndex = EditorGUILayout.Popup(ingredientIndex, options);
		ingredientAmount = EditorGUILayout.IntField(ingredientAmount);
		EditorGUILayout.EndHorizontal();
	}

	private void GetTools()
	{
		//
	}

	private void GuiLine(Color color, int i_height = 1)
	{
		Rect rect = EditorGUILayout.GetControlRect(false, i_height);
		EditorGUI.DrawRect(rect, color);
	}
}
