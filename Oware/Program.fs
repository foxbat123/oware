module Oware

//Inital idea for board

type boardProto = {
    seed : int
    Number : int
}
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
    | South
    | North 
    //The Discriminated Union of North and South. The representation of the two players.

let getSeeds n board = 
    let v = 
    let temp = { House = n ; Number = v } 

    match n with
    |
    //Accepts a house number and a ​board​, and returns the number of  seeds in the specified house. 

let useHouse n board = failwith "Not implemented"
    //Removes and sows the seeds from house n counterclockwise.

let start position =     
    {House = 4 ; Number = 1},{House = 4 ; Number = 2},{House = 4 ; Number = 3},{House = 4 ; Number = 4},
    {House = 4 ; Number = 5},{House = 4 ; Number = 6},{House = 4 ; Number = 7},{House = 4 ; Number = 8},
    {House = 4 ; Number = 9},{House = 4 ; Number = 10},{House = 4 ; Number = 11},{House = 4 ; Number = 12}
    //Sets up the board tuples for the start of the game. Output is the board (?)

let score board = failwith "Not implemented"
    //Which accepts a ​board​ and gives back a tuple of (​southScore​ , ​northScore​)

let gameState board = failwith "Not implemented"
    //Determines whose turn it is.

[<EntryPoint>]
let main _ =
    printfn "Hello from F#!"
    0 // return an integer exit code.
