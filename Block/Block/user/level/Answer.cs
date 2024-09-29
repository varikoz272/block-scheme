using System;

using Block.Algorithm;

namespace Block.user.level
{
	public class Answer : ISQLData
	{
		protected int id;
		private string text;
		public readonly bool isCorrect;
		
		public Answer(int id, string text, bool isCorrect)
		{
			this.id = id;
			this.text = text;
			this.isCorrect = isCorrect;
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
