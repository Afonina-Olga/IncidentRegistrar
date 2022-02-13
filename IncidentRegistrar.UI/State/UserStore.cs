using System;

using IncidentRegistrar.UI.Models;

namespace IncidentRegistrar.UI.State
{
	public class UserStore : IUserStore
	{
		public event Action StateChanged;

		private User _user;
		public User User
		{
			get { return _user; }
			set
			{
				_user = value;
				StateChanged?.Invoke();
			}
		}
	}
}
