using System;

namespace Tinder
{
	public interface IDecisionListener
	{
		IObservable<bool> OnDecisionListened { get; }
	}
}