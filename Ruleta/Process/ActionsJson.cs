using Ruleta.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Ruleta.Process
{
    public class ActionsJson
    {
        string path = Directory.GetCurrentDirectory()+ "/Data/fileRoulette.json"; 
       
        public bool Create(List<Roulette> roulettes)
        {
            try
            {
                string jsondoc = JsonSerializer.Serialize(roulettes);
                File.WriteAllText(path, jsondoc);
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public List<Roulette> Get()
        {
            try
            {
                List<Roulette> roulettes = new List<Roulette>();
                List<Bet> bets = new List<Bet>();
                Roulette roulette = new Roulette();
                Bet bet = new Bet();
                bets.Add(bet);
                roulette.Bets = bets;
                roulettes.Add(roulette);

                if (File.Exists(path))
                {
                    string result = File.ReadAllText(path);
                    if (!string.IsNullOrEmpty(result.Trim()))
                    {
                        return JsonSerializer.Deserialize<List<Roulette>>(result);
                    }
                    else
                    {
                        return roulettes;
                    }
                }
                else
                {
                    if (Create(roulettes))
                    {
                        return roulettes;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
