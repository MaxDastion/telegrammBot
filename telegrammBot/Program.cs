using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using telegrammBot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Args;


class MyMain
{
    private static Dictionary<long, string> userModes = new Dictionary<long, string>();

    public static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
    async  static Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        // Only process Message updates: https://core.telegram.org/bots/api#message
        if (update.Message is not { } message)
            return;
        // Only process text messages
        if (message.Text is not { } messageText)
            return;

        var chatId = message.Chat.Id;

            ReplyKeyboardMarkup replyKeyboardMarkupNormal = new(new[]{new KeyboardButton[] { "Normal", "🕹️" },})
            {
                ResizeKeyboard = true
            };




        var viselica = new Viselica();
        if (message.Text == "🕹️")
        {
            viselica = new Viselica();
            await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "Режим игра",
            cancellationToken: cancellationToken,
            replyMarkup: replyKeyboardMarkupNormal);
            userModes[chatId] = "Game";
        }
        if (message.Text == "Normal")
        {
            await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "обычный режим",
            cancellationToken: cancellationToken,
            replyMarkup: replyKeyboardMarkupNormal);
            userModes[chatId] = "normal";
        }
        if (userModes != null)
        {
            #region NormaMode

            if (userModes[chatId] == "normal")
            {

                if (message.Text == "h")
                {





                    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

                    // Echo received message text
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Hello world",
                        cancellationToken: cancellationToken,
                        replyMarkup: replyKeyboardMarkupNormal);
                }
                if (message.Text == "picture")
                {
                    await botClient.SendPhotoAsync(
                chatId: message.Chat.Id,
                photo: InputFile.FromUri("https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg"),
                caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
                parseMode: ParseMode.Html,
                cancellationToken: cancellationToken);
                }

            }
            #endregion
            #region GameMode

            if (userModes[chatId] == "Game")
            {
                await viselica.Play(botClient, update, cancellationToken);
            }
            #endregion
        }


    }

    static async Task Main(string[] args)
    {
        var Boclient = new TelegramBotClient("7116243344:AAEpVO0KH-8lxZlzxSVRTGlaGQJPZ_ewQS0");
        var client = await Boclient.GetMeAsync();

        using CancellationTokenSource cts = new();

        // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
        };
        Boclient.StartReceiving(
        updateHandler: HandleUpdateAsync,
        pollingErrorHandler: HandlePollingErrorAsync,
        receiverOptions: receiverOptions,
        cancellationToken: cts.Token);

        var me = await Boclient.GetMeAsync();

        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();

        // Send cancellation request to stop bot
        cts.Cancel();



    }
}