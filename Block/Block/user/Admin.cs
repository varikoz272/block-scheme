using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Block.user
{
	public class Admin : User
	{
		public Admin(int id, string name, string password) : base(id, name, password) { }
		
		protected sealed override bool IsAvaibleToChat()
		{
			return true;
		}
		
		protected sealed override bool IsAvaibleToMakeGroups()
		{
			return true;
		}
		
		protected sealed override bool IsAvaibleToKickMembers()
		{
			return true;
		}
		
		protected sealed override bool IsAvaibleToAddMembers()
		{
			return true;
		}
		
		protected sealed override bool IsAvaibleToCreateUsers()
		{
			return true;
		}
		protected sealed override bool IsAvaibleToDeleteUsers()
		{
			return true;
		}
		
	}
}
