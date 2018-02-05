
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

startGame(){

	Player playerOne = new Player();
	Player playerTwo = new Player();

	setPlayer(playerOne, playerTwo);

	/*
	for (i=0; i<=7; i++){
		createPieceAndAdd(PieceType Pawn);
	}
	createPieceAndAdd(PieceType King);
	createPieceAndAdd(PieceType Knight);
	createPieceAndAdd(PieceType Knight);
	createPieceAndAdd(PieceType Bishop);
	createPieceAndAdd(PieceType Bishop);
	createPieceAndAdd(PieceType Queen);
	createPieceAndAdd(PieceType Rook);
	createPieceAndAdd(PieceType Rook);*/

	/*setPosition(a1, 1, 1); setPosition(b1, 2, 1);
	setPosition(c1, 3, 1); setPosition(d1, 4, 1); 
	setPosition(e1, 5, 1); setPosition(f1, 6, 1);
	setPosition(g1, 7, 1); setPosition(h1, 8, 1);

	setPosition(a2, 1, 2); setPosition(b2, 2, 2);
	setPosition(c2, 3, 2); setPosition(d2, 4, 2);
	setPosition(e2, 5, 2); setPosition(f2, 6, 2);
	setPosition(g2, 7, 2); setPosition(h2, 8, 2);

	setPosition(a3, 1, 3); setPosition(b3, 2, 3);
	setPosition(c3, 3, 3); setPosition(d3, 4, 3);
	setPosition(e3, 5, 3); setPosition(f3, 6, 3);
	setPosition(g3, 7, 3); setPosition(h3, 8, 3);

	setPosition(a4, 1, 4); 
	setPosition(b4, 2, 4);
	setPosition(c4, 3, 4);
	setPosition(d4, 4, 4);
	setPosition(e4, 5, 4);
	setPosition(f4, 6, 4); 
	setPosition(g4, 7, 4);
	setPosition(h4, 8, 4);

	setPosition(
	setPosition(
	setPosition(
	setPosition(
	setPosition(
	setPosition(
	setPosition(
	setPosition(*/

	/*Position a1 = new Position(a1, 1, 1);
	Position a2 = new Position(a2, 1, 2);
	Position a3 = new Position(a3, 1, 3);
	Position a4 = new Position(a4, 1, 4);
	Position a5 = new Position(a5, 1, 5);
	Position a6 = new Position(a6, 1, 6);
	Position a7 = new Position(a7, 1, 7);
	Position a8 = new Position(a8, 1, 8);*/

	Pawn1-w = new Piece(0, 1, a2, 1, 2);
	Pawn2-w = new Piece(0, 1, b2, 2, 2);
	Pawn3-w = new Piece(0, 1, c2, 3, 2);
	Pawn4-w = new Piece(0, 1, d2, 4, 2);
	Pawn5-w = new Piece(0, 1, e2, 5, 2);
	Pawn6-w = new Piece(0, 1, f2, 6, 2);
	Pawn7-w = new Piece(0, 1, g2, 7, 2);
	Pawn8-w = new Piece(0, 1, h2, 8, 2);

	RookL-w = new Piece(1, 1, a1, 1, 1);
	RookR-w = new Piece(1, 1, h1, 8, 1);
	KnightL-w = new Piece(2, 1, b1, 2, 1);
	KnightR-w = new Piece(2, 1, g1, 7, 1);
	BishopL-w = new Piece(3, 1, c1, 3, 1);
	BishopR-w = new Piece(3, 1, f1, 6, 1);
	Queen-w = new Piece(4, 1, d1, 5, 1);
	King-w = new Piece(5, 1, e1, 4, 1);

	Mark(a2, Pawn1-w);
	Mark(b2, Pawn2-w);
	Mark(c2, Pawn3-w);
	Mark(d2, Pawn4-w);
	Mark(e2, Pawn5-w);
	Mark(f2, Pawn6-w);
	Mark(g2, Pawn7-w);
	Mark(h2, Pawn8-w);

	Mark(a1, RookL-w); 
	Mark(h1, RookR-w);
	Mark(b1, KnightL-w); 
	Mark(g1, KnightR-w);
	Mark(c1, BishopL-w); 
	Mark(f1, BishopR-w);
	Mark(d1, Queen-w); 
	Mark(e1, King-w);
	
}

createPieceAndAdd(PieceType pieceType){

	int i = 0;
	while (pieces[i] != NULL){
		i++;
	}
	pieces[i].pieceType = pieceType;
        pieces[i].captured = false; 

}

isNonCapturedPieceAtPosition(Position pos){

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

