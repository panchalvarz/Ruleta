using Ruleta.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ruleta.Process
{
	public class ActionsRoulette
	{
		
		public bool SaveRoulette(Roulette roulettein)
		{
			try
			{
				ActionsJson actionsJson = new ActionsJson();
				List<Roulette> roulettes = actionsJson.Get();
				Roulette roulette = roulettes.FirstOrDefault(r => r.Id.Equals(roulettein.Id));
				if (roulette != null)
				{
					roulettes.Remove(roulette);

				}
				roulettes.Add(roulettein);
				return actionsJson.Create(roulettes);
			}
			catch (Exception)
			{

				throw;
			}
		}

		public Roulette GetRoulette(Guid id)
		{
			try
			{
				ActionsJson actionsJson = new ActionsJson();
				List<Roulette> roulettes = actionsJson.Get();
				Roulette roulette = roulettes.FirstOrDefault(r => r.Id.Equals(id));
				if (roulette == null)
				{
					return new Roulette
					{
						Id = id,
						Bets = new List<Bet>()
					};
				}
				return roulette;
			}
			catch (Exception)
			{

				throw;
			}
		}


		public List<Roulette> GetRoulettes()
		{
			try
			{
				ActionsJson actionsJson = new ActionsJson();
				return actionsJson.Get();
			}
			catch (Exception)
			{

				throw;
			}
		}

	}
}
