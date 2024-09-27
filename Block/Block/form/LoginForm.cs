using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Block.manage;

using System.Data;
using System.Data.SqlClient;




namespace Block.form
{
	public partial class LoginForm : Form
	{
		public LoginForm()
		{
			InitializeComponent();
			TopManager.instance.Update();
		}
		
		private void LoginFormFormClosed(object sender, FormClosedEventArgs e)
		{
			Hide();
		}
		
		private LoginInfo GetInput(Button pressed)
		{
			string name = nameTextBox.Text;
			string password = passwordTextBox.Text;
			
			if (pressed == loginButton)
				return new LoginInfo(name, password, Input.Login);
			
			if (pressed == registrationButton)
				return new LoginInfo(name, password, Input.Registration);
			
			return null;
		}
		
		private void ComeBack(LoginInfo info) 
		{
			if (info.name.Equals("")) throw new PassException("Введите имя");
			if (info.password.Equals("")) throw new PassException("Введите пороль");
			
			
			if (info.input == Input.Login)
			{
				foreach (var curUser in TopManager.instance.Users)
					if (curUser.Name.Equals(info.name) && curUser.Password.Equals(info.password))
					{
						Hide();
						new BlockForm(TopManager.instance.GetUserByName(info.name)).Show();
						return;
					}
				throw new PassException("Неправильное имя или пороль");
			}
			
			else
			{
				if (getUnsafePasswords().Contains(info.password)) throw new PassException("Пороль слишком ненадежный");
				
				bool onlyWhiteSpaces = true;
				foreach (var c in info.password)
					if (c != ' ') {
						onlyWhiteSpaces = false;
						break;
					}
				if (onlyWhiteSpaces) throw new PassException("Пороль состоит только из пробелов");
				
				foreach (var curUser in TopManager.instance.Users)
					if (curUser.Name.Equals(info.name))
						throw new PassException("Имя занято");
				
				TopManager.instance.RegisterUser(info.name, info.password);
				Hide();
				new BlockForm(TopManager.instance.GetUserByName(info.name)).Show();
				return;
			}
		}
		
		private void LoginButtonClick(object sender, EventArgs e)
		{
			try 
			{
				ComeBack(GetInput((Button) sender));
			}
			catch (PassException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		private void RegistrationButtonClick(object sender, EventArgs e)
		{
			try 
			{
				ComeBack(GetInput((Button) sender));
			}
			catch (PassException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		private void PassButtonClick(object sender, EventArgs e)
		{
			try 
			{
				ComeBack(GetInput((Button) sender));
			}
			catch (PassException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		
		void LoginFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason.Equals(CloseReason.UserClosing))
				Application.Exit();
		}
		
		public static List<string> getUnsafePasswords() {
			return new List<string>{
				"password",
				"пороль",
				"qwerty",
				"123456"
			};
		}
	}
	
	internal enum Input
	{
		Login,
		Registration,
	}
	
	internal class LoginInfo
	{
		public string name;
		public string password;
		public Input input;
		
		public LoginInfo(string name, string password, Input input)
		{
			this.name = name;
			this.password = password;
			this.input = input;
		}
	}
	
	internal class PassException : Exception
	{
		public PassException(string msg) : base(msg) { }
	}
}
