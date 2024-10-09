/* GÄSTBOK 
av S.E.K på Mittuniversitetet */

using static System.Console;

namespace guestbook;

class Program
{
    /* --- FUNKTIONER --- */
    //Återanvänd funktion från tidigare moment för att lägga till lite färg
    static void printMessage(bool newLine, ConsoleColor color, string message)
    {
        //Kolla om meddelandet ska vara på egen rad eller inte
        if (newLine == true)
        {
            //Ändra färgen
            ForegroundColor = color;
            //Skriv ut meddelandet till skärmen
            WriteLine(message);
            //Nollställ färgen
            ResetColor();
        }
        else
        {
            ForegroundColor = color;
            //Samma rad
            Write(message);
            ResetColor();
        }
    }

    static void introText()
    {
        //Skriv ut gästbok i olika färger hehe
        printMessage(false, ConsoleColor.DarkMagenta, "G");
        printMessage(false, ConsoleColor.Blue, "Ä");
        printMessage(false, ConsoleColor.Cyan, "S");
        printMessage(false, ConsoleColor.Green, "T");
        printMessage(false, ConsoleColor.Yellow, "B");
        printMessage(false, ConsoleColor.DarkYellow, "O");
        printMessage(false, ConsoleColor.Red, "K \n \n");
    }

    static void outroText()
    {
        printMessage(
            true,
            ConsoleColor.DarkGray,
            "Programmet gjort av Saga Einarsdotter Kikajon för kursen Programmering i C# .NET på Mittuniversitetet 2024."
        );
    }

    /* Slut på funktioner */
    static void Main(string[] args)
    {

        //Initiera gästboksklassen
        Guestbook guestBook = new();

        bool onSwitch = true;

        //Starta upp så applikationen snurrar
        while (onSwitch == true)
        {
            Clear(); //Rensa konsoll

            /* --- MENYTEXT --- */
            introText();
            CursorVisible = false;

            printMessage(true, ConsoleColor.Cyan, "MENY");
            WriteLine("[1] Lägg till nytt inlägg");
            WriteLine("[2] Ta bort inlägg");
            WriteLine("[X] Avsluta applikation \n");

            int index = 0;

            /* --- SKRIV UT GÄSTBOKSINLÄGG --- */
            foreach (Post post in guestBook.getPosts())
            {
                printMessage(false, ConsoleColor.DarkMagenta, "~ * ");
                printMessage(false, ConsoleColor.Cyan, "~ * ");
                printMessage(false, ConsoleColor.Yellow, "~ * ");
                printMessage(false, ConsoleColor.Red, "~ * \n");
                //Själva kommentaren
                WriteLine($"({index++}) {post.Author} | {post.Posted}:");
                WriteLine(post.Comment);
            }

            //Input för menyval
            int input = (int)ReadKey(true).Key;

            //Switchsats för alternativ
            switch (input)
            {
                //Lägg till nytt inlägg:
                case '1':
                    Clear();
                    CursorVisible = true;

                    //Be om författare
                    Write("Ange författare: ");
                    string? postAuthor = ReadLine();

                    //Switchar för att upprepa inputkommandon
                    bool authInputAccept = false;
                    bool comInputAccept = false;

                    //Kolla om författare har angetts
                    while (authInputAccept == false)
                    {
                        if (String.IsNullOrWhiteSpace(postAuthor))
                        {
                            printMessage(false, ConsoleColor.Red, "Du måste ange ett namn! Försök igen: ");
                            postAuthor = ReadLine();
                        }
                        else
                        {
                            //Byt switch till false för att komma ur loopen
                            authInputAccept = true;
                        }
                    }

                    //Be om kommentar
                    WriteLine("Skriv kommentar, spara med Enter:");
                    string? gbComment = ReadLine();

                    //Kolla om kommentar har angetts:
                    while (comInputAccept == false)
                    {
                        if (String.IsNullOrWhiteSpace(gbComment))
                        {
                            printMessage(true, ConsoleColor.Red, "Du måste skriva något! Försök igen: ");
                            gbComment = ReadLine();
                        }
                        else
                        {
                            comInputAccept = true;
                        }
                    }

                    //Har vi kommit såhär långt bör det gå att spara till filen
                    if (
                        !String.IsNullOrWhiteSpace(postAuthor)
                        && !String.IsNullOrWhiteSpace(gbComment)
                    )
                    {
                        guestBook.addPost(postAuthor, gbComment);
                    }
                    break;
                //Radera inlägg:
                case '2':
                    Clear();
                    CursorVisible = true;
                    /* printMessage(false, ConsoleColor.Red, "Obs! Denna åtgärd kan inte ångras! "); */
                    Write("Index som ska raderas: ");

                    //Lagra index för radering
                    string? delIndex = ReadLine();

                    //Kontroll innan radera
                    if (!String.IsNullOrWhiteSpace(delIndex))
                    {
                        printMessage(true, ConsoleColor.Yellow, $"Är du säker på att du vill radera {delIndex}? Denna åtgärd kan inte ångras. [ y / n ]");
                        string? delConfirm = ReadLine();


                        //Nested Switch!
                        switch (delConfirm)
                        {
                            case "y":
                                try
                                {
                                    guestBook.deletePost(Convert.ToInt32(delIndex));
                                }
                                catch (Exception e)
                                {
                                    //Felmeddelande
                                    printMessage(true, ConsoleColor.Red, $"Följande fel inträffade: {e.Message} \n");
                                    WriteLine("Tryck på nån tangent för att komma tillbaka till gästboken.");
                                    ReadKey();
                                }
                                break;
                            case "n":
                                printMessage(true, ConsoleColor.Blue, "Handlingen avbröts av användaren. Tryck på nån tangent för att återvända till menyn.");
                                ReadKey();
                                break;
                            default:
                                break;
                        }
                    }

                    break;
                //X
                case 88:
                    Clear();
                    outroText();
                    onSwitch = false;
                    break;
            }
        }
    }
}
