using System;
using UniRx.Async;
using UnityEngine;

namespace Tinder
{
	public class PageViewController : MonoBehaviour
	{
		[SerializeField]
		PageView _pageTemplate;

		[SerializeField]
		RectTransform _root;

		PageView[] _pages;
		int _currentPageIndex;

		const int PageCount = 2;

		public bool AcceptsInput { get; private set; }

		void Awake()
		{
			_currentPageIndex = 0;
			_pages = new PageView[PageCount];
			for (int i = 0; i < PageCount; i++)
			{
				var page = Instantiate(_pageTemplate, _root);
				page.gameObject.SetActive(false);
				_pages[i] = page;
			}

			_pageTemplate.gameObject.SetActive(false);

			AcceptsInput = true;
		}

		public void ResetPaging()
		{
			_currentPageIndex = 0;

			foreach (var p in _pages)
			{
				p.ResetAnimation();
			}
		}

		public async void Enter(IPersonEntry entry)
		{
			var page = _pages[_currentPageIndex];

			page.gameObject.SetActive(true);

			try
			{
				AcceptsInput = false;
				await page.Enter(entry);
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
			finally
			{
				AcceptsInput = true;
			}
		}

		public void ExitCurrent(bool saved)
		{
			var p = _pages[_currentPageIndex];
			p.Exit(saved).Forget(Debug.LogException);

			_currentPageIndex += 1;
			if (_currentPageIndex >= PageCount)
			{
				_currentPageIndex = 0;
			}
		}
	}
}