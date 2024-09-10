using System;
using System.Linq;

using Block.Algorithm;

namespace Block.user
{
	public abstract class User : ISQLData
	{
		protected int id;
		protected string name;
		protected string password;
		
		public User(int id, string name, string password)
		{
			this.id = id;
			this.name = name;
			this.password = password;
		}
		
		public string Name
		{
			get { return name; }
			protected set { name = value; }
		}
		
		public string Password
		{
			get { return password; }
			protected set { password = value; }
		}
		
		public int GetId()
		{
			return id;
		}
		
		protected abstract bool IsAvaibleToChat();
		
		protected abstract bool IsAvaibleToMakeGroups();
		
		protected abstract bool IsAvaibleToKickMembers();
		protected abstract bool IsAvaibleToAddMembers();
		
		protected abstract bool IsAvaibleToCreateUsers();
		protected abstract bool IsAvaibleToDeleteUsers();
	}
}
