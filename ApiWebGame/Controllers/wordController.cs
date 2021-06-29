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
            return Ok(json);
        }

        [HttpGet("{numero}/{palabra}")]
        public IActionResult Get(int numero,String palabra)
        {
            var jsonResult = new MoveResult();

            if (palabra.Length>1)
            {
                if (palabra.Equals(Words[numero]))
                {
                     jsonResult = new MoveResult() { wordPosition = 0, isCorrect = true, isWin = true };
                    
                }
                else
                {
                    jsonResult = new MoveResult() { wordPosition = -1, isCorrect = false, isWin = false };
                }

            }
            else
            {
                jsonResult = new MoveResult() { wordPosition = -1, isCorrect = false, isWin = false };
                
                for (int i = 0; i < Words[numero].Length; i++)
                {
                    if (Words[numero].ToCharArray()[i].ToString().Equals(palabra))
                    {
                        jsonResult = new MoveResult() { wordPosition = i, isCorrect = true, isWin = false };
                        break;
                    }
                }
            }
            var json = new JavaScriptSerializer().Serialize(jsonResult);
            return Ok(json);
        }
    }
}
