using System;

namespace infrastructure.data
{
    class Program {
		static void Main(string[] args) {
			Console.WriteLine("Creando la DB si no existe...⌛");
			SocialNetworkContext db = new SocialNetworkContext();
			db.Database.EnsureCreated();
			Console.WriteLine("¡Listo!✔️");
			Console.ReadKey();
		}
	}
}
