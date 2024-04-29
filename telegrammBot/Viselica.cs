using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using telegrammBot;

namespace telegrammBot
{
    
    internal class Viselica
    {
        private string Word;
        private string FindWord;
        private List<string> WordList = new List<string>() { "апельсин", "банан", "велосипед", "гитара", "домик", "ежик", "зонт", "игрушка", "котенок", 
            "луна", "мороженое", "носок", "огонь", "пицца", "робот", "солнце", "телефон", 
            "учебник", "фонарь", "холодильник", "цветок", "шарик", "щенок", "электрокар" };

        private async void Play(Update update, ITelegramBotClient botClient, CancellationToken cancellationToken)
        {
            while (true)
            {
                if (update.Message.Text.Length != 3)
                {
                    await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                text: "Извините, попробуйте снова",
                cancellationToken: cancellationToken);

                }
                else
                {
                    await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                    text: "У вас получилось 👏🏽👏🏽",
                    cancellationToken: cancellationToken);
                    
                }
            }
        }


        public Viselica(Update update, ITelegramBotClient botClient, CancellationToken cancellationToken) { 
            Random random = new Random();
            string temp = System.IO.File.ReadAllText("WordList.json");
            Word = WordList.ElementAt(random.Next()% WordList.Count);
            foreach (var item in Word)
            {
                FindWord += '_';
            }
            Play(update, botClient,  cancellationToken);
        }   

    }
}
