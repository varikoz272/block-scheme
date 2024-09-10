using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Block.manage;
using System.IO;

namespace Block.user.level
{
	public abstract class Context
	{
		protected static readonly Size BUTTON_SIZE = new Size(40, 40);
		protected static readonly Size ELEMENT_SIZE = new Size(30, 30);
		protected static readonly int SCREEN_WIDTH = Screen.PrimaryScreen.Bounds.Width;
		protected static readonly int SCREEN_HEIGHT = Screen.PrimaryScreen.Bounds.Height;
	
		protected string title;
	
		protected Context(string title)
		{
			this.title = title;
		}
		
		public abstract void GetMessage();
		
		public string Title
		{
			get {return title;}
			private set{title = value;}
		}
	}
	
	public class StringContext : Context
	{
		private string text;
		private string title;
		
		private static readonly int FORM_WIDTH = 800;
		private static readonly int FORM_HEIGHT = 600;
		
		public StringContext(string title, string text) : base(title)
		{
			this.title = title;
			this.text = text;
		}
		
		public override void GetMessage()
		{
			//TODO ПРЕДЕЛАТЬ ВСЕ
			Form questionForm = new Form();
			questionForm.Size = new Size(FORM_WIDTH, FORM_HEIGHT);
			questionForm.StartPosition = FormStartPosition.CenterScreen;
			questionForm.Visible = true;
			questionForm.BackColor = Easel.BG_COLOR;
			
			Label textLabel = new Label();
			textLabel.Text = text;
			textLabel.ForeColor = Easel.OUT_COLOR;
			textLabel.Size = new Size(FORM_WIDTH - 20, FORM_HEIGHT - 20);
			textLabel.Location = new Point(10, 100);
			textLabel.Font = new Font("Cascadia Mono", 14F);
			questionForm.Controls.Add(textLabel);
			
			Label titleLabel = new Label();
			titleLabel.Text = title;
			textLabel.ForeColor = Easel.OUT_COLOR;
			titleLabel.Size = new Size(FORM_WIDTH - 20, 80);
			titleLabel.Location = new Point(10, 10);
			titleLabel.Font = new Font("Cascadia Mono", 20F);
			questionForm.Controls.Add(titleLabel);
		}
		
		public string Text
		{
			get {return text;}
			private set {text = value;}
		}
	}
	
	public class FormContext : Context
	{
		private static readonly int FORM_WIDTH = 500;
		private static readonly int FORM_HEIGHT = 500;
		
		private static readonly string answersCachePath = @"./answersCache.txt";
		
		private Exam exam;
		private int curQuestionId;
		
		public FormContext(string title, Exam exam) : base(title)
		{
			this.exam = exam;
			curQuestionId = 0;
		}
		
		public override void GetMessage()
		{
			//TODO НОРМАЛЬНО СДЕЛАТЬ
			List<Answer> answers = exam.Questions[curQuestionId].Answers;
			Form questionForm = new Form();
			questionForm.Size = new Size(FORM_WIDTH, FORM_HEIGHT);
			questionForm.StartPosition = FormStartPosition.CenterScreen;
			questionForm.Visible = true;
			questionForm.BackColor = Easel.BG_COLOR;
			
			addQuestionLabel(questionForm.Controls, exam.Questions[curQuestionId].Text);
			
			for (int i = 0; i < answers.Count; i++)
				addAnswerLabel(questionForm.Controls, answers[i].Text, Title, curQuestionId, i);
			
			addControlButtons(questionForm.Controls, this, questionForm, curQuestionId, exam.Questions.Count);
		}
		
		private static void addControlButtons(Control.ControlCollection controls, FormContext formHolder, Form form, int curIndex, int maxIndex)
		{
			Button[] controlButtons = new Button[2];
			

			controlButtons[0] = new Button();
			controlButtons[0].Text = (curIndex < maxIndex - 1) ? ">" : "✓";
			controlButtons[0].Size = BUTTON_SIZE;
			controlButtons[0].Location = new Point(FORM_WIDTH - (int)(BUTTON_SIZE.Width * 1.5), FORM_HEIGHT - BUTTON_SIZE.Height * 2);
			controlButtons[0].BackColor = Easel.MAIN_COLOR;
			controlButtons[0].ForeColor = Easel.OUT_COLOR;
			if (curIndex < maxIndex - 1)
				controlButtons[0].Click += (sender, e) => {
				formHolder.curQuestionId++;
				form.Close();
				formHolder.GetMessage();
			};
			
			if (curIndex != 0) {
				controlButtons[1] = new Button();
				controlButtons[1].Text = "<";
				controlButtons[1].Size = BUTTON_SIZE;
				controlButtons[1].Location = new Point(0, FORM_HEIGHT - BUTTON_SIZE.Height * 2);
				controlButtons[1].BackColor = Easel.MAIN_COLOR;
				controlButtons[1].ForeColor = Easel.OUT_COLOR;
				if (curIndex > 0)
				controlButtons[1].Click += (sender, e) => {
					formHolder.curQuestionId--;
					form.Close();
					formHolder.GetMessage();
				};
			}
			
			
			foreach(var curControl in controlButtons)
				controls.Add(curControl);
		}
		
		private static void addQuestionLabel(Control.ControlCollection controls, string text)
		{
			Label questionLabel = new Label();
			questionLabel.Text = text;
			questionLabel.Location = new Point(ELEMENT_SIZE.Width / 2, ELEMENT_SIZE.Height / 2);
			questionLabel.Size = new Size(FORM_WIDTH, ELEMENT_SIZE.Height);
			questionLabel.ForeColor = Easel.OUT_COLOR;
			questionLabel.Font = new System.Drawing.Font("Cascadia Mono", 18F);
			controls.Add(questionLabel);
		}
		
		private static void addAnswerLabel(Control.ControlCollection controls, string text, string testName, int questionId, int id)
		{
			RadioButton answerLabel = new RadioButton();
			answerLabel.Text = text;
			answerLabel.Location = new Point(ELEMENT_SIZE.Width / 2, id * ELEMENT_SIZE.Height + ELEMENT_SIZE.Height * 4);
			answerLabel.Size = new Size(FORM_WIDTH, ELEMENT_SIZE.Height);
			answerLabel.Font = new System.Drawing.Font("Cascadia Mono", 15F);
			answerLabel.ForeColor = Easel.OUT_COLOR;
			answerLabel.CheckedChanged += (sender, e) => {
				//ЗАПОМИНАТЬ ОТВЕТЫ
				recordAnswer(testName, questionId, id);
			};
			controls.Add(answerLabel);
		}
		
		private static void recordAnswer(string testName, int questionId, int answerId) {
			if (!File.Exists(answersCachePath))
				File.Create(answersCachePath);
			
			string content = File.ReadAllText(answersCachePath);
			if (content.Contains("|" + testName + ":" + questionId + ":")) {
				
			}
			
			else {
				content += "|" + testName + ":" + questionId + ":" + answerId + "\n";
				File.WriteAllText(answersCachePath, content);
			}
		}
	}
}
