using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using OculusSampleFramework;

public class SphereDebugger : MonoBehaviour
{

	[SerializeField] private GameObject _startStopButton = null;
	[SerializeField] private SelectionCylinder _selectionCylinder = null;
	private InteractableTool _toolInteractingWithMe = null;
	private bool selected = false;

	private void Awake()
	{
		Assert.IsNotNull(_startStopButton);
		Assert.IsNotNull(_selectionCylinder);

	}

	private void OnEnable()
	{
		_startStopButton.GetComponent<Interactable>().InteractableStateChanged.AddListener(StartStopStateChanged);
	}

	private void OnDisable()
	{
		if (_startStopButton != null)
		{
			_startStopButton.GetComponent<Interactable>().InteractableStateChanged.RemoveListener(StartStopStateChanged);
		}
	}

	private void StartStopStateChanged(InteractableStateArgs obj)
	{
		bool inActionState = obj.NewInteractableState == InteractableState.ActionState;
		if (inActionState)
		{
			ButtonPress();
		}
		_toolInteractingWithMe = obj.NewInteractableState > InteractableState.Default ?
		  obj.Tool : null;
	}

	private void Update()
	{
		if (_toolInteractingWithMe == null)
		{
			_selectionCylinder.CurrSelectionState = SelectionCylinder.SelectionState.Off;
		}
		else
		{
			_selectionCylinder.CurrSelectionState = (
			  _toolInteractingWithMe.ToolInputState == ToolInputState.PrimaryInputDown ||
			  _toolInteractingWithMe.ToolInputState == ToolInputState.PrimaryInputDownStay)
			  ? SelectionCylinder.SelectionState.Highlighted
			  : SelectionCylinder.SelectionState.Selected;
		}
	}

	void Start()
	{

	}

	// Update is called once per frame
	public void ButtonPress()
	{
        if (selected)
        {
			GetComponent<MeshRenderer>().material.color = Color.white;
			selected = !selected;
        }
        else
        {
			GetComponent<MeshRenderer>().material.color = Color.red;
			selected = !selected;
		}
	}
}
