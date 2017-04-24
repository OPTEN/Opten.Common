using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// The Assembly Extensions.
	/// </summary>
	public static class AssemblyExtensions
	{

		/// <summary>
		/// Gets the types implementing.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="assembly">The assembly.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">assembly</exception>
		public static IEnumerable<Type> GetTypesImplementing<T>(this Assembly assembly)
		{
			if (assembly == null) throw new ArgumentNullException("assembly");

			return
				from tp in assembly.GetExportedTypes()
				where typeof(T).IsAssignableFrom(tp)
				select tp;
		}

		/// <summary>
		/// Gets the types with attribute.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="assembly">The assembly.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">assembly</exception>
		public static IEnumerable<Type> GetTypesWithAttribute<T>(this Assembly assembly) where T : Attribute
		{
			if (assembly == null) throw new ArgumentNullException("assembly");

			return
				from a in assembly.GetExportedTypes()
				where a.HasCustomAttribute<T>()
				select a;
		}

	}
}