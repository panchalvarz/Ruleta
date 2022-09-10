using Microsoft.Extensions.Configuration;
using Ruleta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ruleta.Process
{
	public class ActionsBet
	{
		private readonly IConfiguration Configuration;
		public ActionsBet(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public ActionsBet()
		{
			
		}
		public bool Savebet(Bet bet, Guid idroulette)
		{
			try
			{
				ActionsJson actionsJson = new ActionsJson();
				ActionsRoulette actionsRoulette = new ActionsRoulette();
				List<Roulette> roulettes = actionsJson.Get();
				Roulette roulette = roulettes.FirstOrDefault(r => r.Id.Equals(idroulette));
				if (roulette != null)
				{
					bet.ColorWin = false;
					bet.NumberWin = false;
					bet.Valuewin = 0;
					roulette.Bets.Add(bet);
					actionsRoulette.SaveRoulette(roulette);
					return true;
				}
				else
				{
					return false;
				}


			}
			catch (Exception)
			{

				throw;
			}
		}


		public List<Bet> EndRoulette(Guid id)
		{
			try
			{
				ActionsJson actionsJson = new ActionsJson();
				List<Roulette> roulettes = actionsJson.Get();
				Roulette roulette = roulettes.FirstOrDefault(r => r.Id.Equals(id));
				if (roulette != null)
				{
					Random random = new Random();
					int numberwin = random.Next(Convert.ToInt32(Configuration["rouletteMax"]));
					string colorWin = GetColorWin(numberwin);
					foreach (var bet in roulette.Bets)
					{
						double valuecolor = bet.value * Convert.ToDouble(Configuration["valueColorWin"]);
						double valuenumber = bet.value * Convert.ToDouble(Configuration["valueNumWim"]);

						if (bet.Number.Equals(numberwin) && bet.Color.Equals(colorWin))
						{
							bet.Valuewin = valuecolor + valuenumber;
							bet.NumberWin = bet.ColorWin = true;
						}
						else
						{
							if (bet.Number.Equals(numberwin))
							{
								bet.Valuewin = valuenumber;
								bet.NumberWin = true;
							}
							if (bet.Color.ToUpper().Equals(colorWin))
							{
								bet.Valuewin = valuenumber;
								bet.ColorWin = true;
							}
						}

					}
				}
				return roulette.Bets;
			}
			catch (Exception)
			{

				throw;
			}
		}


		public string GetColorWin(int numberWin)
		{
			if (numberWin % 2 == 0)
				return "ROJO";
			else
				return "NEGRO";
		}
	}
}
