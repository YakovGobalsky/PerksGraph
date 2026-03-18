// time stuff - get and parse to string

namespace Opaq.Utils {
	public static class IngameTime {

		//in unix time
		public static int Now => (int)(System.DateTime.Now.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;

	}
}