module Oware

open System
open System
open System
open System
open System

//type player 

(*Prototype:
        Board as a represenation is a record (contains the name of the house and the 
        amount of seeds in that house)

        Seperate seeding and capture function
            Both functions need to be recursive 

        Can't capture from own house

        Where to store the score?

        Win state is if one side has 25+ seeds
            Check for draws as well

        How to check if move is legal
            If a move leaves the opponent with no seeds in their houses

            Capture is illegal if opponent would be left with no seeds on their side

        Record the amount of wins for the current session of the program
*)

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
    //Accepts a house number and a ​board​, and returns the number of  seeds in the specified house. 

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

let useHouse n board = 
        let rec seedFun updatedBoard updatedStart seedCount =
                match seedCount = 0 with
                        |true -> updatedBoard
                        |_ -> match updatedStart = n with 
                                |true -> seedFun updatedBoard (updatedStart + 1) seedCount
                                |_ -> match updatedStart = 13 with
                                        |true -> seedFun (changeSeeds updatedBoard 1 ((getSeeds 1 updatedBoard)+1)) 2 (seedCount - 1)
                                        |_ -> seedFun (changeSeeds updatedBoard updatedStart ((getSeeds updatedStart updatedBoard)+1)) (updatedStart + 1) (seedCount - 1)
        match (getSeeds n board) = 0 with
            | true -> board
            |_ -> seedFun (changeSeeds board n 0) (n + 1) (getSeeds n board)

let score board =
    let z = board //Binding z to input 
    let {SouthScore = SS ; NorthScore = NS} = z //Bind SouthScore and NorthScore values to SS and NS respectivly 
    (SS , NS) //Return a tuple of SS and NS


let gameState board = 
    match board.Turn with
    | North -> "North's turn"
    | South -> "South's turn"
    | _ -> failwith "Excuse me, what the frick did you just frickin' say about me, you little g19? I'll have you know I graduated top of my class in cs2, and I've been involved in numerous secret hacks on the IS department, and I have over 300 confirmed submissions.I have trained for years and I'm the top developer in the entire Rhodes Compuer Department. You are nothing to me but just another firstie. I will wipe you the frick out with code the likes of which has never been seen before on this Earth, mark my fricking words. You think you can get away with saying that kak to me over the Internet? Think again, fricker. As we speak I am contacting my secret network of hackers across campus and your IP is being traced right now so you better prepare for the storm, maggot. The storm that wipes out the pathetic little thing you call your computer. You're fricken' dead, kid. I can be anywhere, anytime, and I can hack you in over seven hundred ways, and that's just with my laptop. Not only am I extensively trained in C#, but I have access to the entire arsenal of languages and operating systems and I will use it to its full extent to wipe your miserable PC off the face of the continent, you little firstie. If only you could have known what unholy retribution your little clever comment was about to bring down upon you, maybe you would have held your fricken tongue. But you couldn't, you didn't, and now you're paying the price, you goddamn idiot. I will heck fury all over you and you will drown in it. You're frickenm dead, kiddo."
    //Returns whose turn it is.

let playGame board = failwith "Not implemented"
        
let capture board start = failwith "Not implemented"

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
