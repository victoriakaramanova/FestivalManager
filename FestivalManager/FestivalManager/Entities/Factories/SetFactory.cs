using System;

using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;





namespace FestivalManager.Entities.Factories
{
	using Contracts;
	using Entities.Contracts;
	using Sets;

	public class SetFactory : ISetFactory
	{
		public ISet CreateSet(string name, string typeName)
		{
            Type type = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(x => x.Name == typeName);

            ISet set = (ISet)Activator.CreateInstance(type, name);

            return set;
		}
	}




}
