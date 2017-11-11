using MessagePack;
using MessagePackCSharpStudy.Tutorial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagePackCSharpStudy
{
	class Program
	{
		static void Main(string[] args)
		{
			var basicSerialize = new BasicSerialize();
			basicSerialize.DoSerialize();
			
		}
	}
}
