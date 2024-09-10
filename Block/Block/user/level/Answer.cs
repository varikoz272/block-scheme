using System;

using Block.Algorithm;

namespace Block.user.level
{
	public class Answer : ISQLData
	{
		protected int id;
		private string text;
		
		public Answer(int id, string text)
		{
			this.id = id;
			this.text = text;
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
