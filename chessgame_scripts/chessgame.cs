
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

	for (i=0; i<16; i++){
		createPieceAndAdd(PieceType Pawn);
	}	
	createPieceAndAdd(PieceType King);
	createPieceAndAdd(PieceType King);
	for (i=0; i<4; i++){
		createPieceAndAdd(PieceType Knight);
		createPieceAndAdd(PieceType Bishop);
		createPieceAndAdd(PieceType Rook);
	}
	createPieceAndAdd(PieceType Queen);
	createPieceAndAdd(PieceType Queen);
	
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
	int PIECE_INT = 0;
	
	// new pieces are inserted in order into pieces array
	// pawns, kings, knights, bishops, queens, rooks
	if (pieceType == Pawn) PIECE_INT = 0;
	if (pieceType == King) PIECE_INT = 16;
	if (pieceType == Knight) PIECE_INT = 18;
	if (pieceType == Bishop) PIECE_INT = 22;
	if (pieceType == Queen) PIECE_INT = 26;	if (pieceType == Rook) PIECE_INT = 28;
	while (pieces[PIECE_INT] != NULL) PIECE_INT++;

	pieces[PIECE_INT].pieceType = pieceType;
        pieces[PIECE_INT].captured = 0;

	// pieces are given a color
	if ( PIECE_INT < 8 ) pieces[PIECE_INT].team == 1; //white pawns
	if ( (PIECE_INT >= 8) && (PIECE_INT < 16) ) pieces[PIECE_INT].team == 0; //black pawns
	if ( PIECE_INT == 16 ) pieces[PIECE_INT].team == 1; //white king
	if ( PIECE_INT == 17 ) pieces[PIECE_INT].team == 0; //black king
	if ( (PIECE_INT == 18) || (PIECE_INT == 19) ) //white knights
		pieces[PIECE_INT].team == 1;
	if ( (PIECE_INT == 20) || (PIECE_INT == 21) ) //black knights
		pieces[PIECE_INT].team == 0;
	if ( (PIECE_INT == 22) || (PIECE_INT == 23) ) //white bishops
		pieces[PIECE_INT].team == 1;
	if ( (PIECE_INT == 24) || (PIECE_INT == 25) ) //black bishops
		pieces[PIECE_INT].team == 0;
	if ( PIECE_INT == 26 ) pieces[PIECE_INT].team == 1; //white queen
	if ( PIECE_INT == 27 ) pieces[PIECE_INT].team == 0; //black queen
	if ( (PIECE_INT == 28) || (PIECE_INT == 29) ) //white rooks
		pieces[PIECE_INT].team == 1;
	if ( (PIECE_INT == 30) || (PIECE_INT == 31) ) //black rooks
		pieces[PIECE_INT].team == 0;
}

endConditionReached(){

	boolean endCond = 0;
	if(GameState == Checkmate || GameState == Draw){
		endCond = 1; 
	} else if(GameState == InProgress){
		endCond = 0; 
	}
	return endCond;
}

isNonCapturedPieceAtPosition(Position pos){
	int PIECE_INT = 0;
	
	// go through pieces array to get piece with Position pos
	while(pieces[PIECE_INT].position != pos){
		PIECE_INT++;
	}
	if( pieces[PIECE_INT].captured == false ) 
		return true;
	if( pieces[PIECE_INT].captured == true )
		return false;
}

