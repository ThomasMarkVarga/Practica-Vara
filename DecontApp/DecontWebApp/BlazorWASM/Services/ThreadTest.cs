using System.Reflection.Metadata;

namespace BlazorWASM.Services
{
	public class ThreadTest
	{
		public int Method(int param)
		{
			int i = 1;
			while (i < 1000000 * param) i++ ;
			return i;
		}
	}
}
