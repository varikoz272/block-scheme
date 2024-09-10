using System;
using System.Linq;

namespace Block.user
{

	public enum Action
	{
		CreateUser,
		DeleteCreatedUser,
		DeleteUser,
		
		CreateGroup,
		DeleteCreatedGroup,
		DeleteGroup,
		
		Chat,
		DeleteMessage,
	}
}
