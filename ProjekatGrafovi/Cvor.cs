using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatGrafovi
{
	public class Cvor
	{
		public int Id { get; set; }
		public int X { get; set; }
		public int Y { get; set; }

		public Cvor(int id, int x, int y)
		{
			Id = id;
			X = x;
			Y = y;
		}

		public Cvor()
		{
		}
	}
}
