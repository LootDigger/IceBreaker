using System.IO;
using CORE.Gameplay;
using UnityEngine;

public class BestScoreSaver
{
   private ISaver _saver;
   private readonly string SAVE_FILE_PATH;

   public BestScoreSaver(ISaver saver)
   {
      SAVE_FILE_PATH = Path.Combine(Application.persistentDataPath, "BEST_SCORE.json");
      _saver = saver;
   }

   public void SaveScore(Score score)
   {
      _saver.SaveData(score, SAVE_FILE_PATH);
   }
   
   public Score LoadScore()
   {
      return _saver.LoadData<Score>(SAVE_FILE_PATH);
   }
}
