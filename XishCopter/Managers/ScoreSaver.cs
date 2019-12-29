using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XishCopter
{
    class ScoreSaver
    {

        FileStream fileStream;
        BinaryWriter Writer;
        BinaryReader Reader;
        public LinkedList<PlayerScore> Scores;
        const int MaxCount = 10;

        public class PlayerScore
        {
            public Int32 Score;
            public string Name;
            public PlayerScore(Int32 score,string name)
            {
                Score = score;
                Name = name;
            }
            public PlayerScore(PlayerScore Ref)
            {
                Score = Ref.Score;
                Name = Ref.Name;
            }
            public PlayerScore()
            {
                Score = 0;
                Name = null;
            }
        }


        public ScoreSaver()
        {
            fileStream = new FileStream("./SaveData.sav",FileMode.OpenOrCreate);
            Reader = new BinaryReader(fileStream);
            Scores = new LinkedList<PlayerScore>();

            while (Reader.PeekChar() != -1)
            {
                PlayerScore NewScore = new PlayerScore();
                char NewChar = Reader.ReadChar();
                while(NewChar!=',')
                {
                    NewScore.Name += NewChar;
                    NewChar = Reader.ReadChar();
                }
                NewScore.Score = Reader.ReadInt32();
                Scores.AddLast(NewScore);
            }

            Reader.Close();
            fileStream.Close();
        }


        public void Save()
        {
            fileStream = new FileStream("./SaveData.sav",FileMode.Create);
            fileStream.Close();
            fileStream = new FileStream("./SaveData.sav", FileMode.Append);
            Writer = new BinaryWriter(fileStream);

            foreach (PlayerScore Score in Scores)
            {
                Writer.Write(Score.Name + ",");
                Writer.Write(Score.Score);
            }

            Writer.Close();
            fileStream.Close();
        }

        public bool TakesPlace(int Score)
        {
            if (Scores == null) return true;
            if (Scores.Count < MaxCount) return true;
            else
            {
                foreach (PlayerScore playerScore in Scores)
                {
                    if (Score > playerScore.Score) return true;
                }
            }
            return false;
        }

        public int AddScore(int score,string name)
        {
            int Index = -1;
            PlayerScore NewPlayer = new PlayerScore(score, name);
            LinkedListNode<PlayerScore> Pointer = Scores.First;
            if (Pointer == null)
            {
                Scores.AddLast(NewPlayer);
                Index = 0;
            }
            else
            {
                Index++;
                while (Pointer != null)
                {
                    if (score > Pointer.Value.Score)
                    {
                        Scores.AddBefore(Pointer, NewPlayer);
                        break;
                    }
                    Pointer = Pointer.Next;
                    Index++;
                }
                if (Pointer == null) Scores.AddLast(NewPlayer);
            }
            if (Scores.Count > MaxCount)
            {
                Scores.RemoveLast();
            }
            if (Index >= MaxCount) Index = -1;
            this.Save();
            return Index;
        }
    }
}
