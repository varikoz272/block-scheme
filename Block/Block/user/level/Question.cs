using System;
using System.Collections.Generic;

using Block.Algorithm;

namespace Block.user.level
{
	public class Question : ISQLData
	{
		private int id;
		private string text;
		private List<Answer> answers;
		private Answer correctAnswer;
		
		public Question(int id, string text, List<Answer> answers, int correctAnswerId)
		{
			this.id = id;
			this.text = text;
			this.answers = answers;
			
			correctAnswer = null;
			if (answers != null)
				if (correctAnswerId < answers.Count) this.correctAnswer = answers[correctAnswerId];
		}
		
		public Question(int id, string question, List<Answer> answers) : this(id, question, answers, 0) { }
		
		public string Text
		{
			get {return text;}
			private set{text = value;}
		}
		
		public List<Answer> Answers
		{
			get {return answers;}
			private set {answers = value;}
		}
		
		public int GetId()
		{
			return id;
		}
	}
}
