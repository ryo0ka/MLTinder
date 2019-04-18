using System.Linq;
using UnityEngine;
using UniRx;

namespace Tinder
{
	public class DemoController : MonoBehaviour
	{
		[SerializeField]
		bool _useMockListener;

		[SerializeField]
		DecisionListener _mlDecisionListener;

		[SerializeField]
		PageViewController _pageViewController;

		[SerializeField]
		PersonEntry[] _pageEntries;

		IDecisionListener _decisionListener;
		int _entryIndex;

		void Start()
		{
			Debug.Log("Starting scene");

			_decisionListener = Application.isEditor && _useMockListener
				? (IDecisionListener) new EditorDecisionListener()
				: (IDecisionListener) _mlDecisionListener;

			_decisionListener.OnDecisionListened.Subscribe(saved => OnDecision(saved));

			_pageViewController.ResetPaging();
			_pageViewController.Enter(_pageEntries.First());

			Debug.Log("Started scene");
		}

		void OnDecision(bool saved)
		{
			if (!_pageViewController.AcceptsInput)
			{
				Debug.Log($"Exit intended ({saved}) but view wasn't ready");
				return;
			}

			_pageViewController.ExitCurrent(saved);

			_entryIndex += 1;
			if (_entryIndex >= _pageEntries.Length)
			{
				_entryIndex = 0;
			}

			var entry = _pageEntries[_entryIndex];
			_pageViewController.Enter(entry);
		}
	}
}