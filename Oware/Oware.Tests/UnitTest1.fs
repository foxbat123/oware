module Oware.Tests
open NUnit.Framework
open FsUnit

let charOf = function
    | 1 -> 'A' | 2 -> 'B' | 3 -> 'C' | 4 -> 'D' | 5 -> 'E' | 6 -> 'F'
    | 7 -> 'a' | 8 -> 'b' | 9 -> 'c' | 10 -> 'd' | 11 -> 'e' | 12 -> 'f'

let makeString = List.map (charOf >> string) >> String.concat ""

let hasSeedCount (a,b,c,d,e,f,a',b',c',d',e',f') v =
    getSeeds 1 v |> should equal a
    getSeeds 2 v |> should equal b
    getSeeds 3 v |> should equal c
    getSeeds 4 v |> should equal d
    getSeeds 5 v |> should equal e
    getSeeds 6 v |> should equal f
    getSeeds 7 v |> should equal a'
    getSeeds 8 v |> should equal b'
    getSeeds 9 v |> should equal c'
    getSeeds 10 v |> should equal d'
    getSeeds 11 v |> should equal e'
    getSeeds 12 v |> should equal f'

let playGame numbers =
    let rec play xs game =
        match xs with
        | [] -> game
        | x::xs -> play xs (useHouse x game)
    play numbers (start South)

[<Test>]
let ``Every house has 4 seeds in it at the start`` () =
    let initial = start South
    initial |> hasSeedCount (4,4,4,4,4,4,4,4,4,4,4,4)

[<Test>]
let ``When house 1 is used, seeds go to the other houses`` () =
    let board = start South |> useHouse 1
    board |> hasSeedCount (0,5,5,5,5,4,4,4,4,4,4,4)

[<Test>]
let ``When house 2 is used, seeds go to the other houses`` () =
    let board = start South |> useHouse 2
    board |> hasSeedCount (4,0,5,5,5,5,4,4,4,4,4,4)

[<Test>]
let ``When house 3 is used, seeds go to the other houses`` () =
    let board = start South |> useHouse 3
    board |> hasSeedCount (4,4,0,5,5,5,5,4,4,4,4,4)

[<Test>]
let ``When house 4 is used, seeds go to the other houses`` () =
    let board = start South |> useHouse 4
    board |> hasSeedCount (4,4,4,0,5,5,5,5,4,4,4,4)

[<Test>]
let ``When house 5 is used, seeds go to the other houses`` () =
    let board = start South |> useHouse 5
    board |> hasSeedCount (4,4,4,4,0,5,5,5,5,4,4,4)

[<Test>]
let ``When house 6 is used, seeds go to the other houses`` () =
    let board = start South |> useHouse 6
    board |> hasSeedCount (4,4,4,4,4,0,5,5,5,5,4,4)

[<Test>]
let ``When house 7 is used, seeds go to the other houses`` () =
    let board = start North |> useHouse 7
    board |> hasSeedCount (4,4,4,4,4,4,0,5,5,5,5,4)

[<Test>]
let ``When house 8 is used, seeds go to the other houses`` () =
    let board = start North |> useHouse 8
    board |> hasSeedCount (4,4,4,4,4,4,4,0,5,5,5,5)

[<Test>]
let ``When house 9 is used, seeds go to the other houses`` () =
    let board = start North |> useHouse 9
    board |> hasSeedCount (5,4,4,4,4,4,4,4,0,5,5,5)

[<Test>]
let ``When house 10 is used, seeds go to the other houses`` () =
    let board = start North |> useHouse 10
    board |> hasSeedCount (5,5,4,4,4,4,4,4,4,0,5,5)

[<Test>]
let ``When house 11 is used, seeds go to the other houses`` () =
    let board = start North |> useHouse 11
    board |> hasSeedCount (5,5,5,4,4,4,4,4,4,4,0,5)

[<Test>]
let ``When house 12 is used, seeds go to the other houses`` () =
    let board = start North |> useHouse 12
    board |> hasSeedCount (5,5,5,5,4,4,4,4,4,4,4,0)

[<Test>]
let ``Seeds are captured when there are 2 or 3 of them`` () =
    score (playGame [4;7;5;8;2]) |> should equal (2,0)
    score (playGame [4;7;5;8;2;11]) |> should equal (2,4)

[<Test>]
let ``The specified player goes first`` () =
    gameState (start South) |> should equal "South's turn"
    gameState (start North) |> should equal "North's turn"

[<Test>]
let ``The player turns alternate`` () =
    let game = start South
    gameState game |> should equal "South's turn"
    let takenturn = useHouse 1 game
    gameState takenturn |> should equal "North's turn"

[<Test>]
let ``A player cannot manipulate their opponent's houses`` () =
    let game = start South in
        useHouse 10 game |> should equal game
    let game = start North in
        useHouse 1 game |> should equal game

[<Test>]
let ``You cannot sow from an empty house`` () =
    let game = playGame [4; 11; 4]
    game |> hasSeedCount (5, 5, 5, 0, 5, 5, 5, 5, 4, 4, 0, 5)
    gameState game |> should equal "South's turn"

[<Test>]
let ``Seeds can't be captured from your own side`` () =
    let game = playGame [1; 7; 2; 8; 3; 9; 4; 12; 5]
    game |> hasSeedCount (4, 3, 2, 1, 0, 9, 5, 3, 2, 9, 9, 1)
    score game |> should equal (0, 0)

[<Test>]
let ``Seeds aren't captured on a non-final space`` () =
    score (playGame [1;9;2;8;4]) |> should equal (0,0)

[<Test>]
let ``Contiguous captured seeds are taken`` () =
    let game = playGame [1; 12; 5; 11; 2; 10; 3]
    game |> hasSeedCount (3, 1, 0, 8, 2, 7, 7, 7, 7, 1, 0, 0)
    score game |> should equal (5, 0)
    gameState game |> should equal "North's turn"

[<Test>]
let ``The original house is skipped when sowing seeds`` () =
    let game = playGame [1; 7; 2; 9; 3; 10; 1; 11; 2; 9; 4; 7; 5; 12; 3; 11; 6]
    game |> hasSeedCount (6, 4, 1, 3, 3, 0, 5, 12, 6, 5, 1, 2)

[<Test>]
let ``Non-contiguous captured seeds are not taken`` () =
    let game = playGame [1; 12; 3; 10; 5; 8; 1; 12; 3; 10; 4; 8; 2]
    game |> hasSeedCount (2, 0, 1, 1, 2, 8, 8, 1, 10, 2, 10, 0)
    score game |> should equal (3, 0)

[<Test>]
let ``Contiguous seeds from the player's houses are not taken`` () =
    let game = playGame [1; 8; 6; 10; 3; 12; 4; 12; 5; 9; 2; 8]
    game |> hasSeedCount (0, 0, 3, 2, 2, 1, 9, 0, 2, 5, 10, 3)
    score game |> should equal (0, 11)

[<Test>]
let ``The side with 25 or more seeds wins`` () =
    let game = playGame [2; 11; 3; 10; 4; 12; 1; 8; 6; 7; 5; 12; 2; 11; 1; 10]
    game |> hasSeedCount (1, 0, 0, 1, 0, 1, 1, 3, 10, 0, 1, 2)
    score game |> should equal (0, 28)
    gameState game |> should equal "North won"

[<Test>]
let ``Must play to give opponent pieces, if they have none`` () =
    let game = playGame [6; 8; 5; 9; 4; 12; 3; 10; 1; 11; 2; 12; 5; 7; 5; 11; 6; 8; 1; 12; 4; 10; 5; 9;
 2; 11; 3; 12; 6; 9; 5; 10; 2; 11; 1; 12; 4; 7; 6; 7; 3; 8; 5; 9; 6; 10; 1; 11;
 2; 12; 3; 8; 4; 9; 1; 10; 2; 11; 3; 12; 1]
    game |> hasSeedCount (1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0)
    score game |> should equal (22, 22)
    gameState game |> should equal "South's turn"

[<Test>]
let ``Can't capture pieces if that would remove all pieces from opponent's board.`` () =
    let game = playGame [6; 8; 5; 9; 4; 12; 3; 10; 1; 11; 2; 12; 5; 7; 5; 11; 6; 8; 1; 12; 4; 10; 5; 9;
 2; 11; 3; 12; 6; 9; 5; 10; 2; 11; 1; 12; 4; 7; 6; 7; 3; 8; 5; 9; 6; 10; 1; 11;
 2; 12; 3; 8; 4; 9; 1; 10; 2; 11; 5; 12; 6; 8; 3; 9; 4; 10; 5; 11; 1; 12; 6]
    game |> hasSeedCount (1, 1, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0)
    score game |> should equal (22, 22)
    gameState game |> should equal "North's turn"

[<Test>]
let ``A draw exists when each side has 24 pieces`` () =
    let game = playGame [6; 8; 5; 9; 4; 12; 3; 10; 1; 11; 2; 12; 5; 7; 5; 11; 6; 8; 1; 12; 4; 10; 5; 9;
 2; 11; 3; 12; 6; 9; 5; 10; 2; 11; 1; 12; 4; 7; 6; 7; 3; 8; 5; 9; 6; 10; 1; 11;
 2; 12; 3; 8; 4; 9; 1; 10; 6; 7; 2; 8; 3; 9; 4; 10; 5; 11; 6; 12]
    game |> hasSeedCount (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)
    score game |> should equal (24, 24)
    gameState game |> should equal "Game ended in a draw"