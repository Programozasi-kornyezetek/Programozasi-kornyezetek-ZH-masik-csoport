using System;

namespace Programozasi_kornyezetek_ZH_masik_csoport {
	class Naposcsibe {
		private string azonosito;

		internal struct TojasSzuletesEvHoNap {
			private int ev;
			private int ho;
			private int nap;

			public TojasSzuletesEvHoNap(int ev, int ho, int nap) {
				this.ev = ev;
				this.ho = ho;
				this.nap = nap;
			}

			public int Ev {
				get => ev;
				set => ev = value;
			}

			public int Ho {
				get => ho;
				set => ho = value;
			}

			public int Nap {
				get => nap;
				set => nap = value;
			}
		}

		CsibeFajta fajta;

		private DateTime tojasSzuletes;
		private TojasSzuletesEvHoNap tojasSzuletesEvHoNap;

		private int tojasKora;

		public bool kikelt;

		public Naposcsibe() {
		}

		public Naposcsibe(string azonosito, CsibeFajta fajta, DateTime tojasSzuletes) {
			this.azonosito = azonosito;
			this.fajta = fajta;
			this.tojasSzuletes = tojasSzuletes;
			this.tojasKora = tojasKoraNap();

			Keltetes keltetes = new Keltetes(10);
			keltetes.ThresholdReached += idejeKeltetni;
			keltetes.Add(tojasKora);
		}

		public Naposcsibe(string azonosito, CsibeFajta fajta, TojasSzuletesEvHoNap tojasSzuletesEvHoNap) {
			this.azonosito = azonosito;
			this.fajta = fajta;
			this.tojasSzuletesEvHoNap = tojasSzuletesEvHoNap;
			this.TojasSzuletes =
				new DateTime(tojasSzuletesEvHoNap.Ev, tojasSzuletesEvHoNap.Ho, tojasSzuletesEvHoNap.Nap);
			this.tojasKora = tojasKoraNap();


			Keltetes keltetes = new Keltetes(10);
			keltetes.ThresholdReached += idejeKeltetni;
			keltetes.Add(tojasKora);
		}

		public string Azonosito {
			get => azonosito;
			set => azonosito = value;
		}

		public CsibeFajta Fajta {
			get => fajta;
			set => fajta = value;
		}

		public DateTime TojasSzuletes {
			get => tojasSzuletes;
			set {
				tojasSzuletes = value;
				this.tojasKora = tojasKoraNap();
			}
		}

		public TojasSzuletesEvHoNap TojasSzuletese {
			get => tojasSzuletesEvHoNap;
			set {
				tojasSzuletesEvHoNap = value;
				this.tojasSzuletes = new DateTime(tojasSzuletesEvHoNap.Ev, tojasSzuletesEvHoNap.Ho,
					tojasSzuletesEvHoNap.Nap);
				this.tojasKora = tojasKoraNap();
			}
		}

		public int TojasKora {
			get => tojasKora;
			set => tojasKoraNap();
		}

		public int tojasKoraNap() {
			TimeSpan interval = DateTime.Now - this.TojasSzuletes;

			Keltetes keltetes = new Keltetes(10);
			keltetes.ThresholdReached += idejeKeltetni;
			keltetes.Add(interval.Days);

			return interval.Days;
		}

		void idejeKeltetni(object sender, EventArgs e) {
			kikelt = true;
			//Console.WriteLine("Keltetes!");
		}

		public override string ToString() {
			return azonosito + " " + fajta + " " + tojasSzuletes.Year + "." + tojasSzuletes.Month + "." + tojasSzuletes.Day + ". " + tojasKora + " napos " + (kikelt?"kikelt":"nem kelt ki");
		}
	}
}