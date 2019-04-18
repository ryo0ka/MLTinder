using System;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace Tinder
{
	public class DecisionListener : MonoBehaviour, IDecisionListener
	{
		[SerializeField]
		Transform _head;

		[SerializeField]
		Transform _front;

		[SerializeField]
		float _validAngle;

		[SerializeField]
		float _minLastAngle;

		[SerializeField]
		float _maxSwipeDuration;

		[SerializeField, UsedImplicitly]
		float _currentAngle;

		Subject<bool> _self;
		Sample<float> _angles;

		public IObservable<bool> OnDecisionListened => _self;

		void Awake()
		{
			_self = new Subject<bool>();
			_angles = new Sample<float>(100);
		}

		void Update()
		{
			var v1 = To2(_head.forward);
			var v2 = To2(_front.position - _head.position);
			var angleFirst = Vector2.SignedAngle(v1, v2);
			var timeFirst = Time.time;

			foreach (var (angle, time) in _angles)
			{
				if (timeFirst - time > _maxSwipeDuration)
				{
					break; // max time range
				}

				if (Mathf.Abs(angleFirst - angle) > _minLastAngle &&
				    Mathf.Abs(angle) < _validAngle)
				{
					bool decision = angleFirst - angle > 0;
					_self.OnNext(decision);
					_angles.Clear();
					break; // done
				}
			}

			_angles.Add(angleFirst, timeFirst);
			_currentAngle = angleFirst;
		}

		static Vector2 To2(Vector3 n)
		{
			return new Vector2(n.x, n.z);
		}
	}
}