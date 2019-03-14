module Oware

type StartingPosition =
    | South
    | North
    //The Discriminated Union of North and South. The representation of the two players.

let getSeeds n board = failwith "Not implemented"
    //Accepts a house number and a ​board​, and returns the number of  seeds in the specified house. 

let useHouse n board = failwith "Not implemented"
    //Removes and sows the seeds from house n.

let start position = failwith "Not implemented"
    //Sets up the board tuples for the start of the game. Output is the board (?)

let score board = failwith "Not implemented"
    //Which accepts a ​board​ and gives back a tuple of (​southScore​ , ​northScore​)

let gameState board = failwith "Not implemented"
    //Determines whose turn it is.

[<EntryPoint>]
let main _ =
    printfn "Hello from F#!"
    0 // return an integer exit code.
