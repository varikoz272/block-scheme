using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;

using Block.user.level;
using Block.manage;

namespace Block.form
{
	public class RoundButton : Button
	{

	}
	
	public class RoundButtonContext : RoundButton
	{
		public static readonly string checkedCachePath = "./checked.txt";
		private Context context;
		
		public RoundButtonContext(Context context)
		{
			this.context = context;
			Click += OnClick;
		}
		
		public Context Context
		{
			get {return context;}
			private set {context = value;}
		}
		
		public void OnClick(object sender, EventArgs e)
		{
			context.GetMessage();
			Check();
			BackColor = Easel.GetDarker(BackColor);
		}
		
		public void Check()
		{
			string content = File.ReadAllText(checkedCachePath);
			if (!content.Contains(context.Title))
			{
				content += context.Title;
				File.WriteAllText(checkedCachePath, content);
			}
		}
		
		public bool IsChecked()
		{
			string content = File.ReadAllText(checkedCachePath);
			return content.Contains(context.Title);
		}
	}
}
