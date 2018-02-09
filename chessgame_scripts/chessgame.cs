setPlayer(Player playerOne, Player playerTwo){

	Console.Write("PlayerOne, enter your id: ");
	playerOne.id = int.Parse(Console.ReadLine());

	Console.Write("PlayerTwo, enter your id: ");
	playerTwo.id = int.Parse(Console.ReadLine());

	Console.WriteLine("Player One, choose a color: b or w");

	char color = Console.ReadLine();

	if (color == "w"){
		playerOne.color = "w";
		playerTwo.color = "b";
	} else if (color == "b"){
		playerOne.color = "b";
		playerTwo.color = "w";
	}
}
