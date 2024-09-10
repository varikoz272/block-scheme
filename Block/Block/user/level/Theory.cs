using System;

using Block.Algorithm;

namespace Block.user.level
{
	public class Theory : ISQLData
	{
		private int id;
		private string name;
		private string text;
		
		public Theory(int id, string name, string text)
		{
			this.id = id;
			this.name = name;
			this.text = text;
		}
		
		public string Name
		{
			get {return name;}
			private set {name = value;}
		}
		
		public string Text
		{
			get {return text;}
			private set {text = value;}
		}
		
		public int GetId()
		{
			return id;
		}
	}
}
