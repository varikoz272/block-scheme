using System;
using System.Drawing;
using System.Linq;

namespace Block.manage
{
	public class Easel
	{
		public static readonly Color ATTENTION_COLOR = Color.FromArgb(255, 39, 24, 126);
		public static readonly Color BG_COLOR = Color.FromArgb(255, 174, 184, 254);
		public static readonly Color MAIN_COLOR = Color.FromArgb(255, 117, 139, 253);
		public static readonly Color OUT_COLOR = Color.FromArgb(255, 241, 242, 246);
		public static readonly Color ALT_COLOR = Color.FromArgb(255, 255, 134, 0);
		
		public static readonly Color DARK_MAIN_COLOR = Color.FromArgb(255, 72, 79, 208);
		public static readonly Color DARK_ALT_COLOR = Color.FromArgb(255, 205, 84, 0);
		
		public static Color GetDarker(Color color)
		{
			if (color.Equals(MAIN_COLOR))
				return DARK_MAIN_COLOR;
			
			if (color.Equals(ALT_COLOR))
				return DARK_ALT_COLOR;
			
			return color;
		}
	}
}
