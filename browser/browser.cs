//Note to self: before a new big version (devlog, promoting, etc with the release of the game probably as
//being the first actual version or something of the browser) remember to format code, and also remove comments
//that aren't helpful and were mainly just for me.

//Gluon Browser by Clippy-CLI (Github)
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

            if (input.Equals("legal", StringComparison.OrdinalIgnoreCase)) {
                handled = true;
                Console.WriteLine("TOS - Follow licenses on github/in Readme and don't do illegal stuff");
                Console.WriteLine("Privacy policy - We collect no data, because I don't implement that");
                Console.WriteLine("you can check the code to prove it");
            }

            if (input.Equals("game", StringComparison.OrdinalIgnoreCase)) {
                handled = true;
                //start a game, all handled right here
                //ideas for game: evade game
                Console.WriteLine("Game starting... (Press escape to exit)");
                Console.WriteLine("Use arrow keys to move *, avoid !");
                Thread.Sleep(3000);
                    bool running = true;
                
                Player p = new Player();
                Wall methodWall = new Wall();
                //wall used for calling wall methods
                
                int counterWalls = 0;
                int approxFrame = 0;
                    while (running) {
                        //this is going to be the fun part, get all the calculations and such
                        int[,] gameGrid = new int[38, 19];
                        methodWall.fillArrayWithPositions(gameGrid);
                        //idk what the plan is here, probably jsut have 0 = nothing 1 = wall 2 = player
                        //then after the grid is calculated (probably at the start of each frame) there is something that
                        //will draw it
                        //calculate before, then draw after console.clear

                        gameGrid[p.getX(), p.getY()] = 2;
                        //automatically, player spot =2

                        //in here, somehow get all wall positions

                        for (int gridX = 0; gridX < 38; gridX++) {
                            for (int gridY = 0; gridY < 19; gridY++) {
                                if (gameGrid[gridX, gridY] != 1 && gameGrid[gridX, gridY] != 2) { 
                                    gameGrid[gridX,gridY] = 0;
                                }
                            }
                        }

                        


                        //key is pressed
                        if (Console.KeyAvailable) {
                            //in here, define keys and actions
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
                            int doesSpawnWall = Random.Shared.Next(60);
                            if (doesSpawnWall == 0) {
                                Wall idkWallName = new Wall();
                                counterWalls++;
                            }
                            Thread.Sleep(16);
                            methodWall.checkIfTouchingPlayer(p);
                            if ((approxFrame % 60) == 0){
                            methodWall.moveAllWalls();
                            }
                            approxFrame++;




                            Console.Clear();
                        //draw game here

                        //first, draw top
                        for (int i = 0; i < 40; i++) Console.Write("#");
                        Console.Write("\n");

                        for (int drawY = 18; drawY >= 0; drawY--) {
                            Console.Write("#");

                            //check each position in a row
                            for (int drawX = 0; drawX < 38; drawX++) {
                                if(gameGrid[drawX,drawY] == 0) {
                                    Console.Write(" ");
                                } else if(gameGrid[drawX,drawY] == 1) {
                                    Console.Write("!");
                                } else if(gameGrid[drawX,drawY] == 2) {
                                    Console.Write("*");
                                } else {
                                    //if this shows up, something probably went wrong
                                    Console.Write("?");
                                }
                            }

                            Console.Write("#");
                            Console.Write("\n");
                        }

                        //draw bottom
                        for (int i = 0; i < 40; i++) Console.Write("#");
                        Console.Write("\n");

















                    }
                    Console.WriteLine("Your score is: " + counterWalls);
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
                Console.WriteLine("Legal - Random stuff like TOS/Privacy Policy");
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
    //still need a mehtod to get wall x and y for drawing the game screen
    public static List<Wall> Walls = new List<Wall>();
    private int x;
    private static int score;
    private int y;
    private int direction;
    //0 = up, 1 = right, 2, down, 3 left
    public void moveAllWalls() {
        foreach(Wall thisWall in Walls) {
            if (thisWall.direction == 0) {
                thisWall.y += 1;
            } else if (direction == 1) {
                thisWall.x += 1;
            } else if (direction == 2) {
                thisWall.y -= 1;
            } else if (direction == 3) {
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
            fillArray[getPos.getX(),getPos.getY()] = 1;
            }
        }
    }
    
    public Wall () {
        //always start from edge, call with a certain chance of happening during game loop
        //add to the array will have to be done during game loop when object name is determined
        //need to add another thing here, as it will always be 0 or one, should be 0 or 40 so 1 of 4 sides
        int half = Random.Shared.Next(4);
        if (half == 0) {
            direction = 0;
            x = Random.Shared.Next(38);
            y = 0;
        } else if (half == 1) {
            direction = 1;
            y = Random.Shared.Next(19);
            x = 0;
        } else if (half == 2) {
            direction = 2;
            y = 38;
            x = Random.Shared.Next(38);
        } else if (half == 3) {
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
        foreach(Wall thisWall in Walls) {
            if ((x == playerX) && (y == playerY)) {
                return true; 
            }
        }
        return false;
    }
}
