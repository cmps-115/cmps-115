
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
	createPieceAndAdd(PieceType Rook);

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

	// new pieces are inserted in order into pieces array
	// pawns, kings, knights, bishops, queens, rooks
	if (pieceType == Pawn) i = 0;
	if (pieceType == King) i = 16;
	if (pieceType == Knight) i = 18;
	if (pieceType == Bishop) i = 22;
	if (pieceType == Queen) i = 26;
	if (pieceType == Rook) i = 28;
	while (pieces[i] != NULL) i++;
	
	pieces[i].pieceType = pieceType;
        pieces[i].captured = false; 
}

isNonCapturedPieceAtPosition(Position pos){

	int i = 0;
	while(pieces[i].position != pos){
		i++;
	}
	if( pieces[i].captured == false ) 
		return true;
	if( pieces[i].captured == true )
		return false;
}

