using System.Collections;
using System.Collections.Generic;

namespace Tinder
{
	public class Sample<T> : IEnumerable<(T, float)>
	{
		readonly int _maxCount;
		readonly LinkedList<(T, float)> _self;

		public Sample(int maxCount)
		{
			_maxCount = maxCount;
			_self = new LinkedList<(T, float)>();
		}

		public IEnumerator<(T, float)> GetEnumerator()
		{
			return _self.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(T element, float time)
		{
			_self.AddFirst((element, time));
			while (_self.Count > _maxCount)
			{
				_self.RemoveLast();
			}
		}

		public void Clear()
		{
			_self.Clear();
		}
	}
}