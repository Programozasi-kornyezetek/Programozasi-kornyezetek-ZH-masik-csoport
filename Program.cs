using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Programozasi_kornyezetek_ZH_masik_csoport {
	internal class Program {
		public static void WriteToFile(string path, string text)
		{
			File.WriteAllText(path, text);
		}
		
		public static void Main(string[] args) {
			Naposcsibe naposcsibe = new Naposcsibe();
			naposcsibe.Azonosito = "1";
			//naposcsibe.TojasSzuletese = new Naposcsibe.TojasSzuletesEvHoNap(2020,2,3);
			naposcsibe.TojasSzuletes = new DateTime(2021, 5, 3);
			//Console.WriteLine(naposcsibe.Azonosito + " " + naposcsibe.TojasKora + " " + naposcsibe.kikelt);

			Naposcsibe naposcsibe2 = new Naposcsibe("2", CsibeFajta.feher, new DateTime(2021, 5, 1));
			//Console.WriteLine(naposcsibe2.Azonosito + " " + naposcsibe2.TojasKora + " " + naposcsibe2.kikelt);

			Dictionary<string, Naposcsibe> tojasok = new Dictionary<string, Naposcsibe>();

			for (int i = 0; i < 10; i++) {
				string azonosito = i.ToString();
				Array csibefajtak = Enum.GetValues(typeof(CsibeFajta));
				Random random = new Random();
				tojasok.Add(azonosito,
					new Naposcsibe(azonosito, (CsibeFajta) csibefajtak.GetValue(random.Next(csibefajtak.Length)),
						new DateTime(random.Next(2021,2021), random.Next(5,5), random.Next(1,7))));
			}

			Console.WriteLine("Kikelt:");
			foreach (var t in tojasok) {
				if (t.Value.kikelt)
					Console.WriteLine(t.Value.ToString());
			}
			Console.WriteLine("Nem kelt ki:");
			foreach (var t in tojasok) {
				if (! t.Value.kikelt)
					Console.WriteLine(t.Value.ToString());
			}
			
			
			var max5nap = tojasok.Values.Where(x => x.tojasKoraNap() <= 5).ToList();
			Console.WriteLine("5 napos csirkék:");
			foreach (var cs in max5nap) {
				Console.WriteLine(cs.ToString());
			}

			var kendermagos = tojasok.Values.Where(x => x.Fajta.ToString().Equals("Kendermagos")).ToList().Count();
			Console.WriteLine("Kendermagosok száma: " + kendermagos);
			var tipusonkent = tojasok.Values.GroupBy(x => x.Fajta).Select(group => new
			{
				Fajta = group.Key,
				Count = group.Count()
			});
			foreach (var cs in tipusonkent) {
				Console.WriteLine(cs.Fajta + " : " + cs.Count);
			}

			string textToFile = "";
			foreach (var t in tojasok) {
				textToFile += t.Value.ToString() + "\n";
			}
			WriteToFile("csirkek.txt", textToFile);
		}
	}
}