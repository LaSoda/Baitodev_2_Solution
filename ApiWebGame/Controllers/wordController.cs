using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWebGame.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class wordController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcom to Hangman Game, inster a number 0-9 to star to play");
        }

        String[] Words = { "css", "ios", "html", "android", "notebook", "windows", "mac", "javascript", "typescript", "python" };

        [HttpGet("{numero}")]
        public IActionResult Get(int numero)
        {
            var jsonResult = new SelectedWord();
            try
            {
                jsonResult = new SelectedWord() { wordLenght = Words[numero].Length };
            }
            catch (Exception)
            {
                jsonResult = new SelectedWord() { wordLenght = Words[0].Length };
            }
            var json = new JavaScriptSerializer().Serialize(jsonResult);
            
            return Ok($"[{json}]");
        }

        [HttpGet("{numero}/{palabra}")]
        public IActionResult Get(int numero,String palabra)
        {
            var jsonResult = new MoveResult();
            List<int> Position = new List<int>();
            if (palabra.Length>1)
            {
                if (palabra.Equals(Words[numero]))
                {
                    for (int i = 0; i < palabra.Length; i++)
                    {
                        Position.Add(i);
                    }
                    jsonResult = new MoveResult() { wordPosition = Position, isCorrect = true, isWin = true };

                }
                else
                {
                    jsonResult = new MoveResult() { wordPosition = Position, isCorrect = false, isWin = false };
                }

            }
            else
            {
                jsonResult = new MoveResult() { wordPosition = Position, isCorrect = false, isWin = false };

                for (int i = 0; i < Words[numero].Length; i++)
                {
                    if (Words[numero].ToCharArray()[i].ToString().Equals(palabra))
                    {
                        Position.Add(i);
                        jsonResult = new MoveResult() { wordPosition = Position, isCorrect = true, isWin = false };
                        
                    }
                }
            }
            var json = new JavaScriptSerializer().Serialize(jsonResult);
            return Ok($"[{json}]");
        }
    }
}
