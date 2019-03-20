module Oware

//Inital idea for board
type house =
    //South
    | A of int //1
    | B of int //2
    | C of int //3
    | D of int //4
    | E of int //5
    | F of int //6
    //North
    | A' of int //7
    | B' of int //8
    | C' of int //9
    | D' of int //10
    | E' of int //11
    | F' of int //12




(*Prototype:
        Board as a represenation is a tuple of type house (contains the name of the house and the 
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
    | South of int
    | North of int
    //The Discriminated Union of North and South. The representation of the two players.

let getSeeds n board = failwith "Not implemented"
    //Accepts a house number and a ​board​, and returns the number of  seeds in the specified house. 

let useHouse n board = failwith "Not implemented"
    //Removes and sows the seeds from house n counterclockwise.

let start position = A 4, B 4, C 4, D 4, E 4, F 4, A' 4, B' 4, C' 4, D' 4, E' 4, F' 4
    //Sets up the board tuples for the start of the game. Output is the board (?)

let score board = failwith "Not implemented"
    //Which accepts a ​board​ and gives back a tuple of (​southScore​ , ​northScore​)

let gameState board = failwith "Not implemented"
    //Determines whose turn it is.

[<EntryPoint>]
let main _ =
    printfn "Hello from F#!"
    0 // return an integer exit code.
