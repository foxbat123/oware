﻿
module Oware

open System


type StartingPosition =
    | South
    | North
    //The Discriminated Union of North and South. The representation of the two players.

type board = { //Using records to represent the houses, the name of field indicates the number of the house and the 
               //assoicated integer value is the number of seeds in that house
               //Holds the current score for both sides as well as who's turn it is currently
    A : int; B : int; C : int; D : int; E : int; F : int; a : int; b : int; c : int; d : int; e : int; f : int;
    SouthScore : int; NorthScore : int; Turn : StartingPosition }

let getSeeds n board = 
    match n with
    | 1 -> board.A
    | 2 -> board.B
    | 3 -> board.C
    | 4 -> board.D
    | 5 -> board.E
    | 6 -> board.F
    | 7 -> board.a
    | 8 -> board.b
    | 9 -> board.c
    | 10 -> board.d
    | 11 -> board.e
    | 12 -> board.f
    | _ -> failwith "Invalid house"
    //Accepts a house number and a ​board​, and returns the number of  seeds in the specified house. 

let start position = 
    //Accepts a starting postion and then initialisies the board record
    {A = 4; B = 4; C = 4; D = 4; E = 4; F = 4; a = 4; b = 4; c = 4; d = 4; 
    e = 4; f = 4; SouthScore = 0; NorthScore = 0; Turn = position}

let changeSeeds board n seeds =
    match n with 
    | 1 -> {board with A = seeds}
    | 2 -> {board with B = seeds}
    | 3 -> {board with C = seeds}
    | 4 -> {board with D = seeds}
    | 5 -> {board with E = seeds}
    | 6 -> {board with F = seeds}
    | 7 -> {board with a = seeds}
    | 8 -> {board with b = seeds}
    | 9 -> {board with c = seeds}
    | 10 -> {board with d = seeds}
    | 11 -> {board with e = seeds}
    | 12 -> {board with f = seeds}
    | _ -> failwith "Invalid house"

let checkDraw board = //This function exists to check whether there are NO seeds left in ANY houses. We use it in checkBoard to avoid it telling us a turn is illegal when it results in a draw
    let rec calculateDraw updatedBoard acc updatedStart =
        match updatedStart with 
        |13 -> acc
        |_ -> calculateDraw updatedBoard (acc + getSeeds updatedStart updatedBoard) (updatedStart + 1)
    
    match calculateDraw board 0 1 with 
    |0 -> true
    |_ -> false

let playerCantMove board = //This function contains the recursive function to check whether the move made will result in all the opponent losing all their seeds
    let rec checkZero updatedBoard acc updatedStart= //The definition of this recursive function takes in a board, an accumulator and a house number (as updatedStart)
        match updatedBoard.Turn with // We match the board with the player whose turn it is
        |North -> match updatedStart with // if its Norths' turn then we want to check Souths' board. 
                  |7 -> acc // When updatedStart reaches 7 we know we have checked every house on Souths' board, therefore we can return the accumulator
                  |_ -> checkZero updatedBoard (acc + getSeeds updatedStart updatedBoard) (updatedStart + 1) // Otherwise we simply call checkZero again, add the amount of seeds in the house we're currently looking at and then increment the updateStart integer.
        |South -> match updatedStart with
                  |13 -> acc
                  |_ -> checkZero updatedBoard (acc + getSeeds updatedStart updatedBoard) (updatedStart + 1)   

    match checkDraw board with //if checkDraw returns true the turn is legal as it results in a draw. Else the turn is illegal.
    |true -> false
    |false -> match board.Turn with //this match statement checks whose turn it is.
              |North -> match checkZero board 0 1 with // if it's Norths' turn then we pass it house number one, as this is the first house it will check. We want to check Souths' board after all
                        |0 ->true // if checkZero returns 0 then we know that the acccumulated amount of seeds is zero. This means that the move is illegal as you cannot leave your opponent with zero seeds
                        |_->false // if there is any other amount of seeds then the move is fine.
              |South -> match checkZero board 0 7 with
                        |0 ->true
                        |_ ->false
   
let houseOwner house = 
        match house <=6 with 
         |true -> South 
         |false -> North 

let capture board start = // This is going to be called within the useHouse function 
     
    let rec capturing updatedBoard updatedStart = // recursive function calls updatedBoard and updatedStart
        match updatedStart = 0 with  // This is the wrap around case
        |true -> capturing updatedBoard 12 // set updatedStart to 12 if it reaches 0
        |false ->  match ((getSeeds updatedStart updatedBoard) > 3 || getSeeds updatedStart updatedBoard < 2 || (houseOwner updatedStart) = board.Turn) with
                   |true -> updatedBoard
                   |false -> let newBoard = match updatedBoard.Turn with 
                                                | North -> {updatedBoard with NorthScore = updatedBoard.NorthScore + (getSeeds updatedStart updatedBoard)}
                                                | South -> {updatedBoard with SouthScore = updatedBoard.SouthScore + (getSeeds updatedStart updatedBoard)}
                             capturing (changeSeeds newBoard updatedStart 0) (updatedStart - 1)
    let returnBoard = capturing board start // calling the recursive function for the first time


    match playerCantMove returnBoard with //Checking if the move leaves the oppenent with zero seeds or not
    |true -> match board.Turn with  
             |North -> {board with board.Turn = South}
             |South ->{board with board.Turn = North}
    |false -> match returnBoard.Turn with //This changes the turn
              |North -> {returnBoard with board.Turn = South}
              |South ->{returnBoard with board.Turn = North}


let changeTurn board = 
    let updatedBoard= board
    match board.Turn with
    |North -> {updatedBoard with Turn = South}
    |South -> {updatedBoard with Turn = North}

let useHouse n board = // n is a house position on the board
        let initialCheck = playerCantMove board
        
        let rec seedFun updatedBoard updatedStart seedCount = // RYAN DID THIS!!

                match seedCount = 0 with 
                        |true ->   match playerCantMove updatedBoard with //Checking if the move leaves the oppenent with zero seeds or not
                                   |true -> match board.Turn with  
                                            |North -> board
                                            |South -> board
                                   |false -> match updatedStart with
                                    //check whether board being passed to capture is in fact a legal board, if not, pass the board in its original form, if legal, pass changed board.
                                             |13 -> capture updatedBoard 12 // decrement the house to look at the house where the last seed was sown
                                             |_ ->  capture updatedBoard (updatedStart - 1)// Base case when seedCount has 0 seeds - need to decrement the house                              
                        |_ -> match updatedStart = n, updatedStart = 13 with 
                                |true,false -> seedFun updatedBoard (updatedStart + 1) seedCount
                                |true, true -> seedFun updatedBoard (1) seedCount
                                |_ -> match updatedStart = 13 with // Wrap around logic. Goes back to 1 when house number reaches 13
                                        |true -> seedFun (changeSeeds updatedBoard 1 ((getSeeds 1 updatedBoard)+1)) 2 (seedCount - 1)
                                        |_ -> seedFun (changeSeeds updatedBoard updatedStart ((getSeeds updatedStart updatedBoard)+1)) (updatedStart + 1) (seedCount - 1)
        
        
        match (getSeeds n board) = 0 with // Returns board as is if house n has no seeds
            | true -> board
            |_ -> match (n < 7) && board.Turn = South || (n > 6) && board.Turn = North with // the test to check that you do not manipulate your opponent's board
                        | true -> let newBoard = seedFun (changeSeeds board n 0) (n + 1) (getSeeds n board)
                                  let finalCheck = playerCantMove newBoard
                                  match initialCheck && finalCheck with
                                  |true ->   board
                                  |false ->  newBoard
                        | false -> board
            
            
           

let score board =(board.SouthScore, board.NorthScore)

let gameState board = 
    let a = board.SouthScore
    let b = board.NorthScore
    match (board.Turn , (a = 24 && b = 24) , a > 24 , b > 24) with // Updated match expression to include a check to see whether the game has ended in a draw and whether any player has won
    | (_ , true, _ , _) -> "Game ended in a draw"
    | South , false , false , false -> "South's turn"
    | North , false, false , false -> "North's turn"
    | South , false , false , true -> "North won"
    | North , false, true , false -> "South won"
    | _ ->  failwith "Excuse me, what the frick did you just frickin' say about me, you little g19? I'll have you know I graduated top of my class in cs2, and I've been involved in numerous secret hacks on the IS department, and I have over 300 confirmed submissions.I have trained for years and I'm the top developer in the entire Rhodes Compuer Department. You are nothing to me but just another firstie. I will wipe you the frick out with code the likes of which has never been seen before on this Earth, mark my fricking words. You think you can get away with saying that kak to me over the Internet? Think again, fricker. As we speak I am contacting my secret network of hackers across campus and your IP is being traced right now so you better prepare for the storm, maggot. The storm that wipes out the pathetic little thing you call your computer. You're fricken' dead, kid. I can be anywhere, anytime, and I can hack you in over seven hundred ways, and that's just with my laptop. Not only am I extensively trained in C#, but I have access to the entire arsenal of languages and operating systems and I will use it to its full extent to wipe your miserable PC off the face of the continent, you little firstie. If only you could have known what unholy retribution your little clever comment was about to bring down upon you, maybe you would have held your fricken tongue. But you couldn't, you didn't, and now you're paying the price, you goddamn idiot. I will heck fury all over you and you will drown in it. You're frickenm dead, kiddo."
//Returns whose turn it is.

let playGame board = failwith "Not implemented"
        



let drawBoard board = 
    (* let string = 
            
          "              %s              " board.Turn
          "|============{N}===========|\n"
        + "|==%i==%i==%i==%i==%i==%i==|\n" board.a, board.b, board.c, board.d, board.e, board.f
        + "|--------------------------|\n"
        + "|==%i==%i==%i==%i==%i==%i==|\n" board.A, board.B, board.C, board.D, board.E, board.F
        + "|============{S}===========|\n" *)
        failwith "Not done"

[<EntryPoint>]
let main _ =
    printfn "Hello from F#!"
    0 // return an integer exit code.