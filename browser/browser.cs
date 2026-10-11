using SixLabors.ImageSharp;
using System.IO;
using SixLabors.ImageSharp.Processing;
using System;
using System.Net.Http;
using System.Text;
using SixLabors.ImageSharp.PixelFormats;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class MyProgram {
    public static async Task Main(string[] args) {
        Console.Clear();
        Console.WriteLine("    #######    ");
        Console.WriteLine("  ##       ##  ");
        Console.WriteLine(" ##  O---O  ## ");
        Console.WriteLine(" ##   \\ /   ## ");
        Console.WriteLine("  ##   O   ##  ");
        Console.WriteLine("    #######    ");
        //neat image for now
        Console.WriteLine("Welcome to Gluon browser!");
        Console.WriteLine("Type help for a list of commands");
        Console.WriteLine("Type exit to quit");
        Console.WriteLine("Made by Clippy-CLI");
        Console.WriteLine();
        string storeInput = ("");
        List<String> tabArray = new List<String>();
        while (true) {
            Console.Write(">");
            string input = Console.ReadLine();
            bool handled = false;

            if (input == null || input.Equals("exit", StringComparison.OrdinalIgnoreCase)) {
                break;
            }

            if (input.Equals("previous")) {
                input = storeInput;
            }

            if (input.Equals("common")) {
                handled = true;
                //prints some common webpages
                Console.WriteLine("Common Webpages:");

                Console.WriteLine("Wikipedia: https://en.wikipedia.org/wiki/(page)");
                Console.WriteLine("Simple wikipeida: https://simple.wikipedia.org/wiki/(page)");
                Console.WriteLine("Project Gutenberg (Ebooks): https://www.gutenberg.org/cache/epub/(bookcode)/pg(bookcode)-images.html");
                Console.WriteLine("Legible news: https://legiblenews.com/");
                Console.WriteLine("");
            }

            if (input.StartsWith("linkscan", StringComparison.OrdinalIgnoreCase)) {
                handled = true;
                try {
                    string urlPart = input.Length > 9 ? input.Substring(9).Trim() : string.Empty;
                    var getter = new GetLinks(urlPart, "");
                    string result = await getter.GetLinksFromSiteAsync();

                    if (string.IsNullOrWhiteSpace(result)) {
                        Console.WriteLine("no links found");
                    }
                    else {
                        Console.WriteLine(result);
                    }
                }
                catch (Exception e) {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
            if (input.StartsWith("images", StringComparison.OrdinalIgnoreCase)) {
                handled = true;
                try {
                    string urlPart = input.Length > 7 ? input.Substring(7).Trim() : string.Empty;
                    var getter = new GetImageLinks(urlPart, "");
                    string result = await getter.GetImageLinksFromSiteAsync();

                    if (string.IsNullOrWhiteSpace(result)) {
                        Console.WriteLine("no images found");
                    }
                    else {
                        Console.WriteLine(result);
                    }
                }
                catch (Exception e) {
                    Console.WriteLine("Error: " + e.Message);
                }
            }

            if (input.StartsWith("image", StringComparison.OrdinalIgnoreCase)) {
                handled = true;
                try {
                    string urlPart = input.Length > 6 ? input.Substring(6).Trim() : string.Empty;
                    var getter = new GetImage(urlPart, "");
                    string result = await getter.GetImageFromSiteAsync();

                    if (string.IsNullOrWhiteSpace(result)) {
                        Console.WriteLine("no images found");
                    }
                    else {
                    }
                }
                catch (Exception e) {
                    Console.WriteLine("Error: " + e.Message);
                }
            }

            if (input.Equals("tab", StringComparison.OrdinalIgnoreCase)) {
                handled = true;
                Console.WriteLine(storeInput + "added as a tab");
                tabArray.Add(storeInput);
            }

            if (input.Equals("tabs", StringComparison.OrdinalIgnoreCase)) {
                handled = true;
                Console.WriteLine("Open Tabs:");
                for (int i = 0; i < tabArray.Count; i++) {
                    Console.WriteLine(tabArray[i]);
                }
            }

            if (input.Equals("clear", StringComparison.OrdinalIgnoreCase)) {
                handled = true;
                Console.Clear();
            }

            if (input.Equals("game", StringComparison.OrdinalIgnoreCase)) {
                handled = true;
                //start a game, all handled right here
                Console.WriteLine("Game starting...");
                //game idea: tycoon or something, simple and allows for lots of continuation
                //since i want this to be minimal, i will just have it use a password system
                //i mean the code is viewable so its not like I can make a file to make it harder to
                //reverse engineer. Anyway it will just be like options, total money, things to buy, and then
                //days passed or something (or like a next turn option) as well as a quit option (quit game)
                //that is what i have so far, then we need like events or random events that change the game and how
                //it goes, then it has a completion point where you buy something, try do it in as few turns as 
                //possible but then again thats manipulateable with the password

                //configure saves, passwords, varibales, up here before in palying loop
                Boolean onTitle = true;

                Console.WriteLine("1. New Game");
                Console.WriteLine("2. Continue Game");

                long money = 0;
                int turns = 0;
                //placehodlers for now, will add more later when I have specifics about game

                int trucks = 0;
                int trains = 0;
                int airplanes = 0;
                //transport vehicles, as you progress you get more money to buy the expensive ones that
                //sell more items, by default just sell most valuable items (exclduing multipliers)
                //every turn if too many

                int sawmills = 0;
                int oilDrills = 0;
                int fabs = 0;
                //number of things you have to produce items to sell

                long wood = 0;
                long oil = 0;
                long microchips = 0;
                //items from those things

                int woodMultiplier = 1;
                int oilMultiplier = 1;
                int microchipMultiplier = 1;
                //multipliers and such because that is what makes it go on forever

                //idk if we need


                while (onTitle) {
                    string menuOption = Console.ReadLine();

                    if (int.TryParse(menuOption, out int selectedOption)) {
                        if (selectedOption == 1) {
                            Console.WriteLine("New game starting...");
                            onTitle = false;
                        }
                        else if (selectedOption == 2) {
                            Console.WriteLine("Please enter a code (type cancel to launch new game)");
                            //so in here we are just going to like add a code parser and then re-assign all int
                            //values from above with ones from here
                            //will probably just be like int|int|int|int and each int corresponds to its position
                            //in order of being defined up above
                            string passOption = Console.ReadLine();
                            //we are going to first check for a correct amount of | and that it is all ints
                            //just do this by formating the string to remove all | while counting that there is a
                            //correct amount, then the leftover string just check that it is a number not int as 
                            //int is likely too small. Im not going to add a checker for if you put in impossible
                            //values to the password thing because if you want to mess around and make weird values
                            //to play the game with, go ahead





                        }
                        else {
                            Console.WriteLine("Please enter a valid option");
                        }
                    }
                    else {
                        Console.WriteLine("Please enter a valid option");
                    }

                }

                Boolean isPlayingGame = true;
                while (isPlayingGame) {
                    //note to add a console.Clear(); after every turn/option so that it looks clean
                    //as well as a limit on certain items because if the ints get too big then uh oh
                    Console.WriteLine("1. Upgrade vehicles");
                    Console.WriteLine("2. Upgrade production");
                    Console.WriteLine("3. Upgrade multipliers");
                    Console.WriteLine("4. Extra purchases");
                    Console.WriteLine("5. Next turn");
                    Console.WriteLine("6. Save and quit");
                    Console.WriteLine("# of Sawmills: ");
                    Console.WriteLine("# of Oil Drills: ");
                    Console.WriteLine("# of Microchip Fabs: ");
                    Console.WriteLine("# of Trucks: ");
                    Console.WriteLine("# of Trains: ");
                    Console.WriteLine("# of Airplanes: ");
                    Console.WriteLine("Income per turn: ");
                    Console.WriteLine("Current money: ");
                    Console.WriteLine("Current turn: ");
                    Console.WriteLine("Input an option");














































                }

                Console.WriteLine("Your game save code is: ");

            }

            if (input.StartsWith("savepage", StringComparison.OrdinalIgnoreCase)) {
                handled = true;
                try {
                    string urlPart = input.Length > 9 ? input.Substring(9).Trim() : string.Empty;
                    var getter = new GetData(urlPart, "");
                    string result = await getter.GetDataFromSiteAsync();

                    if (string.IsNullOrWhiteSpace(result)) {
                        Console.WriteLine("no links found");
                    }
                    else {
                        File.WriteAllText("savedSite.txt", result);
                        Console.WriteLine("Page saved as savedSite.txt");
                    }
                }
                catch (Exception e) {
                    Console.WriteLine("Error: " + e.Message);
                }
            }

            if (input.StartsWith("readfile", StringComparison.OrdinalIgnoreCase)) {
                handled = true;
                string filePath = input.Length > 9 ? input.Substring(9).Trim() : string.Empty;
                try {
                    string fileText = File.ReadAllText(filePath);
                    Console.WriteLine(fileText);
                    Console.WriteLine("File contents displayed successfully");
                }
                catch (IOException e) {
                    Console.WriteLine($"An error occurred while reading the file:  {e.Message}");
                }

            }

            if (input.Equals("help")) {
                handled = true;
                Console.WriteLine("exit - Closes the program");
                Console.WriteLine("previous - Runs the previous command/url");
                Console.WriteLine("common - Shows a list of common sites");
                Console.WriteLine("linkscan + (url) - Shows available links on the webpage");
                Console.WriteLine("tab - opens previous input as a tab");
                Console.WriteLine("tabs - Shows a list of open tabs");
                Console.WriteLine("images + (url) - displays images from site as ascii");
                Console.WriteLine("image + (url) - displays an image from source url");
                Console.WriteLine("clear - clears the terminal");
                Console.WriteLine("game - a small, fun, offline game");
                Console.WriteLine("savepage + (url) - saves the page as a text file");
                Console.WriteLine("readfile + (file) - reads a specified txt file");
            }

            storeInput = (input);
            if (!handled) {
                try {
                    var getter = new GetData(input, "");
                    string result = await getter.GetDataFromSiteAsync();

                    if (string.IsNullOrWhiteSpace(result)) {
                        Console.WriteLine("(no readable <p> content found)");
                    }
                    else {
                        var lines = Regex.Split(result, "\r?\n");
                        foreach (var line in lines) {
                            var t = line.Trim();
                            if (!string.IsNullOrEmpty(t)) Console.WriteLine(t);
                        }
                    }
                }
                catch (Exception e) {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }

        Console.WriteLine("Closed");
    }
}

public class GetData {
    private readonly string chooseURL;
    private readonly string recievePart;

    public GetData(string pickURL, string parameter) {
        chooseURL = pickURL;
        recievePart = parameter;
    }

    public async Task<string> GetDataFromSiteAsync() {
        string recievedData = string.Empty;

        try {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Gluon-Browser");
            client.DefaultRequestHeaders.Add("Accept", "text/html");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");

            var response = await client.GetAsync(chooseURL);
            recievedData = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode) {
                if ((int)response.StatusCode == 403) {
                    return "Http 403, request rejected (Error Detected as bot/blocked traffic";
                }

                if (string.IsNullOrEmpty(recievedData)) {
                    return "Error loading website (Error No Content Recieved From Site)";
                }

                return "An error has occured, check the URL and try again (Error Site not found";
            }
        }
        catch (Exception e) {
            Console.WriteLine("Looks bad: " + e);
            var eString = e.ToString();
            if (eString.IndexOf("403", StringComparison.OrdinalIgnoreCase) != -1) {
                return "Http 403, request rejected (Error Code: yg7f63g)";
            }
            else if (string.IsNullOrEmpty(recievedData)) {
                return "Error loading website (Error Code: d3y7g3)";
            }
            else {
                return "An error has occured, check the URL and try again (Error Code: v7d332)";
            }
        }
        var recievedNew = new StringBuilder();
        string work = recievedData;

        while (true) {
            int start = work.IndexOf("<p", StringComparison.OrdinalIgnoreCase);
            if (start == -1) break;

            work = work.Substring(start);
            int gt = work.IndexOf('>');
            if (gt == -1) break; // malformed

            work = work.Substring(gt + 1);
            int endP = work.IndexOf("</p", StringComparison.OrdinalIgnoreCase);
            if (endP == -1) {
                recievedNew.Append(work);
                break;
            }

            recievedNew.Append(work.Substring(0, endP));

            int endPtag = work.IndexOf('>', endP);
            if (endPtag == -1) break;

            work = work.Substring(endPtag + 1);
        }

        string cleaned = recievedNew.ToString();

        // remove any remaining tags
        var sb = new StringBuilder();
        bool inTag = false;
        for (int i = 0; i < cleaned.Length; i++) {
            char c = cleaned[i];
            if (!inTag) {
                if (c == '<')  { inTag = true; if (sb.Length > 0 && !char.IsWhiteSpace(sb[sb.Length - 1])) sb.Append(' '); }
                else sb.Append(c);
            }
            else {
                if (c == '>') inTag = false;
            }
        }

        string decoded = WebUtility.HtmlDecode(sb.ToString());
        // collapse whitespace and trim
        decoded = Regex.Replace(decoded, "\\s+", " ").Trim();

        return decoded;
    }
}

public class GetLinks {
    private readonly string chooseURL;
    private readonly string recievePart;
    List<String> linkArray = new List<String>();

    public GetLinks(string pickURL, string parameter) {
        chooseURL = pickURL;
        recievePart = parameter;
    }

    public async Task<string> GetLinksFromSiteAsync() {
        string recievedData = string.Empty;

        try {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Gluon-Browser");
            client.DefaultRequestHeaders.Add("Accept", "text/html");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");

            var response = await client.GetAsync(chooseURL);
            // read as string even if not successful so we can inspect
            recievedData = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode) {
                if ((int)response.StatusCode == 403) {
                    return "Http 403, request rejected (Error Detected as bot/blocked traffic";
                }

                if (string.IsNullOrEmpty(recievedData)) {
                    return "Error loading website (Error No Content Recieved From Site)";
                }

                return "An error has occured, check the URL and try again (Error Site not found";
            }
        }
        catch (Exception e) {
            Console.WriteLine("Looks bad: " + e);
            var eString = e.ToString();
            if (eString.IndexOf("403", StringComparison.OrdinalIgnoreCase) != -1) {
                return "Http 403, request rejected (Error Code: yg7f63g)";
            }
            else if (string.IsNullOrEmpty(recievedData)) {
                return "Error loading website (Error Code: d3y7g3)";
            }
            else {
                return "An error has occured, check the URL and try again (Error Code: v7d332)";
            }
        }
        var recievedNew = new StringBuilder();
        string work = recievedData;

        while (true) {

            int start = work.IndexOf("href", StringComparison.OrdinalIgnoreCase);
            if (start == -1) break;

            work = work.Substring(start);
            int gt = work.IndexOf('"');
            //first \" in the link, so it does get removed

            if (gt == -1) break; // malformed

            work = work.Substring(gt + 1);
            //in this stage, would have the first/whatever number url, plus everything after it

            //now down here there is only one '"' which is the end of the link
            int endP = work.IndexOf('"');
            if (endP == -1) {
                // take remainder
                break;
            }

            //replace this with add the link to the array
            //currently 0 (start of URL) to endP
            linkArray.Add(work.Substring(0, endP));
            work = work.Substring(endP + 1);
        }

        var decodedSb = new StringBuilder();
        for (int indexCount = 0; indexCount < linkArray.Count; indexCount++) {

            decodedSb.Append(linkArray[indexCount]);

            decodedSb.Append("\n");

        }

        return decodedSb.ToString();
    }
}


public class GetImageLinks {
    private readonly string chooseURL;
    private readonly string recievePart;

    List<String> linkArray = new List<String>();

    public GetImageLinks(string pickURL, string parameter) {
        chooseURL = pickURL;
        recievePart = parameter;
    }

    public async Task<string> GetImageLinksFromSiteAsync() {
        string recievedData = ("");

        try {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Gluon-Browser");
            client.DefaultRequestHeaders.Add("Accept", "text/html");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");

            var response = await client.GetAsync(chooseURL);
            recievedData = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode) {
                if ((int)response.StatusCode == 403) {
                    return "Http 403, request rejected (Error Detected as bot/blocked traffic";
                }

                if (string.IsNullOrEmpty(recievedData)) {
                    return "Error loading website (Error No Content Recieved From Site)";
                }

                return "An error has occured, check the URL and try again (Error Site not found";
            }
        }
        catch (Exception e) {
            Console.WriteLine("Looks bad: " + e);
            var eString = e.ToString();
            if (eString.IndexOf("403", StringComparison.OrdinalIgnoreCase) != -1) {
                return "Http 403, request rejected (Error Code: yg7f63g)";
            }
            else if (string.IsNullOrEmpty(recievedData)) {
                return "Error loading website (Error Code: d3y7g3)";
            }
            else {
                return "An error has occured, check the URL and try again (Error Code: v7d332)";
            }
        }


        var recievedNew = new StringBuilder();
        string work = recievedData;
        while (true) {


            int start = work.IndexOf("src=", StringComparison.OrdinalIgnoreCase);
            if (start == -1) break;
            work = work.Substring(start);

            int gt = work.IndexOf("\"");

            if (gt == -1) break; // malformed

            work = work.Substring(gt + 1);

            int endP = work.IndexOf("\"", StringComparison.OrdinalIgnoreCase);

            if (endP == -1) {


                break;
            }





            //now to determine if it is an image using the above mentioned file formats
            string linkToAdd = work.Substring(0, endP);

            string linkToAddExtension = linkToAdd.Length >= 4 ? linkToAdd.Substring(linkToAdd.Length - 4) : linkToAdd;
            //no idea if that math is even correct to get that extension or not, 4 is lazy way to do .png or jpeg, etc
            if (linkToAddExtension.Equals(".png")) {
                if ((linkToAdd.Length < 5) || !linkToAdd.Substring(0, 4).Equals("http")) {
                    linkArray.Add(chooseURL + linkToAdd);
                }
                else {
                    linkArray.Add(linkToAdd);
                }
            }
            else if (linkToAddExtension.Equals("jpeg")) {
                if ((linkToAdd.Length < 5) || !linkToAdd.Substring(0, 4).Equals("http")) {
                    linkArray.Add(chooseURL + linkToAdd);
                }
                else {
                    linkArray.Add(linkToAdd);
                }
            }
            else if (linkToAddExtension.Equals(".jpg")) {
                if ((linkToAdd.Length < 5) || !linkToAdd.Substring(0, 4).Equals("http")) {
                    linkArray.Add(chooseURL + linkToAdd);
                }
                else {
                    linkArray.Add(linkToAdd);
                }
            }
            else if (linkToAddExtension.Equals("webp")) {
                if ((linkToAdd.Length < 5) || !linkToAdd.Substring(0, 4).Equals("http")) {
                    linkArray.Add(chooseURL + linkToAdd);
                }
                else {
                    linkArray.Add(linkToAdd);
                }
            }
        }


        string decoded = ("");
        for (int i = 0; i < linkArray.Count; i++) {

            using var imageClient = new HttpClient();
            imageClient.DefaultRequestHeaders.Add("User-Agent", "Gluon-Browser");
            imageClient.DefaultRequestHeaders.Accept.ParseAdd("image/*");
            imageClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
            imageClient.DefaultRequestHeaders.Add("Connection", "keep-alive");


            using (Stream streamImage = await imageClient.GetStreamAsync(linkArray[i])) {
                using (Image image = await Image.LoadAsync(streamImage)) {

                    char[] AsciiRamp =  { ' ', '.', ':', '-', '=', '+', '*', '%', '@', '#' };



                    using Image<Rgb24> imageToPrint = image.CloneAs<Rgb24>();
                    int width = imageToPrint.Width;
                    int height = imageToPrint.Height;
                    int targetWidth = 80;
                    //change dimensions
                    double aspectRadio = (double)imageToPrint.Height / imageToPrint.Width;
                    int targetHeight = (int)(targetWidth * aspectRadio * 0.5);

                    //resize
                    imageToPrint.Mutate(ctx => ctx.Resize(targetWidth, targetHeight));

                    var asciiBuilder = new StringBuilder();

                    imageToPrint.ProcessPixelRows(accessor => {
                        for (int y = 0; y < accessor.Height; y++) {
                            Span<Rgb24> pixelRow = accessor.GetRowSpan(y);

                            for (int x = 0; x < pixelRow.Length; x++) {
                                Rgb24 pixel = pixelRow[x];

                                double luminance = (0.2126 * pixel.R) + (0.7152 * pixel.G) + (0.0722 * pixel.B);

                                int rampIndex = (int)(luminance / 255.0 * (AsciiRamp.Length - 1));

                                asciiBuilder.Append(AsciiRamp[rampIndex]);
                            }

                            asciiBuilder.AppendLine();
                        }
                    });

                    decoded = (decoded + asciiBuilder.ToString());
                    decoded = (decoded + "\n");
                    decoded = (decoded + "\n");
                    decoded = (decoded + "\n");
                    //big line break between each image
                }
            }



        }
        return decoded;
    }
}










public class GetImage {
    private readonly string chooseURL;
    private readonly string recievePart;

    public GetImage(string pickURL, string parameter) {
        chooseURL = pickURL;
        recievePart = parameter;
    }

    public async Task<string> GetImageFromSiteAsync() {
        try {
            using var imageClient = new HttpClient();
            imageClient.DefaultRequestHeaders.Add("User-Agent", "Gluon-Browser");
            imageClient.DefaultRequestHeaders.Accept.ParseAdd("image/*");
            imageClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
            imageClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            using (Stream streamImage = await imageClient.GetStreamAsync(chooseURL)) {
                using (Image image = await Image.LoadAsync(streamImage)) {
                    String decoded = ("");



                    char[] AsciiRamp =  { ' ', '.', ':', '-', '=', '+', '*', '%', '@', '#' };



                    using Image<Rgb24> imageToPrint = image.CloneAs<Rgb24>();
                    int width = imageToPrint.Width;
                    int height = imageToPrint.Height;
                    int terminalWidth = 80;

                    double aspectRadio = (double)imageToPrint.Height / imageToPrint.Width;
                    int targetHeight = (int)(terminalWidth * aspectRadio * 0.5);

                    //resize
                    imageToPrint.Mutate(ctx => ctx.Resize(terminalWidth, targetHeight));

                    var asciiBuilder = new StringBuilder();

                    imageToPrint.ProcessPixelRows(accessor => {
                        for (int y = 0; y < accessor.Height; y++) {
                            Span<Rgb24> pixelRow = accessor.GetRowSpan(y);

                            for (int x = 0; x < pixelRow.Length; x++) {
                                Rgb24 pixel = pixelRow[x];


                                double luminance = (0.2126 * pixel.R) + (0.7152 * pixel.G) + (0.0722 * pixel.B);


                                int rampIndex = (int)(luminance / 255.0 * (AsciiRamp.Length - 1));

                                asciiBuilder.Append(AsciiRamp[rampIndex]);
                            }


                            asciiBuilder.AppendLine();
                        }
                    });
                    Console.WriteLine(asciiBuilder);
                    decoded = (decoded + asciiBuilder.ToString());
                    //big line break between each image
                    return decoded;
                }
            }



        }
        catch {
            return ("Umm something happened");
        }
    }
}
