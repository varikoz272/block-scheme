using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using System.Collections;

using System.Windows.Forms;

using Block.user;
using Block.manage;
using Block.user.level;
using Block.Algorithm;

namespace Block.manage.SQL
{
	public class SqlTableManager
	{
		private SqlConnection connection;
		
		public static readonly string USER_TABLE = "users";
		public static readonly string USER_PASSED_EXAMS = "passed_exams";
		public static readonly string COURSE_TABLE = "course";
		public static readonly string COURSE_EXAMS_TABLE = "course_exams";
		public static readonly string EXAM_TABLE = "exam";
		public static readonly string NEEDED_THEORY_TABLE = "needed_theory";
		public static readonly string THEORY_TABLE = "theory";
		public static readonly string EXAM_QUESTIONS_TABLE = "exam_questions";
		public static readonly string QUESTION_TABLE = "question";
		public static readonly string QUESTION_ANSWERS_TABLE = "question_answers";
		public static readonly string ANSWER_TABLE = "answer";

		public List<User> GetUsers()
		{
			DataTable userTable = new DataTable();
			new SqlDataAdapter("SELECT * FROM " + USER_TABLE, connection).Fill(userTable);
			
			List<User> users = new List<User>();
			foreach (DataRow curRow in userTable.Rows)
				users.Add(GetUser(curRow));
			
			return users;
		}
		
		private User GetUser(DataRow row)
		{
			if (row["role_name"].Equals("Student"))
				return new Student((int) row["id"], row["name"].ToString(), row["password"].ToString());
			if (row["role_name"].Equals("Tutor"))
				return new Tutor((int) row["id"], row["name"].ToString(), row["password"].ToString());
			if (row["role_name"].Equals("Admin"))
				return new Admin((int) row["id"], row["name"].ToString(), row["password"].ToString());
			return null;
		}
		
		public List<Course> GetCourses(List<User> users)
		{
			DataTable courseTable = new DataTable();
			new SqlDataAdapter("SELECT * FROM " + COURSE_TABLE, connection).Fill(courseTable);
			List<Course> courses = new List<Course>();
			
			DataTable courseExamsTable = new DataTable();
			new SqlDataAdapter("SELECT * FROM " + COURSE_EXAMS_TABLE, connection).Fill(courseExamsTable);
			
			DataTable examTable = new DataTable();
			new SqlDataAdapter("SELECT * FROM " + EXAM_TABLE, connection).Fill(examTable);
			List<Exam> exams = new List<Exam>();
			
			DataTable theoryTable = new DataTable();
			new SqlDataAdapter("SELECT * FROM " + THEORY_TABLE, connection).Fill(theoryTable);
			List<Theory> theory= new List<Theory>();
			
			DataTable neededTheoryTable = new DataTable();
			new SqlDataAdapter("SELECT * FROM " + NEEDED_THEORY_TABLE, connection).Fill(neededTheoryTable);
			
			DataTable questionTable = new DataTable();
			new SqlDataAdapter("SELECT * FROM " + QUESTION_TABLE, connection).Fill(questionTable);
			List<Question> questions = new List<Question>();
			
			DataTable examQuestionsTable = new DataTable();
			new SqlDataAdapter("SELECT * FROM " + EXAM_QUESTIONS_TABLE, connection).Fill(examQuestionsTable);
			
			DataTable answerTable = new DataTable();
			new SqlDataAdapter("SELECT * FROM " + ANSWER_TABLE, connection).Fill(answerTable);
			List<Answer> answers = new List<Answer>();
			
			DataTable questionAnswersTable = new DataTable();
			new SqlDataAdapter("SELECT * FROM " + QUESTION_ANSWERS_TABLE, connection).Fill(questionAnswersTable);
			
			DataTable userPassedExams = new DataTable();
			new SqlDataAdapter("SELECT * FROM " + USER_PASSED_EXAMS, connection).Fill(userPassedExams);
			
			foreach (DataRow curCourse in courseTable.Rows)
				courses.Add(new Course((int) curCourse["id"], curCourse["name"].ToString(), curCourse["password"].ToString(), new List<Exam>()));
			
			foreach (DataRow curExam in examTable.Rows)
				exams.Add(new Exam((int) curExam["id"], curExam["name"].ToString(), new List<Theory>(), new List<Question>(), new List<User>()));
			
			foreach (DataRow curConnect in courseExamsTable.Rows)
			{
				Course curCourse = (Course) GetObjectById((int) curConnect["course_id"], courses);
				Exam curExam = (Exam) GetObjectById((int) curConnect["exam_id"], exams);
				curCourse.Exams.Add(curExam);
			}
			
			foreach (DataRow curTheory in theoryTable.Rows)
				theory.Add(new Theory((int) curTheory["id"], curTheory["name"].ToString(), curTheory["text"].ToString()));
			
			foreach (DataRow curConnect in neededTheoryTable.Rows)
			{
				Exam curExam = (Exam) GetObjectById((int) curConnect["exam_id"], exams);
				Theory curTheory = (Theory) GetObjectById((int) curConnect["theory_id"], theory);
				curExam.TheoryNeeded.Add(curTheory);
			}
			
			foreach (DataRow curQuestion in questionTable.Rows)
				questions.Add(new Question((int) curQuestion["id"], curQuestion["text"].ToString(), new List<Answer>()));
		
			foreach (DataRow curConnect in examQuestionsTable.Rows)
			{
				Exam curExam = (Exam) GetObjectById((int) curConnect["exam_id"], exams);
				Question curQuestion = (Question) GetObjectById((int) curConnect["question_id"], questions);
				curExam.Questions.Add(curQuestion);
			}
			
			foreach (DataRow curAnswer in answerTable.Rows)
				answers.Add(new Answer((int) curAnswer["id"], curAnswer["text"].ToString(), curAnswer["is_correct"].Equals("True") ? true : false));//===================!!!
			
			foreach (DataRow curConnect in questionAnswersTable.Rows)
			{
				Question curQuestion = (Question) GetObjectById((int) curConnect["question_id"], questions);
				Answer curAnswer =  (Answer) GetObjectById((int) curConnect["answer_id"], answers);
				curQuestion.Answers.Add(curAnswer);
			}
			
			foreach (DataRow curConnect in userPassedExams.Rows)
			{
				User curUser = (User) GetObjectById((int) curConnect["user_id"], users);
				Exam curExam = (Exam) GetObjectById((int) curConnect["exam_id"], exams);
				curExam.Passed.Add(curUser);
			}
			
			
			return courses;
		}
		
		private static object GetObjectById(int id, List<Course> list) // List<ISQLData>
		{
			foreach (var el in list)
				if (el.GetId() == id) return el;
			return null;
		}
		
		private static object GetObjectById(int id, List<User> list) // List<ISQLData>
		{
			foreach (var el in list)
				if (el.GetId() == id) return el;
			return null;
		}
		
		private static object GetObjectById(int id, List<Question> list)
		{
			foreach (var el in list)
				if (el.GetId() == id) return el;
			return null;
		}
		
		private static object GetObjectById(int id, List<Exam> list) // List<ISQLData>
		{
			foreach (var el in list)
				if (el.GetId() == id) return el;
			return null;
		}
		
		private static object GetObjectById(int id, List<Theory> list) // List<ISQLData>
		{
			foreach (var el in list)
				if (el.GetId() == id) return el;
			return null;
		}
		
		private static object GetObjectById(int id, List<Answer> list) // List<ISQLData>
		{
			foreach (var el in list)
				if (el.GetId() == id) return el;
			return null;
		}
		
		public SqlTableManager(SqlConnection connection)
		{
			this.connection = connection;
		}
		
		public DataTable GetTable(string tableName)
		{
			string query = "SELECT * FROM " + tableName;
			SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
			DataTable table = new DataTable();
			dataAdapter.Fill(table);

			return table;
		}
		
		private Course GetCourseByName(string name)
		{
			foreach (var curCourse in TopManager.instance.Courses)
				if (curCourse.Name.Equals(name))
					return curCourse;
			
			return null;
		}
		
		public int GetFreeId(string tableName)
		{
			List<int> invalids = new List<int>();
			foreach (DataRow curRow in GetTable(tableName).Rows)
				invalids.Add((int) curRow["id"]);
			
			int lowest = 0;
			while (invalids.Contains(lowest)) lowest++;
			
			return lowest;
		}
		
		public void AddRow(string tableName, string[] columns, string[] values)
		{
			string query = "INSERT INTO " + tableName + " (";
			for (int i = 0; i < columns.Length; i++)
				query += columns[i] + ((i == columns.Length - 1) ? ") VALUES (" : ",");
			

			for (int i = 0; i < values.Length; i++)
				query += "\'"+ values[i] + ((i == values.Length - 1) ? "\')" : "\',");
			
			ExecuteCommand(query);
		}
		
		private void ExecuteCommand(string query)
		{
			new SqlCommand(query, connection).ExecuteNonQuery();
			TopManager.instance.Update();
		}

		public void CloseConnection()
		{
			connection.Close();
		}
	}

}
