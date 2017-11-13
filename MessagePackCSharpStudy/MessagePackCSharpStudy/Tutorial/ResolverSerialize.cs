using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagePackCSharpStudy.Tutorial
{
	// attribute付けない版
	public class ContractlessSample
	{
		public int MyProperty1 { get; set; }
		public int MyProperty2 { get; set; }
	}

	[MessagePackObject]
	public class PrivateSample
	{
		[Key(0)]
		int x;

		public void SetX(int v)
		{
			x = v;
		}

		public int GetX()
		{
			return x;
		}
	}

	class ResolverSerialize
	{
		public void DoResolverSerialize()
		{
			var data = new ContractlessSample { MyProperty1 = 99, MyProperty2 = 9999 };
			// 引数にIFormatterResolverを設定出来、attributeなしで利用している。
			var bin = MessagePackSerializer.Serialize(data, MessagePack.Resolvers.ContractlessStandardResolver.Instance);

			// {"MyProperty1":99,"MyProperty2":9999}
			var json = MessagePackSerializer.ToJson(bin);
			var test = MessagePackSerializer.Serialize(json);
			Console.WriteLine(json);

			// 予めIFormatterResolverを設定する。
			MessagePackSerializer.SetDefaultResolver(MessagePack.Resolvers.ContractlessStandardResolver.Instance);

			// serializable.
			var bin2 = MessagePackSerializer.Serialize(data);

			// パブリックフィールドしかデフォルト設定出来ないが、設定でprivateも可能
			var privateData = new PrivateSample();
			privateData.SetX(9999);

			// You can choose StandardResolverAllowPrivate or  ContractlessStandardResolverAllowPrivate
			var privateBin = MessagePackSerializer.Serialize(data, MessagePack.Resolvers.DynamicObjectResolverAllowPrivate.Instance);
		}
	}
}
