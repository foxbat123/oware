module Oware

open System
open System
open System
open System
open System

type board = { //Using records to represent the houses, the name of field indicates the number of the house and the 
               //assoicated integer value is the number of seeds in that house
    A : int; B : int; C : int; D : int; E : int; F : int; a : int; b : int; c : int; d : int; e : int; f : int
}

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
    {A = 4; B = 4; C = 4; D = 4; E = 4; F = 4; a = 4; b = 4; c = 4; d = 4; e = 4; f = 4}
    //Sets up the board tuples for the start of the game. Output is the board (?) Position determines which player starts the game

let score board = failwith "Not implemented"
    //Which accepts a ​board​ and gives back a tuple of (​southScore​ , ​northScore​)

let gameState board = failwith "Not implemented"
    //Determines whose turn it is.

let useHouse start board =
    //Removes and sows the seeds from house start counterclockwise.
    let numSeeds = getSeeds start board //Grabs the amount of seeds that are picked up
    let board = changeSeeds board start 0 //Set the starting house to 0 seeds

    let rec seeding board house numseeds startHouse = //Recursive function to handle
        let board = changeSeeds board house ((getSeeds board house)+1) //Adds one seed to house
        let house = match house = 12 with //Handles the wrapping of the houses
                    | true -> 1
                    | false -> house
        match numseeds > 0 with
        | true -> match house = startHouse - 1 with //Skips the house we intially took from
                  | true -> seeding board (house+2) (numseeds-1) startHouse
                  | false -> seeding board (house+1) (numseeds-1) startHouse
        | false -> board //Not sure about what to do with the base case

    seeding board (start + 1) numSeeds start
        
let capture board start = failwith "Not implemented"

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

let drawBoard board = 
    let string = 
          "|============{N}===========|\n"
        + "|==%i==%i==%i==%i==%i==%i==|\n" board.a, board.b, board.c, board.d, board.e, board.f
        + "|--------------------------|\n"
        + "|==%i==%i==%i==%i==%i==%i==|\n" board.A, board.B, board.C, board.D, board.E, board.F
        + "|============{S}===========|\n"

[<EntryPoint>]
let main _ =
    printfn "Hello from F#!"
    0 // return an integer exit code.
