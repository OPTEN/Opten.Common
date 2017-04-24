using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Opten.Common.Helpers
{
	/// <summary>
	/// The Activation Helper.
	/// </summary>
	public static class ActivatorHelper
	{

		/// <summary>
		/// Creates an instance of a type using that type's default constructor.
		/// </summary>
		/// <typeparam name="T">The type of instance to create</typeparam>
		/// <returns>
		/// An instantiation of T
		/// </returns>
		public static T CreateInstance<T>() where T : class, new()
		{
			return Activator.CreateInstance(typeof(T)) as T;
		}

		/// <summary>
		/// Creates an instance of a type using a constructor with specific arguments
		/// </summary>
		/// <typeparam name="T">The <see cref="Type" /> or base class</typeparam>
		/// <param name="constructorArguments">Object array containing constructor arguments</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">Failed to create Type due to null Type or null constructor args</exception>
		public static T CreateInstance<T>(object[] constructorArguments) where T : class
		{
			if (constructorArguments == null)
				throw new ArgumentNullException("Failed to create Type due to null Type or null constructor args");

			Type type = typeof(T);
			Assembly assembly = type.Assembly;
			const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			List<Type> constructorArgumentTypes = constructorArguments.Select(value => value.GetType()).ToList();

			ConstructorInfo constructor = type.GetConstructor(bindingFlags, null, CallingConventions.Any, constructorArgumentTypes.ToArray(), null);

			return constructor.Invoke(constructorArguments) as T;
		}

		/// <summary>
		/// Gets the property value of an instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">Instance is null</exception>
		/// <exception cref="System.NullReferenceException">Type name is null</exception>
		public static T GetPropertyValueOfInstance<T>(object instance, string propertyName)
		{
			if (instance == null) throw new ArgumentNullException("Instance is null");
			if (string.IsNullOrWhiteSpace(propertyName)) return default(T);

			PropertyInfo property = instance.GetType().GetProperty(propertyName);

			return (T)property.GetValue(instance);
		}

		/// <summary>
		/// Gets the private method return value of an instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="methodArguments">The method arguments.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">Instance is null
		/// or
		/// Method arguments are null</exception>
		public static T GetPrivateMethodReturnValueOfInstance<T>(object instance, string methodName, object[] methodArguments)
		{
			if (instance == null) throw new ArgumentNullException("Instance is null");
			if (string.IsNullOrWhiteSpace(methodName)) return default(T);
			if (methodArguments == null) throw new ArgumentNullException("Method args are null");
			
			MethodInfo method = instance.GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

			return (T)method.Invoke(obj: instance, parameters: methodArguments);
		}

		/// <summary>
		/// Gets the private method return value from a static class.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="typeName">Name of the type.</param>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="methodArguments">The method arguments.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">Type name is null
		/// or
		/// Method arguments are null</exception>
		public static T GetPrivateMethodReturnValueFromStaticClass<T>(string typeName, string methodName, object[] methodArguments)
		{
			if (string.IsNullOrWhiteSpace(typeName)) throw new ArgumentNullException("Type name is null");
			if (string.IsNullOrWhiteSpace(methodName)) return default(T);
			if (methodArguments == null) throw new ArgumentNullException("Method args are null");

			return GetPrivateMethodReturnValueFromStaticClass<T>(Type.GetType(typeName), methodName, methodArguments);
		}

		/// <summary>
		/// Gets the private method return value from a static class.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="type">The type.</param>
		/// <param name="methodName">Name of the method.</param>
		/// <param name="methodArguments">The memthod arguments.</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">Type is null</exception>
		public static T GetPrivateMethodReturnValueFromStaticClass<T>(Type type, string methodName, object[] methodArguments)
		{
			if (type == null) throw new ArgumentNullException("Type is null");
			if (string.IsNullOrWhiteSpace(methodName)) return default(T);
			if (methodArguments == null) throw new ArgumentNullException("Method args are null");

			MethodInfo method = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);

			return (T)method.Invoke(obj: null, parameters: methodArguments);
		}

	}
}