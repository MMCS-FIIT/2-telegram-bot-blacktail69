using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
namespace MyTelegramBot;
class Program    //my bot's username: @Blacktail69_bot
{
    /// <summary>
    /// Token for tg bot activation
    /// </summary>
    private static readonly string token = "8683520625:AAFJLs5nK3ZPww8Amugb5UHklMRzgR7MBls";
    
    private static TelegramBotClient client;

    //introduction
    private static string intro = "Тикки, давай! 🐾 Или Плагг, когти? 🐞\nПривет! Я — Miraculous - твой личный помощник в мире супергероев Парижа. Готов узнать, чьим Талисманом ты бы владел на самом деле? /nЖми кнопку тест, чтобы начать проверку!";

    //possible answers
    private static string[] answers = ["test", "тест", "пройти тест", "хочу пройти тест", "погнали","вперёд","начинай","согласен","поехали","стартуем","го","я за","окей","идёт","ладненько","давай", "тикки, давай"];
    private static string[] test_starters = ["test", "тест", "пройти тест", "хочу пройти тест", "погнали",
    "вперёд","начинай","согласен","поехали","стартуем","го","я за","окей","идёт","ладненько","давай"];

    static void Main(string[] args)
    {
        client = new TelegramBotClient(token); 
        client.StartReceiving(Update, Error);
        Console.ReadLine();
    }


    //answers user
    private static async Task Update(ITelegramBotClient client, Update update, CancellationToken token)
    {
        var msg = update.Message;
        if (msg != null)
        {
            //  Обработка команды /start
            if (update.Message is { Text: "/start" } message)
            {
                var keyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[] { InlineKeyboardButton.WithCallbackData("Пройти тест") },
                        new[] { InlineKeyboardButton.WithCallbackData("Я не смотрел ;;") }
                    });

                await client.SendTextMessageAsync(msg.Chat.Id, intro, replyMarkup: keyboard);
                return;
                
            }

            //  обработка нажатий на кнопки /start
            if (update.CallbackQuery is { } callbackQuery)
            {
                
                string action = callbackQuery.Data;
                long chatId = callbackQuery.Message.Chat.Id;

                if (action == "Пройти тест")
                {
                    await client.SendTextMessageAsync(chatId, "Отличный выбор!");
                    
                }
                else if (action == "Я не смотрел ;;")
                {
                    await client.SendTextMessageAsync(chatId, "Ты многое теряешь! Обязательно посмотри и возвращайся ко мне!🐞");
                }

                await client.AnswerCallbackQueryAsync(callbackQuery.Id);
            }

            
        }

            

            //await client.SendTextMessageAsync(msg.Chat.Id, msg.Text, replyToMessageId: msg.MessageId); //replyes to the message with the same text, the third param is optional, "marks" the message
            if (msg.Text == "hello")
            {
                var stic = await client.SendStickerAsync(
                    chatId: msg.Chat.Id,
                    sticker: "https://assets.stickerswiki.app/s/xonqizivaabjirmushukkanli/c7bd2527.thumb.webp"
                );
            }
            //await client.SendTextMessageAsync(msg.Chat.Id, msg.Text, replyMarkup: GetButtons());
        }
        
    

    //buttons
    private static IReplyMarkup? GetButtons()
    {
        return new ReplyKeyboardMarkup(new[]
        {
            // Первый ряд кнопок
            new[]
            {
                new KeyboardButton("Пройти тест"),
                new KeyboardButton("Я не смотрел ;;"),
            },
        })
        {
            ResizeKeyboard = true // Чтобы кнопки не были огромными
        };
    }

    //он просто есть
    private static async Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
