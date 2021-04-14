//As of now, this is not fully implemented. This was a basic implementation of difficulty settings.
static public class Difficulty {
    public enum Difficulties {Easy, Medium, Hard}
    //Difficult
    //Changing the below variable to Difficulties.Easy or Difficulties.Hard changes the difficulty.
    static public Difficulties currentDifficulty = Difficulties.Medium; //Defaults to Medium
    //test
}