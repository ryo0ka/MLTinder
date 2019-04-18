using UnityEngine;

namespace Tinder
{
	public interface IPersonEntry
	{
		Texture Thumbnail { get; }
		string Name { get; }
		int Age { get; }
		string Description { get; }
	}
}