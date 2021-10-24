module TestProject

open System
open System
open System
open NUnit.Framework

[<SetUp>]
let Setup () = ()

let ret x = Some x

let (>>=) (m: 'a option) f =
    match m with
    | Some x -> f x
    | None -> None

type MaybeBuilder() =
    member _.Return(x) = ret x
    member _.Bind(m, f) = m >>= f

let maybe = MaybeBuilder()

let IsBiggerThan2 num =
    match num with
    | _ when num > 2 -> Some num
    | _ -> None

[<Test>]
let Eitherのテスト () =
    let hoge n =
        maybe {
            let! a = IsBiggerThan2 n
            let! b = IsBiggerThan2(n - 1)
            let! c = IsBiggerThan2(n - 2)
            return (a, b, c)
        }

    printfn $"%A{hoge 6}"
    printfn $"%A{hoge 5}"
    printfn $"%A{hoge 4}"
    Assert.Pass()

[<Test>]
let オブジェクト型をリストに変換するテスト () =
    let someList = [ 1; 2 ] :> obj
    let list: List<int> = unbox someList
    printfn $"%A{list}"
    Assert.Pass()
