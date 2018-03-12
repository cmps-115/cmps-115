
// Human subclass for AI

GetMove(){

    return currentMove;
}

MoveSuccessfullyExecuted(Move move){

    bool success = 1;
    Piece piece_moved = getPiece();
    
    // sets success to the result of each rules function
    // rules functions return bool
    
    if( piece_moved.PieceType == Pawn )
        success = LegalPawnMove(move);
    else if( piece_moved.PieceType == King )
        success = LegalKingMove(move);
    else if( piece_moved.PieceType == Knight )
        success = LegalKnightMove(move);
    else if( piece_moved.PieceType == Bishop )
        success = LegalBishopMove(move);
    else if( piece_moved.PieceType == Queen )
        success = LegalQueenMove(move);
    else
        success = LegalRookMove(move);
    
    // will stop checking if success is ever false
    // and therefore will return false
    
    if( success == 1 ) success = ArePieceBlockingMove(move);
    if( success == 1 ) success = IsPositionFree(des);
    if( success == 1 ) success = IsPositionCapturable(des);
    
    return success;
}

GetID(){

    return id;
}

GetColor(){

    return color;
}


    
    
