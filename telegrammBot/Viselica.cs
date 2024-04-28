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

namespace telegrammBot
{
    static class MyClass
    {
       public static List<string> WordList {get; set;}
    }
    internal class Viselica
    {
        private string Word;
        private string FindWord;

        private async void Play(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken) {
            while (true)
            {
                if (message.Text.Length != 1)
                {
                    await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                text: "Извините, попробуйте снова",
                cancellationToken: cancellationToken);

                }
                else {
                    await botClient.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                    text: "У вас получилось 👏🏽👏🏽",
                    cancellationToken: cancellationToken);
                }
            }
        }


        public Viselica(Message message, ITelegramBotClient botClient, CancellationToken cancellationToken) { 
            Random random = new Random();
            string temp = System.IO.File.ReadAllText("WordList.json");
            MyClass.WordList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(temp);
            Word = MyClass.WordList.ElementAt(random.Next()% MyClass.WordList.Count);
            foreach (var item in Word)
            {
                FindWord += '_';
            }
            Play( message, botClient,  cancellationToken);
        }   

    }
}
