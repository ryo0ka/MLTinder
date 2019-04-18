using System;
using UnityEngine;

namespace Tinder
{
	[Serializable]
	public class PersonEntry : IPersonEntry
	{
		[SerializeField]
		Texture _thumbnail;

		[SerializeField]
		string _name;

		[SerializeField]
		int _age;

		[SerializeField, TextArea]
		string _description;

		public Texture Thumbnail => _thumbnail;
		public string Name => _name;
		public int Age => _age;
		public string Description => _description;
	}
}