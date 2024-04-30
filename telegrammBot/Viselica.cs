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

    internal  class Viselica
    {
        private string Word;
        private string FindWord {
            get => FindWord;
            
            set {
                foreach (var item in value)
                {
                    FindWord += '_';
                }
            } }
        private List<string> WordList = new List<string>() { "апельсин", "банан", "велосипед", "гитара", "домик", "ежик", "зонт", "игрушка", "котенок", 
            "луна", "мороженое", "носок", "огонь", "пицца", "робот", "солнце", "телефон", 
            "учебник", "фонарь", "холодильник", "цветок", "шарик", "щенок", "электрокар" };
        public async         Task
Play(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message.Text == "🕹️")
            {
                await botClient.SendTextMessageAsync(
                            chatId: update.Message.Chat.Id,
                        text: $"Искомое слово {FindWord}",
                        cancellationToken: cancellationToken);
            }
            else
            {

                if (update.Message.Text.Length > 1)
                {
                    await botClient.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                text: "Извините, попробуйте снова",
                cancellationToken: cancellationToken);


                }
                else
                {
                    FindWord[Word.Any(x=> x== update.Message.Text[0])] = update.Message.Text;
                    await botClient.SendTextMessageAsync(
                        chatId: update.Message.Chat.Id,
                    text: $"Искомое слово {FindWord}",
                    cancellationToken: cancellationToken);

                }
            }
            
        }


        public Viselica() { 
            Random random = new Random();
            string temp = System.IO.File.ReadAllText("WordList.json");
            Word = WordList.ElementAt(random.Next()% WordList.Count);
            
       
           


        }   

    }
}
