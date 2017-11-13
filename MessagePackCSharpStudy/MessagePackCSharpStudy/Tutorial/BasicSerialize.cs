using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagePackCSharpStudy.Tutorial
{
	[MessagePackObject]
	public class Sample1
	{
		[Key(0)]
		public int Foo { get; set; }
		[Key(1)]
		public int Bar { get; set; }
	}

	[MessagePackObject]
	public class Sample2
	{
		[Key("foo")]
		public int Foo { get; set; }
		[Key("bar")]
		public int Bar { get; set; }
	}

	[MessagePackObject(keyAsPropertyName: true)]
	public class Sample3
	{
		// no needs KeyAttribute
		public int Foo { get; set; }

		// If ignore public member, you can use IgnoreMemberAttribute
		[IgnoreMember]
		public int Bar { get; set; }
	}

	// 番外編 普通書こうと思わないけれど
	[MessagePackObject]
	public class IntKeySample
	{
		[Key(3)]
		public int A { get; set; }
		[Key(10)]
		public int B { get; set; }
	}


	public class BasicSerialize
	{
		public void DoSerialize()
		{
			// [10,20]
			Console.WriteLine(MessagePackSerializer.ToJson(new Sample1 { Foo = 10, Bar = 20 }));

			// {"foo":10,"bar":20}
			Console.WriteLine(MessagePackSerializer.ToJson(new Sample2 { Foo = 10, Bar = 20 }));
			var data = new Sample2 { Foo = 10, Bar = 20 };
			var bin = MessagePackSerializer.Serialize(data);
			
			// バイナリから型にデシリアライズ
			var classObject = MessagePackSerializer.Deserialize<Sample2>(bin);
			// バイナリからdynamicにデシリアライズ
			var dynamicObject = MessagePackSerializer.Deserialize<dynamic>(bin);

			// {"Foo":10}
			// 文字列の方がコストかかるけれど、色がつく
			Console.WriteLine(MessagePackSerializer.ToJson(new Sample3 { Foo = 10, Bar = 20 }));

			// 番外編 キーで指定した個所に保持されてしまう。
			// [null,null,null,0,null,null,null,null,null,null,0]
			Console.WriteLine(MessagePackSerializer.ToJson(new IntKeySample()));

		}
	}
}
