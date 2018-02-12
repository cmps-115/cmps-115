
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
	
	Pawn1-w = new Piece(0, 1, a2, 1, 2); Pawn1-b = new Piece(0, 0, a7, 1, 7);
	Pawn2-w = new Piece(0, 1, b2, 2, 2); Pawn2-b = new Piece(0, 0, b7, 2, 7);
	Pawn3-w = new Piece(0, 1, c2, 3, 2); Pawn3-b = new Piece(0, 0, c7, 3, 7);
	Pawn4-w = new Piece(0, 1, d2, 4, 2); Pawn4-b = new Piece(0, 0, d7, 4, 7);
	Pawn5-w = new Piece(0, 1, e2, 5, 2); Pawn5-b = new Piece(0, 0, e7, 5, 7);
	Pawn6-w = new Piece(0, 1, f2, 6, 2); Pawn6-b = new Piece(0, 0, f7, 6, 7);
	Pawn7-w = new Piece(0, 1, g2, 7, 2); Pawn7-b = new Piece(0, 0, g7, 7, 7);
	Pawn8-w = new Piece(0, 1, h2, 8, 2); Pawn8-b = new Piece(0, 0, h7, 8, 7);
  
	RookL-w = new Piece(1, 1, a1, 1, 1);   RookL-b = new Piece(1, 0, a8, 1, 8);
	RookR-w = new Piece(1, 1, h1, 8, 1);   RookR-b = new Piece(1, 0, h8, 8, 8);
	KnightL-w = new Piece(2, 1, b1, 2, 1); KnightL-b = new Piece(2, 0, b8, 2, 8);
	KnightR-w = new Piece(2, 1, g1, 7, 1); KnightR-b = new Piece(2, 0, g8, 7, 8);
	BishopL-w = new Piece(3, 1, c1, 3, 1); BishopL-b = new Piece(3, 0, c8, 3, 8);
	BishopR-w = new Piece(3, 1, f1, 6, 1); BishopR-b = new Piece(3, 0, f8, 6, 8);
	Queen-w = new Piece(4, 1, d1, 5, 1);   Queen-b = new Piece(4, 0, d8, 5, 8);
	King-w = new Piece(5, 1, e1, 4, 1);    King-b = new Piece(5, 0, e8, 4, 8)
		
	Mark(a2, Pawn1-w); Mark(a7, Pawn1-b);
	Mark(b2, Pawn2-w); Mark(b7, Pawn2-b);
	Mark(c2, Pawn3-w); Mark(c7, Pawn3-b);
	Mark(d2, Pawn4-w); Mark(d7, Pawn4-b);
	Mark(e2, Pawn5-w); Mark(e7, Pawn5-b);
	Mark(f2, Pawn6-w); Mark(f7, Pawn6-b);
	Mark(g2, Pawn7-w); Mark(g7, Pawn7-b);
	Mark(h2, Pawn8-w); Mark(h7, Pawn8-b);

	Mark(a1, RookL-w);   Mark(a8, RookL-b);
	Mark(h1, RookR-w);   Mark(h8, RookR-b);
	Mark(b1, KnightL-w); Mark(b8, KnightL-b);
	Mark(g1, KnightR-w); Mark(g8, KnightR-b);
	Mark(c1, BishopL-w); Mark(c8, BishopL-b);
	Mark(f1, BishopR-w); Mark(f8, BishopR-b);
	Mark(d1, Queen-w);   Mark(d8, Queen-b);
	Mark(e1, King-w);    Mark(e8, King-w);
}

createPieceAndAdd(PieceType pieceType){
	int PIECE_INT = 0;
	
	// new pieces are inserted in order into pieces array
	// pawns, kings, knights, bishops, queens, rooks
	if (pieceType == Pawn) PIECE_INT = 0;
	if (pieceType == King) PIECE_INT = 16;
	if (pieceType == Knight) PIECE_INT = 18;
	if (pieceType == Bishop) PIECE_INT = 22;
	if (pieceType == Queen) PIECE_INT = 26;	
	if (pieceType == Rook) PIECE_INT = 28;
	while (pieces[PIECE_INT].pieceType != null) PIECE_INT++;

	pieces[PIECE_INT].pieceType = pieceType;
        pieces[PIECE_INT].captured = 0;

	int team_color = 0;
	// pieces are given a team
	// black pieces: team_color = 0
	// white pieces: team_color = 1
	if ( PIECE_INT < 8 ) team_color++; //white pawns
	else if ( PIECE_INT == 16 ) team_color++; //white king
	else if ( (PIECE_INT == 18) || (PIECE_INT == 19) ) //white knights
		team_color++;
	else if ( (PIECE_INT == 22) || (PIECE_INT == 23) ) //white bishops
		team_color++;
	else if ( PIECE_INT == 26 ) team_color++; //white queen
	else if ( (PIECE_INT == 28) || (PIECE_INT == 29) ) //white rooks
		team_color++;
	pieces[PIECE_INT].team == team_color;
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

