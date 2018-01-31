
setPlayer(playerOne:Player, playerTwo:Player){
	
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

createPieceAndAdd(pieceType: PieceType){

	int i = 0;
	while (pieces[i] != NULL){
		i++;
	}
	pieces[i].pieceType = pieceType;
        pieces[i].captured = false; 

}

isNonCapturedPieceAtPosition(pos: Position){

	int i = 0;
	while(pieces[i].position != pos){
		i++;
	}
	if( pieces[i].captured == false ){ 
		return true;
	}
	if( pieces[i].captured == true ){
		return false;
	}	
}

