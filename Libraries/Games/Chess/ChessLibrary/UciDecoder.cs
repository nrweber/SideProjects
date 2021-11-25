namespace ChessLibrary;

public static class UciDecoder
{
    private static readonly HashSet<string> InfoKeywords = new HashSet<string>{"depth", "seldepth", "time", "nodes", "pv", "multipv", "score", "currmove", "currmovenumber", "hashfull", "nps", "tbhits", "sbhits", "cpuload", "string", "refutation","currline"};
    public static UciInfo ParseUciInfoLine(string line)
    {
        var info = new UciInfo();

        String[] parts = line.Split(' ');

        List<List<String>> allInformation = new List<List<String>>();
        List<String> information = new List<String>();

        foreach(string p in parts)
        {
            if(InfoKeywords.Contains(p))
            {
                allInformation.Add(information);
                information = new List<String>();
            }

            information.Add(p);
        }

        if(information.Count != 0)
            allInformation.Add(information);

        foreach(var infoGroup in allInformation)
        {
            if(infoGroup[0] == "depth")
            {
                info.Depth = Int32.Parse(infoGroup[1]);
            }
            if(infoGroup[0] == "score")
            {
                if(infoGroup[1] == "cp")
                {
                    info.CPScore = Int32.Parse(infoGroup[2]);
                }
                else if(infoGroup[1] == "mate")
                {
                    info.MateScore = Int32.Parse(infoGroup[2]);
                }
            }
        }

        return info;
    }
}


public record struct UciInfo
{
    public int? Depth;
    public int? CPScore;
    public int? MateScore;
}
