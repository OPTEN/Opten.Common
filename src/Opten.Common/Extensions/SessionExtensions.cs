using System;
using System.Web;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// The Session Extensions.
	/// </summary>
	public static class SessionExtensions
	{
		/// <summary>
		/// Determines whether the session contains key.
		/// </summary>
		/// <param name="httpSessionState">State of the HTTP session.</param>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public static bool ContainsKey(this HttpSessionStateBase httpSessionState, string key)
		{
			if (httpSessionState == null) return false;

			if (string.IsNullOrWhiteSpace(key)) return false;

			foreach (string sessionKey in httpSessionState.Keys)
				if (sessionKey.Equals(key, StringComparison.OrdinalIgnoreCase))
					return true;

			return false;
		}
	}
}