
// Castle function
// Implements legal castling move

public void Castle(Piece king, Piece rook)
{
    Position kingPos = king.GetPosition();
    Position rookPos = rook.GetPosition();
  
    int kingPosX = kingPos.GetX();
    int rookPosX = rookPos.GetX();
      // is rook to the left or to the right of king?
    int diff = kingPosx - rookPosx;
 
    bool legal = IsCastleLegal(kingPos, rookPos, diff);
    
    if( (legal==true) && (diff>0) ) // rook is to the left
    {
      kingPos.SetX(kingPosX-2); //move king 2 spaces to the left
      kingPosX = kingPos.GetX();
      rookPos.SetX(kingPosX+1); //move rook to the right of king
    } 
    else if( (legal==true) && (diff<0) ) // rook is to the right
    {         
      kingPos.SetX(kingPosX+2); //move king 2 spaces to the right
      kingPosX = kingPos.GetX();
      rookPos.SetX(kingPosX-1); //move rook to the left of king
    }
    
}

public bool IsCastleLegal(Position kingPos, Position rookPos, int diff)
{
    bool legal = true;
    kingPosY = kingPos.GetY();
    rookPosY = rookPos.GetY();
    // are there pieces standing in between king and rook?
    if( diff > 0 ) // rook is to the left
    {
      if( (kingPosY==0) && (rookPosY==0) )
      {
        legal = IsOccupied(a2);
        if(legal==true) legal = IsOccupied(a3);
        if(legal==true) legal = IsOccupied(a4);
      }
      else if( (kingPosY==7) && (rookPosY==7) )
      {
        legal = IsOccupied(h2);
        if(legal==true) legal = IsOccupied(h3);
        if(legal==true) legal = IsOccupied(h4);
      }
    }
    
    else if( diff < 0 ) // rook is to the right
    {
      if( (kingPosY==0) && (rookPosY==0) )
      {
        legal = IsOccupied(a6);
        if(legal==true) legal = IsOccupied(a7);
      }
      else if( (kingPosY==7) && (rookPosY==7) )
      {
        legal = IsOccupied(h6);
        if(legal==true) legal = IsOccupied(h7);
      }
    }
    
    return legal;
}
  
  
  
