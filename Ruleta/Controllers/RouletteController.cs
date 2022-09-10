using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ruleta.Models;
using Ruleta.Process;
using System;
using System.Collections.Generic;

namespace Ruleta.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RouletteController : ControllerBase
	{
		private readonly IConfiguration Configuration;

		public RouletteController(IConfiguration configuration)
		{
			Configuration = configuration;
		}



		[HttpPost("~/CreateRoulette")]
		public Guid CreateRoulette()
		{
			ActionsRoulette actions = new ActionsRoulette();
			Guid id = Guid.NewGuid();
			Roulette roulette = actions.GetRoulette(id);
			if (actions.SaveRoulette(roulette))
			{
				return id;
			}
			return Guid.Empty;
		}

		[HttpPost("~/StartRoulette")]
		public string StartRoulette(Guid id)
		{
			ActionsRoulette actions = new ActionsRoulette();
			Roulette roulette = actions.GetRoulette(id);
			if (roulette != null)
			{
				roulette.State = true;
				if (actions.SaveRoulette(roulette))
				{
					return "Apertura de ruleta exitosa";
				}
			}
			else
			{
				return "Error al inicar la ruleta: No existe";
			}
			return string.Empty;
		}

		[HttpPost("~/ListRoulette")]
		public List<Roulette> ListRoulette()
		{
			ActionsRoulette actions = new ActionsRoulette();
			return actions.GetRoulettes();
		}




		[HttpPost("~/CreateBet")]
		public string CreateBet(Guid idroulette, Bet bet)
		{
			string response = string.Empty;
			ActionsRoulette actions = new ActionsRoulette();
			ActionsBet actionsBet = new ActionsBet();
			Roulette roulette = actions.GetRoulette(idroulette);

			if (roulette.State && roulette.Id.Equals(idroulette))
			{
				int nu = Convert.ToInt32(Configuration["rouletteMin"]);
				if (bet?.Number >= Convert.ToInt32(Configuration["rouletteMin"]) &&
					bet?.Number <= Convert.ToInt32(Configuration["rouletteMax"]) &&
					bet?.value <= Convert.ToInt32(Configuration["valueMaxbet"]) &&
					(bet.Color.ToUpper().Equals("NEGRO") || bet.Color.ToUpper().Equals("ROJO")))
				{
					if (actionsBet.Savebet(bet, idroulette))
						response = "Apuesta Creada";
					else
						response = "No se genero apuesta";
				}
				else
				{
					response = "los números válidos para apostar son del 0 al 36 " +
							   " Color :NEGRO o ROJO, el valor máximo por apuesta : 10000";
				}
			}
			else
			{
				response = "La ruleta no esta abierta";
			}

			return response;
		}



		[HttpPost("~/endRoulette")]
		public List<Bet> EndRoulette(Guid id)
		{
			ActionsRoulette actions = new ActionsRoulette();
			ActionsBet actionsBet = new ActionsBet(Configuration);
			Roulette roulette = new Roulette();
			roulette.State = false;
			roulette.Bets = actionsBet.EndRoulette(id);
			actions.SaveRoulette(roulette);
			return roulette.Bets;
		}
	}
}
