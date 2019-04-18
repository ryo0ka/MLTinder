using System;
using UniRx;
using UnityEngine;

namespace Tinder
{
	public class EditorDecisionListener : IDecisionListener
	{
		public IObservable<bool> OnDecisionListened { get; }

		public EditorDecisionListener()
		{
			var saveStream =
				Observable.EveryUpdate()
				          .Where(_ => Input.GetKeyUp(KeyCode.RightArrow))
				          .Select(_ => true);

			var discardStream =
				Observable.EveryUpdate()
				          .Where(_ => Input.GetKeyUp(KeyCode.LeftArrow))
				          .Select(_ => false);

			OnDecisionListened = saveStream.Merge(discardStream).Publish().RefCount();
		}
	}
}