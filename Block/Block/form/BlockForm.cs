using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using Block.user;
using Block.user.level;
using Block.manage;
using Block.Algorithm;

namespace Block.form
{
	public partial class BlockForm : Form
	{
		private CourseVisualizer visualizer;
		private int prevY = 9;
		
		public BlockForm(User user)
		{
			InitializeComponent();
			this.BackColor = Easel.BG_COLOR;
			TopManager.instance.activeUser = user;
			visualizer = new CourseVisualizer(coursePage);
			visualizer.PrintMembersList();
		}
		
		private void BlockFormFormClosed(object sender, FormClosedEventArgs e)
		{
			TopManager.instance.disconnectDatabase();
			Application.ExitThread();
		}
		
		void SwitchCourseButtonClick(object sender, EventArgs e)
		{
			visualizer.Clear(new List<object> { sender, trackBar } );
			var curCourse = TopManager.instance.NextCourse;
			visualizer.Print(curCourse);
			if (curCourse != null)
				visualizer.PrintMembersList();
			
			trackBar.Value = trackBar.Maximum;
			prevY = trackBar.Maximum;
		}

		void TrackBarScroll(object sender, EventArgs e)
		{
			int offset = visualizer.GetMinMaxY().s / 10;
			visualizer.MoveAllY((trackBar.Value - prevY) * offset);
			prevY = trackBar.Value;
		}
	}
	
	internal sealed class CourseVisualizer
	{
		private static readonly int ELEMENT_D = 100;
		private static readonly int ELEMENT_R = ELEMENT_D / 2;
		
		private readonly TabPage field;
		private readonly int MIDDLE_X;
		private readonly int MIDDLE_Y;
		private readonly int OFFSET_X;
		private readonly int START_Y;
		
		private IntPairCache maxMinYCache;
		
		public CourseVisualizer(TabPage page)
		{
			field = page;
			MIDDLE_X = page.Width / 2;
			MIDDLE_Y = page.Height / 2;
			OFFSET_X = MIDDLE_X / 4 + ELEMENT_D;
			START_Y = page.Height - (int) (ELEMENT_D * 1.5);
		}
		
		public IntPairCache GetMinMaxY()
		{
			if (maxMinYCache != null) return maxMinYCache;
			
			maxMinYCache = new IntPairCache(field.Controls[0].Location.Y, field.Controls[0].Location.Y);
			
			foreach(Control curControl in field.Controls)
			{
				if (curControl.Location.Y < maxMinYCache.f) maxMinYCache.f = curControl.Location.Y;
				if (curControl.Location.Y > maxMinYCache.s) maxMinYCache.s = curControl.Location.Y;
			}
			
			return maxMinYCache;
		}
		
		public void MoveAllY(int yOffset)
		{
			foreach(Control curControl in field.Controls)
				if (!(curControl.Name.Equals("trackBar")))
					curControl.Top += yOffset;
		}
		
		public void Clear(List<object> except)
		{
			foreach (Control curEl in field.Controls)
			{
				if (!(except.Contains(curEl)))
					field.Controls.Remove(curEl);
			}
		}
		
		public void Print(Course course)
		{
			if (course == null) return;
			
			List<RoundButton> visualized = new List<RoundButton>();
			
			int startUnwrapPosition = 0;
			
			foreach (Exam curExam in course.Exams)
			{
				List<Control> unwrapedTheory = UnwrapExam(curExam, startUnwrapPosition);
				startUnwrapPosition += 2;
				RoundButton examButton = GetExamRoundButton(
					GetButtonMiddlePlacement(curExam.TheoryNeeded.Count + 2 + startUnwrapPosition, MIDDLE_X),
					curExam
				);
				
				foreach(Control curControl in unwrapedTheory)
					field.Controls.Add(curControl);
				field.Controls.Add(examButton);
				
				startUnwrapPosition += curExam.TheoryNeeded.Count + 5;
				startUnwrapPosition -= startUnwrapPosition % 2;
			}
			
			field.Controls.Add(GetExamRoundButton(
				GetButtonMiddlePlacement(startUnwrapPosition, MIDDLE_X),
				Exam.GetFinalExam()
			));
		
			maxMinYCache = null;
		}
		
		private List<Control> UnwrapExam(Exam exam, int startUnwrapPosition)
		{
			List<Control> unwraped = new List<Control>();
			
			Label label = new Label();
			label.Text = exam.Name;
			label.Font = new System.Drawing.Font("Cascadia Mono", 15F);
			label.ForeColor = Easel.ATTENTION_COLOR;
			label.BackColor = Easel.ALT_COLOR;
			label.Location = GetButtonMiddlePlacement(startUnwrapPosition, MIDDLE_X);
			label.Location = new Point(0, label.Location.Y);
			label.Size = new Size(field.Size.Width, 32);
			label.TextAlign = ContentAlignment.MiddleCenter;
			startUnwrapPosition += 2;
			
			for (int i = 0; i < exam.TheoryNeeded.Count; i++)
				unwraped.Add(GetBasicRoundButton(
					exam.TheoryNeeded[i].Name,
					GetButtonPlacement(i + startUnwrapPosition, MIDDLE_X, exam.TheoryNeeded.Count + startUnwrapPosition),
					exam.TheoryNeeded[i].Text
				));
			
			unwraped.Add(label);
			return unwraped;
		}
		
		
		private static Point GetButtonPlacement(int id, int middleX, int maxId)
		{
			if (id + 1 == maxId && id % 2 == 0)
				return GetButtonMiddlePlacement(id, middleX);
			return new Point(((id % 2) * 2 - 1) * ELEMENT_D + middleX - ELEMENT_R, id / 2 * ELEMENT_D + id / 2 * ELEMENT_R / 4);
		}
		
		private static Point GetButtonMiddlePlacement(int id, int middleX)
		{
			return new Point(middleX - ELEMENT_R, id / 2 * ELEMENT_D + id / 2 * ELEMENT_R / 4);
		}
		
		private static RoundButtonContext GetBasicRoundButton(string name, Point location, string context)
		{
			var button = new RoundButtonContext(new StringContext(name, context));
			
			button.Location = location;
			button.Name = name;
			button.Size = new System.Drawing.Size(ELEMENT_D, ELEMENT_D);
			button.TabIndex = 0;
			button.Text = name;
			button.Font = new System.Drawing.Font("Cascadia Mono", 10F);
			button.ForeColor = Easel.OUT_COLOR;
			button.BackColor = Easel.MAIN_COLOR;
			button.Visible = true;
			if (button.IsChecked())
					button.BackColor = Easel.DARK_MAIN_COLOR;
			return button;
		}
		
		private static RoundButtonContext GetExamRoundButton(Point location, Exam exam)
		{
			var button = new RoundButtonContext(new FormContext(exam.Name, exam));
			button.Location = location;
			button.Name = exam.Name;
			button.Size = new System.Drawing.Size(ELEMENT_D, ELEMENT_D);
			button.TabIndex = 0;
			button.Font = new System.Drawing.Font("Cascadia Mono", 10F);
			button.ForeColor = Easel.ATTENTION_COLOR;
			button.BackColor = Easel.ALT_COLOR;
			button.Text = exam.Name;
			button.Visible = true;
			if (button.IsChecked())
				button.BackColor = Easel.DARK_ALT_COLOR;
						
			return button;
		}
		
		public void PrintMembersList()
		{
			for (int i = 0; i < TopManager.instance.Users.Count; i++)
			{
				var curUser = TopManager.instance.Users[i];
				var curLabel = new Label();
				curLabel.Text = curUser.Name;
				curLabel.Location = new Point(5, 20 * i + 50);
				curLabel.ForeColor = ((i % 2 == 0) ? Easel.ATTENTION_COLOR : Easel.DARK_MAIN_COLOR);
				curLabel.Font = new System.Drawing.Font("Cascadia Mono", 10F);
				curLabel.Visible = true;
				
				field.Controls.Add(curLabel);
			}
		}
		
		private Label GetLabelAtY(int y)
		{
			foreach (Control curControl in field.Controls)
				if (curControl.GetType().Name.Equals("Label"))
					if (curControl.Location.Y == y) return curControl as Label;
			return null;
		}
		
	}
}
