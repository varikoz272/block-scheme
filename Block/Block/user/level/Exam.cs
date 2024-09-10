using System;
using System.Collections.Generic;

using Block.Algorithm;
using Block.manage;

namespace Block.user.level
{
	public class Exam : ISQLData
	{
		private int id;
		private string name;
		private List<Theory> theoryNeeded;
		private List<Question> questions;
		private List<User> passed;
		
		public Exam(List<Theory> theoryNeeded,  List<Question> questions, List<User> passed) :
			this (0, "Тест", theoryNeeded, questions, passed) { }
		
		public Exam(int id, string name, List<Theory> theoryNeeded,  List<Question> questions, List<User> passed)
		{
			this.id = id;
			this.passed = passed;
			this.name = name;
			this.theoryNeeded = theoryNeeded;
			this.questions = questions;
		}
		
		public string Name
		{
			get {return name;}
			private set {name = value;}
		}
		
		public List<Theory> TheoryNeeded
		{
			get {return theoryNeeded;}
			private set {theoryNeeded = value;}
		}
		
		public List<Question> Questions
		{
			get {return questions;}
			private set {questions = value;}
		}
		
		public List<User> Passed
		{
			get {return passed;}
			private set {passed = value;}
		}
		
		public int GetId()
		{
			return id;
		}
		
		public static Exam GetFinalExam()
		{
			List<Question> questions = new List<Question>();
			foreach (Exam curExam in TopManager.instance.Courses[0].Exams)
			{
				foreach (Question curQuestion in curExam.Questions)
				{
					questions.Add(curQuestion);
				}
			}
			return new Exam(-1, "Финал", new List<Theory>(), questions, new List<User>());
		}
	}
}
