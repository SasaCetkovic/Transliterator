using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transliterator
{
	class DefaultTransliterator : ITransliterator
	{
		private static Dictionary<string, string> _exceptions;
		private static Dictionary<string, string> _pairsLatCyr;
		private static Dictionary<string, string> _singlesLatCyr;
		private static Dictionary<string, string> _singlesCyrLat;

		static DefaultTransliterator()
		{
			Populate();
			PopulatePairs();
			PopulateSingleLetters();
		}


		#region -- ITransliterator Implementation --

		public string ToCyrillic(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				return text;
			}

			var sb = new StringBuilder(text);

			foreach (var item in _exceptions)
			{
				sb.Replace(item.Key, item.Value);
			}

			foreach (var item in _pairsLatCyr)
			{
				sb.Replace(item.Key, item.Value);
			}

			foreach (var item in _singlesLatCyr)
			{
				sb.Replace(item.Key, item.Value);
			}

			return sb.ToString();
		}

		public string ToLatin(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				return text;
			}

			var sb = new StringBuilder(text);

			foreach (var item in _singlesCyrLat)
			{
				sb.Replace(item.Key, item.Value);
			}

			return sb.ToString();
		}

		#endregion


		#region -- Helpers --

		private static void Populate()
		{
			_exceptions = new Dictionary<string, string>
			{
				["nadživ"] = "наджив",
				["Nadživ"] = "Наджив",
				["NADŽIV"] = "НАДЖИВ",

				["injekc"] = "инјекц",
				["Injekc"] = "Инјекц",
				["INJEKC"] = "ИНЈЕКЦ",

				["konjug"] = "конјуг",
				["Konjug"] = "Конјуг",
				["KONJUG"] = "КОНЈУГ",

				["odžal"] = "оджал",
				["Odžal"] = "Оджал",
				["ODŽAL"] = "ОДЖАЛ",

				["podžar"] = "поджар",
				["Podžar"] = "Поджар",
				["PODŽAR"] = "ПОДЖАР",

				["konjun"] = "конјун",
				["Konjun"] = "Конјун",
				["KONJUN"] = "КОНЈУН",

				["odživ"] = "оджив",
				["Odživ"] = "Оджив",
				["ODŽIV"] = "ОДЖИВ",

				["injekt"] = "инјект",
				["Injekt"] = "Инјект",
				["INJEKT"] = "ИНЈЕКТ",

				["podžanr"] = "поджанр",
				["Podžanr"] = "Поджанр",
				["PODŽANR"] = "ПОДЖАНР",

				["nadžanr"] = "наджанр",
				["Nadžanr"] = "Наджанр",
				["NADŽANR"] = "НАДЖАНР",

				["podžarg"] = "поджарг",
				["Podžarg"] = "Поджарг",
				["PODŽARG"] = "ПОДЖАРГ",

				["tanjug"] = "Танјуг",
				["Tanjug"] = "Танјуг",
				["TANJUG"] = "ТАНЈУГ"
			};
		}

		private static void PopulatePairs()
		{
			_pairsLatCyr = new Dictionary<string, string>
			{
				["lj"] = "љ",   // lj
				["Lj"] = "Љ",
				["LJ"] = "Љ",
				["lJ"] = "Љ",
				["nj"] = "њ",   // nj
				["Nj"] = "Њ",
				["NJ"] = "Њ",
				["nJ"] = "Њ",
				["dž"] = "џ",   // dž
				["Dž"] = "Џ",
				["DŽ"] = "Џ",
				["dŽ"] = "Џ"
			};
		}

		private static void PopulateSingleLetters()
		{
			_singlesLatCyr = new Dictionary<string, string>
			{
				["a"] = "а",
				["A"] = "А",
				["b"] = "б",
				["B"] = "Б",
				["c"] = "ц",
				["C"] = "Ц",
				["č"] = "ч",
				["Č"] = "Ч",
				["ć"] = "ћ",
				["Ć"] = "Ћ",
				["d"] = "д",
				["D"] = "Д",
				["đ"] = "ђ",
				["Đ"] = "Ђ",
				["e"] = "е",
				["E"] = "Е",
				["f"] = "ф",
				["F"] = "Ф",
				["g"] = "г",
				["G"] = "Г",
				["h"] = "х",
				["H"] = "Х",
				["i"] = "и",
				["I"] = "И",
				["j"] = "ј",
				["J"] = "Ј",
				["k"] = "к",
				["K"] = "К",
				["l"] = "л",
				["L"] = "Л",
				["m"] = "м",
				["M"] = "М",
				["n"] = "н",
				["N"] = "Н",
				["o"] = "о",
				["O"] = "О",
				["p"] = "п",
				["P"] = "П",
				["r"] = "р",
				["R"] = "Р",
				["s"] = "с",
				["S"] = "С",
				["š"] = "ш",
				["Š"] = "Ш",
				["t"] = "т",
				["T"] = "Т",
				["u"] = "у",
				["U"] = "У",
				["v"] = "в",
				["V"] = "В",
				["z"] = "з",
				["Z"] = "З",
				["ž"] = "ж",
				["Ž"] = "Ж"
			};

			_singlesCyrLat = _singlesLatCyr.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
			_singlesCyrLat.Add("џ", "dž");
			_singlesCyrLat.Add("Џ", "Dž");
			_singlesCyrLat.Add("љ", "lj");
			_singlesCyrLat.Add("Љ", "Lj");
			_singlesCyrLat.Add("њ", "nj");
			_singlesCyrLat.Add("Њ", "Nj");
		}

		#endregion
	}
}
