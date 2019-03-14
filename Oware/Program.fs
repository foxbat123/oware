module Oware

type StartingPosition =
    | South
    | North

let getSeeds n board = failwith "Not implemented"

let useHouse n board = failwith "Not implemented"
    //Removes and sows the seeds from house n 

let start position = failwith "Not implemented"
    //Sets up the board tuples for the start of the game

let score board = failwith "Not implemented"

let gameState board = failwith "Not implemented"
    //Determines who's turn it is

[<EntryPoint>]
let main _ =
    printfn "Hello from F#!"
    0 // return an integer exit code
