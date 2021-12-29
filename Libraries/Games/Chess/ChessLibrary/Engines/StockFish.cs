using System.Diagnostics;

namespace ChessLibrary.Engines;


public static class Stockfish
{
    public static UciInfo[] GetBestMoves(String fen, int depth=12, int numberOfMoves=1)
    {
        UciInfo[] moves = new UciInfo[numberOfMoves];

        using (Process engine = new Process())
        {
            engine.StartInfo.FileName = "stockfish";
            engine.StartInfo.UseShellExecute = false;
            engine.StartInfo.RedirectStandardOutput = true;
            engine.StartInfo.RedirectStandardInput = true;
            engine.Start();

            StreamWriter myStreamWriter = engine.StandardInput;

            myStreamWriter.WriteLine("uci");

            bool done = false;
            while(done == false)
            {
                var line = engine.StandardOutput.ReadLine();

                if(line == null || line.StartsWith("uciok"))
                    done = true;
            }

            myStreamWriter.WriteLine($"setoption name MultiPV value {numberOfMoves}");


            myStreamWriter.WriteLine($"position fen {fen}");


            myStreamWriter.WriteLine($"go depth {depth}");
            done = false;
            while(done == false)
            {
                var line = engine.StandardOutput.ReadLine();

                if(line == null || line.StartsWith("bestmove"))
                    done = true;
                else if(line.StartsWith("info "))
                {
                    var info = UciDecoder.ParseUciInfoLine(line);

                    if(info.MultiPV != null)
                        moves[(int)info.MultiPV-1] = info;

                }


            }

            engine.Kill();
        }

        return moves;
    }
}
