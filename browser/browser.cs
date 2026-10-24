//Gluon Browser by Clippy-CLI (Github)
using SkiaSharp;
using System.IO;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class MyProgram {
    public static async Task Main(string[] args) {
        Console.TreatControlCAsInput = true;
        Console.Clear();
        Console.WriteLine("    #######    ");
        Console.WriteLine("  ##       ##  ");
        Console.WriteLine(" ##  O---O  ## ");
        Console.WriteLine(" ##   \\ /   ## ");
        Console.WriteLine("  ##   O   ##  ");
        Console.WriteLine("    #######    ");
        Console.WriteLine("Welcome to Gluon browser!");
        Console.WriteLine("Type help for a list of commands");
        Console.WriteLine("Type exit to quit");
        Console.WriteLine("Made by Clippy-CLI" + "\n");
        string storeInput = ("");
        List<String> tabArray = new List<String>();
        while (true) {
            Console.Write(">");
            string input = Console.ReadLine();
            bool handled = false;

            if (input == null || input.Equals("")) {
                handled = true;
            }

            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase)) {
                handled = true;
                break;
            }

            if (input.Equals("previous")) {
                handled = true;
                input = storeInput;
            }


            if (input.Equals("common")) {
                handled = true;
                Console.WriteLine("Common Webpages:");

                Console.WriteLine("Wikipedia: https://en.wikipedia.org/wiki/(page)");
                Console.WriteLine("Simple wikipeida: https://simple.wikipedia.org/wiki/(page)");
                Console.WriteLine("Project Gutenberg (Ebooks): https://www.gutenberg.org/cache/epub/(bookcode)/pg(bookcode)-images.html");
                Console.WriteLine("Legible news: https://legiblenews.com/");
                Console.WriteLine("");
            }

            if (input.StartsWith("link", StringComparison.OrdinalIgnoreCase)) {
                handled = true;
                try {
                    string urlPart = input.Length > 5 ? input.Substring(5).Trim() : string.Empty;
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

            if (input.StartsWith("download", StringComparison.OrdinalIgnoreCase)) {
                handled = true;
                try {
                    string urlPart = input.Length > 9 ? input.Substring(9).Trim() : string.Empty;
                    var getter = new GetFile(urlPart, "");
                    await getter.GetFileFromSiteAsync();
                    Console.WriteLine("File saved");
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
                Console.WriteLine("Game starting... (Press escape to exit)");
                Console.WriteLine("Use arrow keys to move *, avoid !");
                Thread.Sleep(3000);
                bool running = true;

                Player p = new Player();
                Wall methodWall = new Wall();

                int counterWalls = 0;
                int approxFrame = 0;
                while (running) {
                    Console.CursorVisible = false;
                    int[,] gameGrid = new int[38, 19];
                    methodWall.fillArrayWithPositions(gameGrid);

                    if (gameGrid[p.getX(), p.getY()] != 1) {
                        gameGrid[p.getX(), p.getY()] = 2;
                    }
                    else {
                        running = false;
                    }

                    for (int gridX = 0; gridX < 38; gridX++) {
                        for (int gridY = 0; gridY < 19; gridY++) {
                            if (gameGrid[gridX, gridY] != 1 && gameGrid[gridX, gridY] != 2) {
                                gameGrid[gridX, gridY] = 0;
                            }
                        }
                    }




                    if (Console.KeyAvailable) {
                        ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                        if (keyInfo.Key == ConsoleKey.Escape) {
                            running = false;
                        }
                        if (keyInfo.Key == ConsoleKey.RightArrow) {
                            p.changeX(1);
                        }
                        if (keyInfo.Key == ConsoleKey.LeftArrow) {
                            p.changeX(-1);
                        }
                        if (keyInfo.Key == ConsoleKey.UpArrow) {
                            p.changeY(1);
                        }
                        if (keyInfo.Key == ConsoleKey.DownArrow) {
                            p.changeY(-1);
                        }
                    }
                    int doesSpawnWall = Random.Shared.Next(30);
                    if (doesSpawnWall == 0) {
                        Wall idkWallName = new Wall();
                        counterWalls++;
                    }

                    Thread.Sleep(16);
                    if ((approxFrame % 30) == 0) {
                        methodWall.moveAllWalls();
                    }
                    approxFrame++;




                    Console.Clear();

                    for (int i = 0; i < 40; i++) Console.Write("#");
                    Console.Write("\n");

                    for (int drawY = 18; drawY >= 0; drawY--) {
                        Console.Write("#");

                        for (int drawX = 0; drawX < 38; drawX++) {
                            if (gameGrid[drawX, drawY] == 0) {
                                Console.Write(" ");
                            }
                            else if (gameGrid[drawX, drawY] == 1) {
                                Console.Write("!");
                            }
                            else if (gameGrid[drawX, drawY] == 2) {
                                Console.Write("*");
                            }
                            else {
                                Console.Write("?");
                            }
                        }

                        Console.Write("#");
                        Console.Write("\n");
                    }

                    for (int i = 0; i < 40; i++) Console.Write("#");
                    Console.Write("\n");
                }

                Console.WriteLine("Your score is: " + counterWalls);
                methodWall.clearAllWalls();
                Console.CursorVisible = true;
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
                Console.WriteLine("links + (url) - Shows available links on the webpage");
                Console.WriteLine("tab - opens previous input as a tab");
                Console.WriteLine("tabs - Shows a list of open tabs");
                Console.WriteLine("images + (url) - displays images from site as ascii");
                Console.WriteLine("image + (url) - displays an image from source url");
                Console.WriteLine("clear - clears the terminal");
                Console.WriteLine("game - a small, fun, offline game");
                Console.WriteLine("savepage + (url) - saves the page as a text file");
                Console.WriteLine("readfile + (file) - reads a specified txt file");
                Console.WriteLine("download + (url) - downloads file from URL");
                Console.WriteLine("(url) - reads the page at specified url");
                Console.WriteLine("Type a search term to search with duckduckgo!");
            }

            storeInput = (input);
            if (!handled) {
                handled = true;
                if ((input.Length > 8 ) && (input.Substring(0,8).Equals("https://") || input.Substring(0,7).Equals("http://"))) {
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
                } else {
                        try {
                            var getter = new GetSearch(input, "");
                            string result = await getter.GetSearchFromSiteAsync();

                            if (string.IsNullOrWhiteSpace(result)) {
                                Console.WriteLine("No results");
                            }else {
                                var lines = Regex.Split(result, "\r?\n");
                                foreach (var line in lines) {
                                    var t = line.Trim();
                                    if (!string.IsNullOrEmpty(t)) Console.WriteLine(t);
                                }
                            }
                        } catch (Exception e) {
                        Console.WriteLine("Error: " + e.Message);
                        }
                }
            }
        }

        Console.WriteLine("Closing...");
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
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
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
            if (gt == -1) break;

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

        var sb = new StringBuilder();
        bool inTag = false;
        for (int i = 0; i < cleaned.Length; i++) {
            char c = cleaned[i];
            if (!inTag) {
                if (c == '<') { inTag = true; if (sb.Length > 0 && !char.IsWhiteSpace(sb[sb.Length - 1])) sb.Append(' '); }
                else sb.Append(c);
            }
            else {
                if (c == '>') inTag = false;
            }
        }

        string decoded = WebUtility.HtmlDecode(sb.ToString());
        decoded = Regex.Replace(decoded, "\\s+", " ").Trim();

        return decoded;
    }
}

public class GetSearch {
    private readonly string searchQuery;
    private readonly string recievePart;

    public GetSearch(string query, string parameter) {
        searchQuery = query;
        recievePart = parameter;
    }

    public async Task<string> GetSearchFromSiteAsync() {
        string recievedData = string.Empty;

        try {

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
            client.DefaultRequestHeaders.Add("Accept", "text/html");
            client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
            client.DefaultRequestHeaders.Add("Connection", "keep-alive");
            string url = $"https://html.duckduckgo.com/html/?q={Uri.EscapeDataString(searchQuery)}";
            var response = await client.GetAsync(url);

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
        } catch (Exception e) {
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
        String formattedURLS = ("");
        while (true) {
            if (work.IndexOf("links_main links_deep result__body") != -1) {
            work = work.Substring(work.IndexOf("links_main links_deep result__body"));
            String linkAndTitle = work.Substring(work.IndexOf("<a"));
            
            int startTitle = linkAndTitle.IndexOf(">") + 1;
            int endTitle = linkAndTitle.IndexOf("<", startTitle);
            String saidTitle = linkAndTitle.Substring(startTitle, endTitle - startTitle);
            
            linkAndTitle = linkAndTitle.Substring(linkAndTitle.IndexOf("href"));
            linkAndTitle = linkAndTitle.Substring(linkAndTitle.IndexOf("\"") + 1);
            linkAndTitle = linkAndTitle.Substring(0, linkAndTitle.IndexOf("\""));
            string saidURL = linkAndTitle;
            work = work.Substring(work.IndexOf("<a"));
            work = work.Substring(work.IndexOf(">") + 1);

            String saidPreview = work.Substring(0, work.IndexOf("</a"));
            saidPreview = saidPreview.Replace("<b>", "");
            saidPreview = saidPreview.Replace("</b>", "");
            //formattedURLS = (formattedURLS + saidTitle + "\n" + saidURL + "\n" + saidPreview + "\n" + "\n" + "\n");
            //temporary debug version, no preview as preview will be fixed later
            formattedURLS = (formattedURLS + saidTitle + "\n" + saidURL + "\n");

            } else {
                break;
            }
        }
        return formattedURLS;
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
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
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

            int start = work.IndexOf("href", StringComparison.OrdinalIgnoreCase);
            if (start == -1) break;

            work = work.Substring(start);
            int gt = work.IndexOf('"');

            if (gt == -1) break;

            work = work.Substring(gt + 1);

            int endP = work.IndexOf('"');
            if (endP == -1) {

                break;
            }

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
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
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

            if (gt == -1) break;

            work = work.Substring(gt + 1);

            int endP = work.IndexOf("\"", StringComparison.OrdinalIgnoreCase);

            if (endP == -1) {


                break;
            }





            string linkToAdd = work.Substring(0, endP);

            string linkToAddExtension = linkToAdd.Length >= 4 ? linkToAdd.Substring(linkToAdd.Length - 4) : linkToAdd;
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


        var decoded = new StringBuilder();
        for (int i = 0; i < linkArray.Count; i++) {
            using var imageClient = new HttpClient();
            imageClient.DefaultRequestHeaders.UserAgent.ParseAdd("Gluon-Browser");
            imageClient.DefaultRequestHeaders.Accept.ParseAdd("image/*");
            imageClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.9");

            using Stream streamImage = await imageClient.GetStreamAsync(linkArray[i]);
            using var memoryStream = new MemoryStream();
            await streamImage.CopyToAsync(memoryStream);

            using var bitmap = SKBitmap.Decode(memoryStream.ToArray())
                ?? throw new InvalidDataException("Could not decode image.");

            const int terminalWidth = 80;
            int targetHeight = Math.Max(
                1,
                (int)(bitmap.Height / (double)bitmap.Width
                    * terminalWidth * 0.5));

            using var resized = bitmap.Resize(
                new SKImageInfo(terminalWidth, targetHeight),
                new SKSamplingOptions(SKFilterMode.Linear))
                ?? throw new InvalidDataException("Could not resize image.");

            char[] asciiRamp = { ' ', '.', ':', '-', '=', '+', '*', '%', '@', '#' };
            var asciiBuilder = new StringBuilder();

            for (int y = 0; y < resized.Height; y++) {
                for (int x = 0; x < resized.Width; x++) {
                    SKColor pixel = resized.GetPixel(x, y);
                    double luminance =
                        0.2126 * pixel.Red +
                        0.7152 * pixel.Green +
                        0.0722 * pixel.Blue;

                    int rampIndex = (int)(
                        luminance / 255.0 * (asciiRamp.Length - 1));
                    asciiBuilder.Append(asciiRamp[rampIndex]);
                }

                asciiBuilder.AppendLine();
            }

            decoded.Append(asciiBuilder);
            decoded.AppendLine();
        }

        return decoded.ToString();
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
        using var imageClient = new HttpClient();
        imageClient.DefaultRequestHeaders.UserAgent.ParseAdd("Gluon-Browser");
        imageClient.DefaultRequestHeaders.Accept.ParseAdd("image/*");
        imageClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.9");

        using Stream streamImage = await imageClient.GetStreamAsync(chooseURL);
        using var memoryStream = new MemoryStream();
        await streamImage.CopyToAsync(memoryStream);

        using var bitmap = SKBitmap.Decode(memoryStream.ToArray())
            ?? throw new InvalidDataException("Could not decode image.");

        const int terminalWidth = 80;
        int targetHeight = Math.Max(
            1,
            (int)(bitmap.Height / (double)bitmap.Width
                * terminalWidth * 0.5));

        using var resized = bitmap.Resize(
            new SKImageInfo(terminalWidth, targetHeight),
            new SKSamplingOptions(SKFilterMode.Linear))
            ?? throw new InvalidDataException("Could not resize image.");

        char[] asciiRamp = { ' ', '.', ':', '-', '=', '+', '*', '%', '@', '#' };
        var asciiBuilder = new StringBuilder();

        for (int y = 0; y < resized.Height; y++) {
            for (int x = 0; x < resized.Width; x++) {
                SKColor pixel = resized.GetPixel(x, y);

                double luminance =
                    0.2126 * pixel.Red +
                    0.7152 * pixel.Green +
                    0.0722 * pixel.Blue;

                int rampIndex = (int)(
                    luminance / 255.0 * (asciiRamp.Length - 1));

                asciiBuilder.Append(asciiRamp[rampIndex]);
            }

            asciiBuilder.AppendLine();
        }

        string decoded = asciiBuilder.ToString();
        Console.WriteLine(decoded);
        return decoded;
    }
}

public class GetFile {
    private readonly string chooseURL;
    private readonly string recievePart;

    public GetFile(string pickURL, string parameter) {
        chooseURL = pickURL;
        recievePart = parameter;
    }

    public async Task GetFileFromSiteAsync() {
        using var fileClient = new HttpClient();
        fileClient.DefaultRequestHeaders.UserAgent.ParseAdd("Gluon-Browser");
        fileClient.DefaultRequestHeaders.Accept.ParseAdd("image/*");
        fileClient.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.9");
        
        try {
        using Stream downloadStream = await fileClient.GetStreamAsync(chooseURL);
        string fileName = string.IsNullOrWhiteSpace(recievePart)
                ? Path.GetFileName(new Uri(chooseURL).AbsolutePath)
                : recievePart;

            if (string.IsNullOrWhiteSpace(fileName))
                fileName = "download";

            string savePath = Path.Combine(
                Environment.CurrentDirectory,
                Path.GetFileName(fileName)
        );
        using FileStream fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None);
        await downloadStream.CopyToAsync(fileStream);
        } catch {
            Console.WriteLine("Failed to save file");
        }
    }
}

public class Player {
    private int x;
    private int y;

    public Player() {
        x = 19;
        y = 10;
    }
    public int getX() {
        return x;
    }
    public int getY() {
        return y;
    }
    public void changeX(int howMuch) {
        if (!((x + howMuch) > 37) && !((x + howMuch) < 0)) {
            x += howMuch;
        }
    }
    public void changeY(int howMuch) {
        if (!((y + howMuch) > 18) && !((y + howMuch) < 0)) {
            y += howMuch;
        }
    }
}
public class Wall {
    public static List<Wall> Walls = new List<Wall>();
    private int x;
    private static int score;
    private int y;
    private int direction;
    public void moveAllWalls() {
        foreach (Wall thisWall in Walls) {
            if (thisWall.direction == 0) {
                thisWall.y += 1;
            }
            else if (thisWall.direction == 1) {
                thisWall.x += 1;
            }
            else if (thisWall.direction == 2) {
                thisWall.y -= 1;
            }
            else if (thisWall.direction == 3) {
                thisWall.x -= 1;
            }
        }
    }
    public int getScore() {
        return score;
    }

    public int getX() {
        return x;
    }
    public int getY() {
        return y;
    }
    public void fillArrayWithPositions(int[,] fillArray) {
        foreach (Wall getPos in Walls) {
            if (getPos.getX() > 0 && getPos.getX() < 38 && getPos.getY() > 0 && getPos.getY() < 19) {
                fillArray[getPos.getX(), getPos.getY()] = 1;
            }
        }
    }

    public Wall() {
        int half = Random.Shared.Next(4);
        if (half == 0) {
            direction = 0;
            x = Random.Shared.Next(38);
            y = 0;
        }
        else if (half == 1) {
            direction = 1;
            y = Random.Shared.Next(19);
            x = 0;
        }
        else if (half == 2) {
            direction = 2;
            y = 19;
            x = Random.Shared.Next(38);
        }
        else if (half == 3) {
            direction = 3;
            y = Random.Shared.Next(19);
            x = 38;
        }
        score++;
        Walls.Add(this);
    }
    public Boolean checkIfTouchingPlayer(Player checkPlayer) {
        int playerX = checkPlayer.getX();
        int playerY = checkPlayer.getY();
        foreach (Wall thisWall in Walls) {
            if ((thisWall.x == playerX) && (thisWall.y == playerY)) {
                return true;
            }
        }
        return false;
    }

    public void clearAllWalls() {
        Walls.Clear();
    }
}