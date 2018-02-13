 public abstract class Player{
	public int id;
	public char color;
	abstract public Move getMove ();
	abstract public bool moveSuccessfullyExcecuted ();
	abstract public int getId ();
	abstract public char getColor ();
}
