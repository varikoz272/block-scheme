
using System;

namespace Block.user
{
	public class Tutor : User
	{
		public Tutor(int id, string name, string password) : base(id, name, password) { }
		
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
			return false;
		}
		protected sealed override bool IsAvaibleToDeleteUsers()
		{
			return false;
		}
	}
}
