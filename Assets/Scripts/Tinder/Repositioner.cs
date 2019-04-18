using UnityEngine;
using UnityEngine.XR.MagicLeap;
using Utils;

namespace Tinder
{
	public class Repositioner : MonoBehaviour
	{
		[SerializeField]
		Transform _target;

		[SerializeField]
		float _position;

		Transform _camera;
		bool _repositioning;

		void Start()
		{
			_camera = Camera.main.transform;

			MLInput.OnTriggerDown += (b, f) =>
			{
				_repositioning = true;
			};

			MLInput.OnTriggerUp += (b, f) =>
			{
				_repositioning = false;
			};
		}

		void Update()
		{
			if (!_repositioning && !Input.GetKey(KeyCode.Space)) return;

			_target.position = _camera.position + _camera.forward * _position;
			_target.LookAt(_camera);
			_target.SetLocalEulerAngles(x: 0, y: _target.localEulerAngles.y + 180, z: 0);
		}
	}
}