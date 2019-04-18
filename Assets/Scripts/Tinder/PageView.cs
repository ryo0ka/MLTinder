using UniRx.Async;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Utils.Views;

namespace Tinder
{
	public class PageView : MonoBehaviour
	{
		[SerializeField]
		RectTransform _root;
		
		[SerializeField]
		RawImage _thumbnailImage;

		[SerializeField]
		Text _nameText;

		[SerializeField]
		Text _descriptionText;

		[SerializeField]
		CanvasGroup _mask;

		[SerializeField]
		AnimationCurve _curve;

		[SerializeField]
		float _initialDepth;

		[SerializeField]
		float _lastAngle;

		[SerializeField]
		float _lastOffset;

		public async UniTask Enter(IPersonEntry entry)
		{
			ResetAnimation();

			_thumbnailImage.SetTexture(entry.Thumbnail);
			_nameText.text = $"{entry.Name}, {entry.Age}";
			_descriptionText.text = entry.Description;

			await UnityUtils.Animate(.5f, _curve, t =>
			{
				_mask.alpha = t;
				float initialDepth = _initialDepth / _root.lossyScale.z;
				_root.SetAnchoredPosition(z: Mathf.Lerp(initialDepth, 0f, t));
			});
		}

		public async UniTask Exit(bool saved)
		{
			ResetAnimation();

			await UnityUtils.Animate(.5f, _curve, t =>
			{
				_mask.alpha = 1f - t;

				float lastAngle = saved ? _lastAngle : -_lastAngle;
				_root.SetLocalEulerAngles(z: Mathf.Lerp(0, lastAngle, t));

				float lastOffset = saved ? _lastOffset : -_lastOffset;
				_root.SetAnchoredPosition(x: Mathf.Lerp(0, lastOffset, t));
			});
		}

		public void ResetAnimation()
		{
			_mask.alpha = 1;
			_root.SetAnchoredPosition(x: 0, z: 0);
			_root.SetLocalEulerAngles(z: 0);
		}
	}
}